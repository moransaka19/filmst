using AutoMapper.Configuration;

namespace PLL.IoC
{
	public class MapperBootstrapper
	{
		public static void Bootstrap(MapperConfigurationExpression cfg)
		{

			BLL.IoC.MapperBootstrapper.Bootstrap(cfg);
		}
	}
}
