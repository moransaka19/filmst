using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using filmstermob.Services;

namespace filmstermob.Droid.Helpers
{
    class MeidaHelper
    {
        public static byte[] GetMedia(string path)
        {
            byte[] mediaBytes;

            using (var streamReader = new StreamReader(path))
            {
                using (var memstream = new MemoryStream())
                {
                    streamReader.BaseStream.CopyTo(memstream);
                    mediaBytes = memstream.ToArray();
                }
                return mediaBytes;
            }
        }

        public static string SaveFile(string collectionName, byte[] mediaByte, string fileName)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            fileName = fileName.Replace(" ", string.Empty);
            string fullpath = System.IO.Path.Combine(path, collectionName);
            var fileDir = new Java.IO.File(fullpath);
            if (!fileDir.Exists())
            {
                fileDir.Mkdirs();
            }

            var pathWithFile = System.IO.Path.Combine(fullpath, fileName);

            using (FileStream fs = File.Create(pathWithFile))
            {
                fs.Write(mediaByte, 0, mediaByte.Length);
            }

            return pathWithFile;
        }
    }
}