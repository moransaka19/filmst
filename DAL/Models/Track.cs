using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Track
    {
        [Key]
        public string TrackName { get; set; }
        public ICollection<string> MediaTag { get; set; }
        public int FileSize { get; set; }
    }
}
