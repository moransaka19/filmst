using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Abstractions.DAL.Models
{
	public interface IMedia
	{
		string Name { get; set; }
		TimeSpan Duration { get; set; }
		string FileName { get; set; }
		string Type { get; set; }
		string Genre { get; set; }
		string Singler { get; set; }
		long PlayListId { get; set; }
		IPlayList PlayList { get; set; }
		double BitRate { get; set; }
		double Rate { get; set; }
	}
}
