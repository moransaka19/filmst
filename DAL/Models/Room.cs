using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Room
    {
        [Key]
        public string Name { get; set; }
        public string Password { get; set; }
        public ICollection<ApplicationUser> UsersIn { get; set; }
        public ICollection<Track> TracksIn { get; set; }
    }
}
