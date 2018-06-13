using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using filmst._0.Data;
using filmst._0.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace filmst.Controllers
{
	[Route("[controller]/[action]")]
    public class TrackController : Controller
    {
		private readonly ApplicationDbContext _db;
		
		public TrackController(ApplicationDbContext dbContext)
		{
			_db = dbContext;
		}
		
		[HttpGet]
        public IActionResult GetAll()
        {
			IEnumerable<Track> tracks = _db.Set<Track>();

            return Ok(tracks);
        }

		[HttpPost]
		public IActionResult Create([FromBody] Track track)
		{
			_db.Set<Track>().Add(track);
			_db.SaveChanges();

			return Ok();
		}
    }
}
