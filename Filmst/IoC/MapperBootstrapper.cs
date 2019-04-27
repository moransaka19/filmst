using AutoMapper.Configuration;

namespace Filmst.IoC
{
	public class MapperBootstrapper
	{
		public static void Bootstrap(MapperConfigurationExpression cfg)
		{
			

			PLL.IoC.MapperBootstrapper.Bootstrap(cfg);
		}
	}
}
