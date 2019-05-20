using System;
using System.Collections.Generic;
using TagLib;
using System.Text;
using System.Linq;

namespace filmstermob.Services
{
    public static class MediaServ
    {
        public static filmstermob.Models.Media GetMedia(string fullPath)
        {
            filmstermob.Models.Media result = null;
            if (System.IO.File.Exists(fullPath))
            {
                using (File file = File.Create(fullPath))
                {
                    var genre = file.Tag.Genres.Length > 0 ? file.Tag.Genres.Aggregate((cur, next) => $"{cur},{next}") : "";
                    var singler = file.Tag.Artists.Length > 0 ? file.Tag.Artists.Aggregate((cur, next) => $"{cur},{next}") : "";
                    result = new filmstermob.Models.Media
                    {
                        Name = file.Name,
                        Duration = file.Properties.Duration,
                        FileName = fullPath,
                        Type = file.Properties.MediaTypes.ToString(),
                        Genre = genre,
                        Singler = singler,
                        BitRate = file.Properties.AudioBitrate,
                        Rate = file.Properties.AudioSampleRate,
                        Album = file.Tag.Album,
                        Description = file.Properties.Description,
                        MimeType = file.MimeType,
                        StartPosition = file.InvariantStartPosition,
                        EndPosition = file.InvariantEndPosition
                    };
                }
            }

            return result;
        }

        public static IEnumerable<filmstermob.Models.Media> GetMedia(params string[] fullPaths)
        {
            List<filmstermob.Models.Media> result = new List<filmstermob.Models.Media>();
            foreach (var fullPath in fullPaths.Where(path => path != null && System.IO.File.Exists(path)))
            {
                using (File file = File.Create(fullPath))
                {
                    var mediaFile = new filmstermob.Models.Media
                    {
                        Name = file.Name,
                        Duration = file.Properties.Duration,
                        FileName = fullPath,
                        Type = file.Properties.MediaTypes.ToString(),
                        Genre = file.Tag.Genres.Aggregate((cur, next) => $"{cur},{next}"),
                        Singler = file.Tag.Artists.Aggregate((cur, next) => $"{cur},{next}"),
                        BitRate = file.Properties.AudioBitrate,
                        Rate = file.Properties.AudioSampleRate,
                        Album = file.Tag.Album,
                        Description = file.Properties.Description,
                        MimeType = file.MimeType,
                        StartPosition = file.InvariantStartPosition,
                        EndPosition = file.InvariantEndPosition
                    };
                    result.Add(mediaFile);
                }
            }
            return result;
        }
    }
}
