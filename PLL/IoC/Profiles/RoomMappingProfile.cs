using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SharedKernel.Abstractions.BLL.DTOs.Rooms;
using SharedKernel.Abstractions.PLL.Rooms.Models;

namespace PLL.IoC.Profiles
{
	class RoomMappingProfile : Profile
	{
		public RoomMappingProfile()
		{
			CreateMap<IAddRoomViewModel, IAddRoomDTO>()
				.ForMember(r => r.HostId, opt => opt.Ignore());
		}
	}
}
