using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
	public class Media
	{
		public string Name { get; set; }
		public TimeSpan Duration { get; set; }
		public string FileName { get; set; }
		public string Type { get; set; }
		public string Genre { get; set; }
		public string Singler { get; set; }
		public long PlayListId { get; set; }
		public PlayList PlayList { get; set; }
		public double BitRate { get; set; }
		public double Rate { get; set; }
	}
}
