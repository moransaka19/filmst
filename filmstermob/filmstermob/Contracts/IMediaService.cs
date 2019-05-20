using filmstermob.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace filmstermob.Contracts
{
    public interface IMediaService
    {
        Task OpenMedia();
        ObservableCollection<FileViewModel> GetFiles();
        void ClearFiles(List<string> filePaths);
    }
}
