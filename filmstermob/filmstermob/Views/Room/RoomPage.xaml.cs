using filmstermob.Contracts;
using filmstermob.Services;
using filmstermob.ViewModels;
using InTheHand.Forms;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        ObservableCollection<FileViewModel> files;
        filmstermob.Models.Room Room;
        RoomHubService _roomHubService;
        bool _isConnecting;

        public RoomPage(filmstermob.Models.Room room, bool isConnecting)
        {
            InitializeComponent();
            _roomHubService = new RoomHubService(room.UniqName, room.Password);

            Room = room;
            files = new ObservableCollection<FileViewModel>();
            files = DependencyService.Get<IMediaService>().GetFiles();
            var tempUri = new Uri(files[0].Path);
            mediaPlayer.Source = tempUri;
            _isConnecting = isConnecting;

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

        async void MediaPlayer_CurrentStateChanged(object sender, MediaElementState e)
        {
            var t = e;
        }

        private void MediaPlayer_BindingContextChanged(object sender, EventArgs e)
        {

        }

        private void MediaPlayer_Focused(object sender, FocusEventArgs e)
        {

        }

        private void MediaPlayer_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

        }

        private void MediaPlayer_CurrentStateChanged_1(object sender, EventArgs e)
        {

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Task.Run(async () =>
            {
                await _roomHubService.InitListen();
                if (_isConnecting)
                {
                    var medias = MediaServ.GetMedia(files.Select(x => x.Path).ToList());
                    await _roomHubService.CheckMedia(medias);
                }
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            _roomHubService.StopHub();
        }
    }
}