using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Entities;
using SharedKernel.Abstractions.BLL.DTOs.Rooms;
using SharedKernel.Extensions;

namespace BLL.IoC.Profiles
{
	class RoomMappingProfile : Profile
	{
		public RoomMappingProfile()
		{
			CreateMap<IAddRoomDTO, Room>()
				   .ForMember(r => r.PasswordHash, opt => opt.MapFrom(dto => dto.Password.GetHashCode()));

			CreateMap<IUpdateRoomDTO, Room>()
				.ForMember(r => r.PasswordHash, opt => opt.MapFrom(dto => dto.Password.GetHashCode()))
				.ForMember(r => r.UserRooms, opt => opt.MapFrom(dto =>
					dto.UserIds.Select(uid => new UserRoom() { UserId = uid, RoomId = dto.Id })))

				.ForMember(r => r.UserRooms, opt => opt.PreCondition(dto => !dto.UserIds.IsNullOrEmpty()))
				.ForMember(r => r.PasswordHash, opt => opt.Condition(dto => dto.Password != default))
				.ForMember(r => r.UniqName, opt => opt.Condition(dto => dto.UniqName != default))
				.ForMember(r => r.Name, opt => opt.Condition(dto => dto.Name != default))
				.ForMember(r => r.PlayListId, opt => opt.Condition(dto => dto.PlayListId != default));
		}
	}
}
