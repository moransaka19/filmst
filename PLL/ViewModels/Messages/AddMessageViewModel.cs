﻿using SharedKernel.Abstractions.PLL.Messages.Models;

namespace PLL.ViewModels.Messages
{
	public class AddMessageViewModel : IAddMessageViewModel
	{
		public long UserId { get; set; }
		public long RoomId { get; set; }
		public string Message { get; set; }
	}
}
