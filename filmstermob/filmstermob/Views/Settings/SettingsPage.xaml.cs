using filmstermob.Contracts;
using filmstermob.Models;
using filmstermob.ViewModels;
using filmstermob.Views.Settings;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace filmstermob.Views
{
    [DesignTimeVisible(true)]
    public partial class SettingsPage : ContentPage
    {
        ObservableCollection<FileViewModel> Medias;
        ItemsViewModel viewModel;
        public SettingsPage()
        {
            InitializeComponent();
            Medias = DependencyService.Get<IMediaService>().GetFiles();
            viewModel = new ItemsViewModel();
            foreach (var item in Medias)
            {
                viewModel.Items.Add(new Models.Item() { Text = item.Name, Description = item.Path });
            }

            BindingContext = viewModel;

            MessagingCenter.Subscribe<App, ObservableCollection<string>>((App)Xamarin.Forms.Application.Current, "MediasSelected", (s, images) =>
            {
                RefreshMedias();
            });
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;

            await Navigation.PushAsync(new FileEditPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }


        void RefreshMedias()
        {
            Medias = DependencyService.Get<IMediaService>().GetFiles();
            ObservableCollection<Item> refreshItems = new ObservableCollection<Item>();
            foreach (var item in Medias)
            {
                refreshItems.Add(new Models.Item() { Text = item.Name, Description = item.Path });
            }
            MessagingCenter.Send(this, "AddItem", refreshItems);
            viewModel.LoadItemsCommand.Execute(null);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel = new ItemsViewModel();
            //MessagingCenter.Unsubscribe<App, List<string>>(this, "MediaSelected");
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            await DependencyService.Get<IMediaService>().OpenMedia();
        }
    }
}