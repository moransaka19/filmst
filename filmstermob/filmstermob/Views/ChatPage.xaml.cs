using filmstermob.Models;
using filmstermob.Services;
using filmstermob.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace filmstermob.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        ItemsViewModel viewModel;
        MessageHubService messageHubService;

        public ChatPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ItemsViewModel();
            messageHubService = new MessageHubService("str", "str");

            Task.Run(async () =>
            {
                await messageHubService.InitListen();
            });

            MessagingCenter.Subscribe<MessageHubService, Item>(this, "AddItem", (obj, item) =>
            {
                ItemsListView.ScrollTo(ItemsListView.ItemsSource.Cast<Item>().LastOrDefault(), ScrollToPosition.End, true);
            });
        }

        async void AddMessage_Clicked(object sender, EventArgs e)
        {
            var item = new Item()
            {
                Text = message.Text,
                Description = message.Text
            };
            await messageHubService.SendMessageAsync(message.Text);
            ItemsListView.ScrollTo(ItemsListView.ItemsSource.Cast<Item>().LastOrDefault(), ScrollToPosition.End, true);
            message.Text = string.Empty;
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

            messageHubService.StopHub();
        }
    }
}