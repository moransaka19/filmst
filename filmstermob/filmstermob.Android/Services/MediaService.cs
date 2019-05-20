using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using filmstermob.Contracts;
using filmstermob.Droid.Services;
using filmstermob.ViewModels;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(MediaService))]
namespace filmstermob.Droid.Services
{
    public class MediaService : Java.Lang.Object, IMediaService
    {
        public ObservableCollection<FileViewModel> GetFiles()
        {
            ObservableCollection<FileViewModel> files = new ObservableCollection<FileViewModel>();
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string fullpath = System.IO.Path.Combine(path, Configs.FolderForMedia);
            var fileDir = new Java.IO.File(fullpath);
            if (!fileDir.Exists())
            {
                fileDir.Mkdirs();
            }

            var fileNames = Directory
                .EnumerateFiles(fullpath, "*", SearchOption.AllDirectories);

            foreach (var item in fileNames)
            {
                files.Add(new FileViewModel(Path.GetFileName(item), item));
            }

            return files;
        }

        public async Task OpenMedia()
        {
            try
            {
                Toast.MakeText(Xamarin.Forms.Forms.Context, "Select max 20 audio", ToastLength.Long).Show();
                var mediaIntent = new Intent(
                    Intent.ActionPick);
                mediaIntent.SetType("audio/*");
                mediaIntent.PutExtra(Intent.ExtraAllowMultiple, true);
                mediaIntent.SetAction(Intent.ActionGetContent);
                ((Activity)Forms.Context).StartActivityForResult(
                    Intent.CreateChooser(mediaIntent, "Select photo"), MainActivity.OPENGALLERYCODE);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Toast.MakeText(Xamarin.Forms.Forms.Context, "Error. Can not continue, try again.", ToastLength.Long).Show();
            }
        }


        void IMediaService.ClearFiles(List<string> filePaths)
        {
            foreach (var p in filePaths)
            {
                if (File.Exists(p))
                {
                    File.Delete(p);
                }
            }
        }

    }
}