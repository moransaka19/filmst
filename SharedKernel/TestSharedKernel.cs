using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel
{
	public class TestSharedKernel
	{
		public static void Test(string from)
		{
			Console.BackgroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine($"=> From {from} to SharedKernel");
			Console.BackgroundColor = ConsoleColor.Black;
		}
	}
}
