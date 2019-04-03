using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using SimpleInjector;

namespace Filmst.IoC
{
	public class MapperBootstrapper
	{
		public static void Bootstrap(MapperConfigurationExpression cfg)
		{
			

			BLL.IoC.MapperBootstrapper.Bootstrap(cfg);
		}
	}
}
