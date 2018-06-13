using filmst._0.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace filmst._0.Models
{
    public class Room
    {
        [Key]
        public string Name { get; set; }
        public string Password { get; set; }
        public ICollection<ApplicationUser> UsersIn { get; set; }
        public ICollection<Track> TracksIn { get; set; }

		public Room()
		{
			UsersIn = new List<ApplicationUser>();
			TracksIn = new List<Track>();
		}
    }
}
