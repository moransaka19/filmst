using filmstermob.Providers;
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
    public partial class AuthPage : ContentPage
    {
        public AuthPage()
        {
            InitializeComponent();

            BindingContext = this;
        }

        async void Login_Clicked(object sender, EventArgs e)
        {
            try
            {
                var token = await AccessTokenProvider.GetToken(login.Text, password.Text);
                CrossSettings.Current.AddOrUpdateValue("auth", token);
                CrossSettings.Current.AddOrUpdateValue("userCreds", $"{login.Text} {password.Text}");
                await Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                error.Text = "Login failed!";
                error.IsVisible = true;
            }
        }

        async void RegisterClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new RegisterPage()), true);
        }
    }
}