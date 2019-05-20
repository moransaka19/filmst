using System;
using System.Collections.Generic;
using System.Text;

namespace filmstermob.ViewModels
{
    public class FileViewModel
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public FileViewModel(string name)
        {
            Name = name;
        }

        public FileViewModel(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }
}
