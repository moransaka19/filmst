using filmstermob.Services;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace filmstermob.Views.Room
{
    [DesignTimeVisible(true)]
    public partial class RoomPage : ContentPage
    {
        filmstermob.Models.Room Room;
        public RoomPage(filmstermob.Models.Room room)
        {
            InitializeComponent();
            Room = room;
        }

        async void ChatOpen_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChatPage(Room.UniqName, Room.Password));
        }

        async void SettingsOpen_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RoomDetails());
        }

        async void SignOut_Clicked(object sender, EventArgs e)
        {
            CrossSettings.Current.AddOrUpdateValue("roomauth", false);
            await RoomService.RoomSignOut();
            await Navigation.PopAsync(true);
        }
    }
}