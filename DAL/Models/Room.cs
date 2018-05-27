using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Room
    {
        [Key]
        public string RoomName { get; set; }
        public string RoomPassword { get; set; }
        public ICollection<ApplicationUser> UsersInRoom { get; set; }
        public ICollection<Track> TracksInRoom { get; set; }
    }
}
