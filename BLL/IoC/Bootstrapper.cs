using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IoC
{
	public class Bootstrapper
	{
		public static void Bootstrap(Container container)
		{


			DAL.IoC.Bootstrapper.Bootstrap(container);
		}
	}
}
