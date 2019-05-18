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
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();

            BindingContext = this;
        }

        async void RegisterClicked(object sender, EventArgs e)
        {
            try
            {
                if (password.Text != confirmPassword.Text)
                    throw new FormatException();

                var token = await AccessTokenProvider.GetToken(login.Text, password.Text);
                CrossSettings.Current.AddOrUpdateValue("auth", token);
                await Navigation.PopModalAsync();
            }
            catch (FormatException errRegister)
            {
                error.Text = "Passwords is not same!";
                error.IsVisible = true;
            }
            catch (Exception ex)
            {
                error.Text = "Login failed!";
                error.IsVisible = true;
            }
        }

        async void CancelRegisterClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}