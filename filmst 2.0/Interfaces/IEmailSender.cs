using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace filmst._0.Interfaces
{
    //TODO: Think about it;
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
