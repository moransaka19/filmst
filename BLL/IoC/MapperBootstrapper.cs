using AutoMapper.Configuration;
using DAL.Entities;
using SharedKernel.Abstractions.BLL.DTOs.Messages;

namespace BLL.IoC
{
	public class MapperBootstrapper
	{
		public static void Bootstrap(MapperConfigurationExpression cfg)
		{
			cfg.CreateMap<IMessageDTO, Message>()
				.ForMember(m => m.Room, opt => opt.Ignore())
				.ForMember(m => m.User, opt => opt.Ignore())
				
				// TODO: Get hash messahe
				.ForMember(m => m.HashMessage, 
					opt => opt.MapFrom(dto => dto.Message));

			DAL.IoC.MapperBootstrapper.Bootstrap(cfg);
		}
	}
}
