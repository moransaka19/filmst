using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Database;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Widget;
using filmstermob.Droid.Helpers;
using filmstermob.Services;
using Plugin.FilePicker.Abstractions;
using Plugin.MediaManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Xamarin.Forms;

namespace filmstermob.Droid
{
    [Activity(Label = "filmstermob", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static int OPENGALLERYCODE = 100;
        private Context context;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            context = Application.ApplicationContext;
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            ObservableCollection<string> medias = new ObservableCollection<string>();
            if (resultCode == Result.Canceled)
            {
                // Notify user file picking was cancelled.
                OnFilePickCancelled();
                Finish();
            }
            else
            {
                System.Diagnostics.Debug.Write(data.Data);
                try
                {
                    ClipData clipData = data.ClipData;
                    if (clipData != null)
                    {
                        for (int i = 0; i < clipData.ItemCount; i++)
                        {
                            ClipData.Item item = clipData.GetItemAt(i);
                            Android.Net.Uri _uri = item.Uri;
                            var filePath = IOUtil.getPath(context, _uri);

                            if (string.IsNullOrEmpty(filePath))
                                filePath = _uri.Path;

                            var file = IOUtil.readFile(filePath);

                            var fileName = GetFileName(context, _uri);

                            var newPath = MeidaHelper.SaveFile(Configs.FolderForMedia, file, fileName);
                            var t = MediaServ.GetMedia(newPath);
                            medias.Add(filePath);
                            //OnFilePicked(new FilePickerEventArgs(fileName, filePath));
                        }
                    }
                    else
                    {
                        var _uri = data.Data;

                        var filePath = IOUtil.getPath(context, _uri);

                        if (string.IsNullOrEmpty(filePath))
                            filePath = _uri.Path;

                        var file = IOUtil.readFile(filePath);

                        var fileName = GetFileName(context, _uri);

                        var newPath = MeidaHelper.SaveFile(Configs.FolderForMedia, file, fileName);
                        var t = MediaServ.GetMedia(newPath);
                        medias.Add(filePath);

                        OnFilePicked(new FilePickerEventArgs(fileName, filePath));
                    }
                }
                catch (Exception readEx)
                {
                    OnFilePickCancelled();
                    System.Diagnostics.Debug.Write(readEx);
                }
            }
            MessagingCenter.Send<App, ObservableCollection<string>>((App)Xamarin.Forms.Application.Current, "MediasSelected", medias);
        }

        string GetFileName(Context ctx, Android.Net.Uri uri)
        {

            string[] projection = { MediaStore.MediaColumns.DisplayName };

            var cr = ctx.ContentResolver;
            var name = "";
            var metaCursor = cr.Query(uri, projection, null, null, null);

            if (metaCursor != null)
            {
                try
                {
                    if (metaCursor.MoveToFirst())
                    {
                        name = metaCursor.GetString(0);
                    }
                }
                finally
                {
                    metaCursor.Close();
                }
            }
            return name;
        }

        internal static event EventHandler<FilePickerEventArgs> FilePicked;
        internal static event EventHandler<EventArgs> FilePickCancelled;

        private static void OnFilePickCancelled()
        {
            FilePickCancelled?.Invoke(null, null);
        }

        private static void OnFilePicked(FilePickerEventArgs e)
        {
            var picked = FilePicked;

            if (picked != null)
                picked(null, e);
        }

        public String GetRealPathFromURI(Android.Net.Uri contentURI)
        {
            try
            {
                ICursor imageCursor = null;
                string fullPathToAudio = "";

                imageCursor = ContentResolver.Query(contentURI, null, null, null, null);
                imageCursor.MoveToFirst();
                int idx = imageCursor.GetColumnIndex(MediaStore.Audio.AudioColumns.Data);

                if (idx != -1)
                {
                    fullPathToAudio = imageCursor.GetString(idx);
                }
                else
                {
                    ICursor cursor = null;
                    var docID = DocumentsContract.GetDocumentId(contentURI);
                    var id = docID;
                    if (docID.Contains(":"))
                    {
                        id = docID.Split(':')[1];
                    }
                    var whereSelect = MediaStore.Audio.AudioColumns.Id + "=?";
                    var projections = new string[] { MediaStore.Audio.AudioColumns.Data};

                    cursor = ContentResolver.Query(MediaStore.Audio.Media.InternalContentUri, projections, whereSelect, new string[] { id }, null);
                    if (cursor.Count == 0)
                    {
                        cursor = ContentResolver.Query(MediaStore.Audio.Media.ExternalContentUri, projections, whereSelect, new string[] { id }, null);
                    }
                    var colData = cursor.GetColumnIndexOrThrow(MediaStore.Audio.AudioColumns.Data);
                    cursor.MoveToFirst();
                    fullPathToAudio = cursor.GetString(colData);
                }
                return fullPathToAudio;
            }
            catch (Exception ex)
            {
                Toast.MakeText(Xamarin.Forms.Forms.Context, "Unable to get path", ToastLength.Long).Show();
            }

            return null;

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}