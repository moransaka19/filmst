using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SharedKernel.Abstractions.BLL.DTOs.Messages;
using SharedKernel.Abstractions.BLL.Services;
using SharedKernel.Abstractions.PLL.Messages;
using SharedKernel.Abstractions.PLL.Messages.Models;

namespace PLL.Controllers
{
	public class MessageController : IMessageController
	{
		private readonly IMessageService _messageService;

		public MessageController(IMessageService messageService)
		{
			_messageService = messageService;
		}

		public void Add(IAddMessageViewModel model)
		{
			_messageService.Add(Mapper.Map<IMessageDTO>(model));
		}
	}
}
