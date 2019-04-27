using SimpleInjector;
using SharedKernel.Abstractions.BLL.Services;
using BLL.Services;

namespace BLL.IoC
{
	public class Bootstrapper
	{
		public static void Bootstrap(Container container)
		{
			container.Register<ITokenService, TokenService>();
			container.Register<IAuthService, AuthService>();

			DAL.IoC.Bootstrapper.Bootstrap(container);
		}
	}
}
