using System;
using System.Collections.Generic;
using System.Text;

namespace filmstermob.Models
{
    public class Room
    {
        public string Name { get; set; }
        public string UniqName { get; set; }
        public string Password { get; set; }
        public List<Media> Medias { get; set; }
    }
}
