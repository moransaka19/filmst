using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserValidation.Models
{
    public class User
    {
        string userEmail { get; set; }
        string userPassword { get; set; }

        public User(string email, string password)
        {
            userEmail = email;
            userPassword = password;
        }
    }
}
