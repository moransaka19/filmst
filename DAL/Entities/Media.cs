using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using SharedKernel.Abstractions.DAL.Models;

namespace DAL.Entities
{
	public class Media : IMedia
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

		[NotMapped]
		IPlayList IMedia.PlayList
		{
			get => PlayList;
			set => PlayList = value as PlayList;
		}
	}
}
