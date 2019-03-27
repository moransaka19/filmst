using BLL;
using SharedKernel;
using System;

namespace PLL
{
	public class TestPLLController
	{
		public static void Test(string from)
		{
			Console.BackgroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine($"=> From {from} to PLLController");
			Console.BackgroundColor = ConsoleColor.Black;

			TestService.Test("PLL");
			TestSharedKernel.Test("PLL");
		}
	}
}