using System;
using System.Collections.Generic;
using System.Text;

namespace filmstermob
{
    public static class Configs
    {
        public static string ServerHost { get; set; } = "http://filmster.local";

        public static class TestLogin
        {
            public static string UserName { get; set; } = "admin";
            public static string Password { get; set; } = "Password1";
        }

        public static class Requests
        {
            public static string Login { get; set; } = "/api/Auth/Login";
        }
    }
}
