using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Filmst.Controllers;
using Microsoft.AspNetCore.SignalR;
using SharedKernel.Abstractions.PLL.Rooms;

namespace Filmst.IoC
{
	public class Bootstrapper
	{
		public static void Bootstrap(Container container)
		{
			PLL.IoC.Bootstrapper.Bootstrap(container);
		}
	}
}
