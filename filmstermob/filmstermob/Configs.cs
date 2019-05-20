using System;
using System.Collections.Generic;
using System.Text;

namespace filmstermob
{
    public static class Configs
    {
        public static string ServerHost { get; set; } = "http://sync4u.azurewebsites.net";
        public static string FolderForMedia { get; set; } = "TmpMedia";
        public static class TestLogin
        {
            public static string UserName { get; set; } = "admin";
            public static string Password { get; set; } = "Password1";
        }

        public static class HubEvents
        {
            public static string Room { get; set; } = "/room";
        }

        public static class Requests
        {
            public static string Login { get; set; } = "/api/Auth/Login";
            public static string RoomSignIn { get; set; } = "/api/Rooms/SignIn";
            public static string RoomCreate { get; set; } = "/api/Rooms";
            public static string RoomSignOut { get; set; } = "/api/Rooms/SignOut";
        }
    }
}
