using filmstermob.Contracts;
using filmstermob.Models;
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
        ItemsViewModel playList;
        filmstermob.Models.Room Room;
        RoomHubService _roomHubService;
        bool _isConnecting;

        public RoomPage(filmstermob.Models.Room room, bool isConnecting)
        {
            InitializeComponent();
            _roomHubService = new RoomHubService(room.UniqName, room.Password);

            playList.LoadItemsCommand.Execute(null);

            BindingContext = playList = new ItemsViewModel();
            files = DependencyService.Get<IMediaService>().GetFiles();
            Room = room;

            foreach (var file in files)
            {
                MessagingCenter.Send(this, "AddItem", new Item() { Description = file.Path, Text = file.Name });
            }

            if (playList.Items.Count > 0)
            {
                var tempUri = new Uri(playList.Items.First().Description);
                mediaPlayer.Source = tempUri;
            }
        }
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            

            if (playList.Items.Count == 0)
                playList.LoadItemsCommand.Execute(null);
        }

        async void Play_Clicked(object sender, EventArgs e)
        {
            StateOfPlayer.Text = "Playing";
            mediaPlayer.Play();
        }

        async void Stop_Clicked(object sender, EventArgs e)
        {
            StateOfPlayer.Text = "Stopped";
            mediaPlayer.Stop();
        }

        async void MediaSelected_Clicked(object sender, SelectedItemChangedEventArgs e)
        {
            StateOfPlayer.Text = "Stopped";
            mediaPlayer.Stop();
            var item = e.SelectedItem as Item;
            if (item == null)
                return;

            var tempUri = new Uri(item.Description);
            mediaPlayer.Source = tempUri;
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

        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {

        }
    }
}