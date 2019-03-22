using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using filmst._0.Data;
using filmst._0.Models;
using Microsoft.AspNetCore.Mvc;

namespace filmst.Controllers
{
    //TODO: Add service for rooms;
    //TODO: Add repository for rooms;
    //TODO: Add Unit of work;
    //TODO: Add logging for each instance
    //TODO: Add ViewModels; Add DTOs; Add entities layers;
    //TODO: Add factories for each of type entities;
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
