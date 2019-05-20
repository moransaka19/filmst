using filmstermob.Models;
using filmstermob.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace filmstermob.Views.Settings
{
    [DesignTimeVisible(true)]
    public partial class FileEditPage : ContentPage
    {
        ItemDetailViewModel viewModel;
        public FileEditPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }
        public FileEditPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Text = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }

        async void Delete_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Alert", "Would you like to remove this file?", "Yes", "No");
            if (answer)
            {
                if (File.Exists(viewModel.Item.Description))
                {
                    File.Delete(viewModel.Item.Description);
                    MessagingCenter.Send(this, "DeleteItem", viewModel.Item);
                    await Navigation.PopAsync(true);
                }
            }
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(true);
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(true);
        }
    }
}