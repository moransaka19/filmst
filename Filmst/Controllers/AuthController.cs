using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PLL.VIewModels.Auth;
using SharedKernel.Abstractions.PLL.Auth;
using SharedKernel.Exceptions;

namespace Filmst.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthController _authController;

		public AuthController(IAuthController authController)
		{
			_authController = authController;
		}

		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody] LoginViewModel model)
		{
			object res;

			try
			{
				res = await _authController.LoginAsync(model);
			}
			catch (UserNotFoundException)
			{
				return NotFound(new { Ok = false, ErrorMessage = "User not found" });
			}
			catch (IncorrectPasswordException)
			{
				return BadRequest(new { Ok = false, ErrorMessage = "Incorrect password" });
			}

			return Ok(new { Ok = true, Result = res });
		}

		[HttpPost("Register")]
		public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
		{

			object res;

			try
			{
				res = await _authController.RegisterAsync(model);
			}
			catch (UserNotRegisteredException e)
			{
				return BadRequest(new { Ok = false, ErrorMessage = e.Message });
			}

			return Ok(new { Ok = true, Result = res });
		}
	}
}