using System;
using System.Collections.Generic;
using System.Text;

namespace filmstermob.Models
{
    public enum MenuItemType
    {
        Browse,
        About,
        Settings,
        LogOut
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
