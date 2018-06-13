using filmst._0.Data;
using filmst._0.Models;
using filmst.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace filmst.Controllers
{
	[Authorize]
	[Route("[controller]/[action]")]
	public class RoomController : Controller
    {
		private readonly ApplicationDbContext _db;
		private readonly UserManager<ApplicationUser> _userManager;

		public RoomController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			_db = context;
			_userManager = userManager;
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			IEnumerable<Room> rooms = _db.Rooms;

			return Ok(rooms);
		}

		[HttpGet]
		public IActionResult Get(string RoomName)
		{
			Room room = _db.Rooms.Where(r => r.Name == RoomName).FirstOrDefault();

			return Ok(room);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateRoomViewModel model)
		{
			Room room = new Room { Name = model.Name, Password = model.Password };
			ApplicationUser user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

			room.UsersIn.Add(user);

			_db.Set<Room>().Add(room);
			_db.SaveChanges();

			return Ok();
		}

		[HttpGet]
		public async Task<IActionResult> AddUserToRoom(string UserName, string roomName)
		{
			ApplicationUser user = await _userManager.FindByNameAsync(UserName);

			var room = _db.Rooms
				.Where(r => r.Name.ToUpper() == roomName.ToUpper())
				.Include(r => r.UsersIn)
				.Include(r => r.TracksIn)
				.FirstOrDefault();

			room.UsersIn.Add(user);

			_db.Entry(room).State = EntityState.Modified;

			_db.SaveChanges();

			return Ok();
		}
	}
}
