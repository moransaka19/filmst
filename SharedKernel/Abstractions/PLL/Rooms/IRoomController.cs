﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel.Abstractions.PLL.Rooms.Models;

namespace SharedKernel.Abstractions.PLL.Rooms
{
	public interface IRoomController
	{
		Task AddAsync(IAddRoomViewModel model);
		Task UpdateAsync(IUpdateRoomViewModel model);
		Task SignInAsync(ISignInRoomViewModel model);
		string GetRoomName();
		void DisconnectFromRoom();
	}
}