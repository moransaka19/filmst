using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
	public class TestDALClass
	{
		public static void Test(string from)
		{
			Console.BackgroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine($"=> From {from} to DAl");
			Console.BackgroundColor = ConsoleColor.Black;

			TestSharedKernel.Test("DAL");
		}
	}
}
