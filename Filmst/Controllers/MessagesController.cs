using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PLL.ViewModels.Messages;
using SharedKernel.Abstractions.PLL.Messages;

namespace Filmst.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
		private readonly IMessageController _messageController;

		public MessagesController(IMessageController messageController)
	    {
			_messageController = messageController;
		}

	    [HttpPost]
	    public IActionResult Post([FromBody] AddMessageViewModel model)
	    {
		    _messageController.Add(model);

		    return NoContent();
	    }
    }
}