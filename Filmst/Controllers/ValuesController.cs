using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Microsoft.AspNetCore.Mvc;
using PLL;

namespace Filmst.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		// GET api/values/5
		[HttpGet]
		public ActionResult<string> Get()
		{
			Console.BackgroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine("=> From Controller");
			Console.BackgroundColor = ConsoleColor.Black;

			TestService.Test("Controller");
			TestPLLController.Test("Controller");

			return Ok();
		}
	}
}
