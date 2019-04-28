using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Types.Sql;
using SharedKernel.Abstractions.DAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SharedKernel.Abstractions.PLL.Media
{
    public interface IMediaService
    {
        IMedia GetMedia(string fullPath);
        IEnumerable<IMedia> GetMedia(params string[] fullPaths);
    }
}