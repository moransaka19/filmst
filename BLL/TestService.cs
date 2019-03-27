using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using SharedKernel;

namespace BLL
{
	public class TestService
	{
		public static void Test(string from)
		{
			Console.BackgroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine($"=> From {from} to BLL");
			Console.BackgroundColor = ConsoleColor.Black;

			TestDALClass.Test("BLL");

			TestSharedKernel.Test("BLL");
		}
	}
}
