using PLL.Controllers;
using SharedKernel.Abstractions.PLL.Auth;
using SimpleInjector;

namespace PLL.IoC
{
	public class Bootstrapper
	{
		public static void Bootstrap(Container container)
		{
			container.Register<IAuthController, AuthController>();

			BLL.IoC.Bootstrapper.Bootstrap(container);
		}
	}
}
