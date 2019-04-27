using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Entities;
using SharedKernel.Abstractions.BLL.DTOs.Messages;
using SharedKernel.Abstractions.BLL.Services;
using SharedKernel.Abstractions.DAL.Repositories;

namespace BLL.Services
{
	public class MessageService : IMessageService
	{
		private readonly IRepository<Message> _messageRepository;

		public MessageService(IRepository<Message> messageRepository)
		{
			_messageRepository = messageRepository;
		}

		public void Add(IMessageDTO dto)
		{
			_messageRepository.Add(Mapper.Map<Message>(dto));
		}
	}
}
