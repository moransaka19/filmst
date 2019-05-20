using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PLL.ViewModels.Rooms;
using SharedKernel.Abstractions.PLL.Rooms;
using SharedKernel.Abstractions.PLL.Rooms.Models;
using SharedKernel.Exceptions;

namespace Filmst.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class RoomsController : ControllerBase
	{
		private readonly IRoomController _roomController;

		public RoomsController(IRoomController roomController)
		{
			_roomController = roomController;
		}

		[HttpGet("{roomName}")]
		public ActionResult<IRoomViewModel> Get(string roomName)
		{
			IRoomViewModel roomInfo;

			try
			{
				roomInfo = _roomController.GetRoomInfo(roomName);
			}
			catch (KeyNotFoundException)
			{
				return BadRequest("Room not found");
			}

			return Ok(roomInfo);
		}

		[HttpPost("SignIn")]
		public async Task<IActionResult> Post([FromBody] SignInRoomViewModel model)
		{
			try
			{
				await _roomController.SignInAsync(model);
			}
			catch (RoomNotFoundException)
			{
				return NotFound();
			}
			catch (IncorrectPasswordException)
			{
				return BadRequest("Incorrect password");
			}
			catch
			{
				return StatusCode(500);
			}

			return Ok();
		}

		[HttpGet("SignOut")]
		public async Task<IActionResult> Get()
		{
			await _roomController.DisconnectFromRoomAsync();

			return Ok();
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] AddRoomViewModel model)
		{
			try
			{
				await _roomController.AddAsync(model);
			}
			catch (DuplicateNameException)
			{
				return BadRequest(new { Message = "Room already exists" });
			}
			catch
			{
				return StatusCode(500);
			}

			return Ok();
		}
	}
}