using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PLL.ViewModels.Rooms;
using SharedKernel.Abstractions.PLL.Rooms;

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

		[HttpPost("{name}")]
		public async Task<IActionResult> Get([FromBody] RoomViewModel model)
		{


			return Ok();
		}

	    [HttpPost]
	    public async Task<IActionResult> Post([FromBody] AddRoomViewModel model)
	    {
		    await _roomController.AddAsync(model);

		    return NoContent();
	    }

	    [HttpPut]
	    public async Task<IActionResult> Put([FromBody] UpdateRoomViewModel model)
	    {
		    await _roomController.UpdateAsync(model);

		    return NoContent();
	    }
    }
}