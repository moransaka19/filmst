using PLL.Controllers;
using SharedKernel.Abstractions.PLL.Auth;
using SharedKernel.Abstractions.PLL.Messages;
using SimpleInjector;

namespace PLL.IoC
{
	public class Bootstrapper
	{
		public static void Bootstrap(Container container)
		{
			container.Register<IAuthController, AuthController>();
			container.Register<IMessageController, MessageController>();

			BLL.IoC.Bootstrapper.Bootstrap(container);
		}
	}
}
