using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Track
    {
        [Key]
        public string TrackName { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Group { get; set; }
        public string Album { get; set; }
        public string Bits { get; set; }
        public string Bitrate { get; set; }
        public string Length { get; set; }
        public string Year { get; set; }
        public string AudioChanels { get; set; }
        public int FileSize { get; set; }
    }
}
