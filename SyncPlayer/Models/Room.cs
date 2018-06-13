using System.Collections.Generic;

namespace SyncPlayer.Models
{
	public class Room
    {
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
