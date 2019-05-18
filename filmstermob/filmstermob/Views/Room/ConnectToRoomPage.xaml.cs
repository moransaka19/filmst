using filmstermob.Services;
using filmstermob.Views.Room;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace filmstermob.Views
{
    [DesignTimeVisible(true)]
    public partial class ConnectToRoomPage : ContentPage
    {
        Models.Room Room = null;
        public ConnectToRoomPage()
        {
            InitializeComponent();
            if (Room != null)
            {
                Task.Run(async () =>
                {
                    await Navigation.PushAsync(new RoomPage(Room));
                });
            }
        }

        async void LoginRoom_Clicked(object sender, EventArgs e)
        {
            var resp = await RoomService.RoomSignin(login.Text, password.Text);
            if (resp == System.Net.HttpStatusCode.BadGateway || resp == System.Net.HttpStatusCode.InternalServerError)
            {
                await DisplayAlert("Login", $"Smth wrong happenned...", "OK");
                error.IsVisible = true;
                error.Text = "Error...";
            }
            if (resp == System.Net.HttpStatusCode.NotFound)
            {
                await DisplayAlert("Login", $"Room not found.", "OK");
            }
            if (resp == System.Net.HttpStatusCode.Unauthorized)
            {
                await DisplayAlert("Login", $"You have been login to {login.Text} not sucessfully", "OK");
                error.IsVisible = true;
                error.Text = "Password or uniqname is incorrect!";
            }
            if (resp == System.Net.HttpStatusCode.OK || resp == System.Net.HttpStatusCode.Accepted)
            {
                CrossSettings.Current.AddOrUpdateValue("roomauth", true);
                Room = new Models.Room() { UniqName = login.Text, Password = password.Text };
                await Navigation.PushAsync(new RoomPage(Room));
            }
        }

        async void CreateRoom_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateRoomPage());
        }
    }
}