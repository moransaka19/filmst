using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PLL.ViewModels.Messages;
using SharedKernel.Abstractions.PLL.Messages;

namespace Filmst.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
	[Authorize]
    public class MessagesController : ControllerBase
    {
		private readonly IMessageController _messageController;

		public MessagesController(IMessageController messageController)
	    {
			_messageController = messageController;
		}

	    [HttpPost]
	    public async Task<IActionResult> Post([FromBody] AddMessageViewModel model)
	    {
		    await _messageController.AddAsync(model);

		    return NoContent();
	    }
    }
}