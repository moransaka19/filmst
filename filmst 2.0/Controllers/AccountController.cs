using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using filmst._0.Data;
using filmst._0.Models;
using filmst._0.Services;
using Microsoft.AspNetCore.Authorization;

namespace filmst.Controllers
{
    //TODO: Think about refactor it
    //TODO: Add logger to each instanse of acc mang
	[Route("[controller]/[action]")]
	public class AccountController : Controller //TODO: Check it/
	{
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly ILogger _logger;
		private readonly UserManager<ApplicationUser> _userManager;

		public AccountController(SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger, UserManager<ApplicationUser> userManager)
		{
			_signInManager = signInManager;
			_logger = logger;
			_userManager = userManager;
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Login([FromBody] RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByNameAsync(model.Email);
				var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

				if (result.Succeeded)
				{
					return Ok();
				}

				//TODO: Need cath error case 
			}
			return BadRequest();
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
				var result = await _userManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					await _signInManager.SignInAsync(user, isPersistent: false);
				}

				//TODO: Need cath error case 
				return Ok();
			}

			return BadRequest();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			_logger.LogInformation("User logged out.");
			return RedirectToPage("/Index");
		}
	}
}
