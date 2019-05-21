using filmstermob.Contracts;
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
    public partial class CreateRoomPage : ContentPage
    {
        public CreateRoomPage()
        {
            InitializeComponent();
        }

        async void CreateRoom_Clicked(object sender, EventArgs e)
        {
            var files = DependencyService.Get<IMediaService>().GetFiles();
            var media = MediaServ.GetMedia(files.Select(x => x.Path).ToList()).ToList();

            filmstermob.Models.Room room = new Models.Room()
            {
                Name = name.Text,
                UniqName = Uniqname.Text,
                Password = password.Text,
                Medias = media // TODO: make retriving files from selected folder in settings; If no, push page with selecting for it;
            };
            await RoomService.RoomCreate(room);
            CrossSettings.Current.AddOrUpdateValue("roomauth", true);
            await Navigation.PushAsync(new RoomPage(room, false));
        }
    }
}