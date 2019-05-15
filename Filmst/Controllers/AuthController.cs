using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
				return NotFound(new {Message = "User not found"});
			}
			catch (IncorrectPasswordException)
			{
				return BadRequest(new {Message = "Incorrect password"});
			}
			catch
			{
				return StatusCode(500);
			}

			return Ok(res);
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
				return BadRequest(new {e.Message});
			}
			catch
			{
				return StatusCode(500);
			}

			return Ok(res);
		}

		[Authorize]
		[HttpGet("TryLogin")]
		public IActionResult TryLogin()
		{
			return Ok();
		}
	}
}