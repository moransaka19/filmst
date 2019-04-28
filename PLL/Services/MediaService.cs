using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagLib;
using DAL.Entities;
using SharedKernel.Abstractions.PLL.Media;
using SharedKernel.Abstractions.DAL.Models;

namespace PLL.Services
{
    public class MediaService : IMediaService
    {
        public IMedia GetMedia(string fullPath)
        {
            IMedia result = null;
            if (!string.IsNullOrEmpty(fullPath) && System.IO.File.Exists(fullPath))
            {
                using (File file = File.Create(fullPath))
                {
                    result = new Media
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
                        EndPostiotion = file.InvariantEndPosition
                    };
                }
            }

            return result;
        }

        public IEnumerable<IMedia> GetMedia(params string[] fullPaths)
        {
            List<IMedia> result = new List<IMedia>();
            foreach (var fullPath in fullPaths.Where(path => !string.IsNullOrEmpty(path) && System.IO.File.Exists(path)))
            {
                using (File file = File.Create(fullPath))
                {
                    var mediaFile = new Media
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
                        EndPostiotion = file.InvariantEndPosition
                    };
					result.Add(mediaFile);
                }
            }
            return result;
        }
    }
}
