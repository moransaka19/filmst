using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
