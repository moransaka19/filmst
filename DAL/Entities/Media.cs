using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using SharedKernel.Abstractions.DAL.Models;

namespace DAL.Entities
{
	// Create
	// Read
	public class Media : IMedia
	{
		public string Name { get; set; }
		public TimeSpan Duration { get; set; }
		public string FileName { get; set; }
		public string Type { get; set; }
		public string Genre { get; set; }
		public string Singler { get; set; }
		public long PlayListId { get; set; }
		private PlayList _playList { get; set; }
		public double BitRate { get; set; }
		public double Rate { get; set; }
        public string Album { get; set; }
        public string Description { get; set; }
        public string MimeType { get; set; }
        public decimal StartPosition { get; set; }
        public decimal EndPostiotion { get; set; }

        [NotMapped]
		IPlayList IMedia.PlayList
		{
			get => _playList;
			set => _playList = value as PlayList;
		}
	}
}
