using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PLL.ViewModels.Rooms;
using SharedKernel.Abstractions.PLL.Rooms;
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

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] AddRoomViewModel model)
		{
			try
			{
				await _roomController.AddAsync(model);
			}
			catch
			{
				return StatusCode(500);
			}

			return NoContent();
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] UpdateRoomViewModel model)
		{
			try
			{
				await _roomController.UpdateAsync(model);
			}
			catch (UserIsNotHostException)
			{
				return Forbid();
			}
			catch
			{
				return StatusCode(500);
			}

			return NoContent();
		}
	}
}