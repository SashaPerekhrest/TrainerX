using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TrenerX.Models;

namespace TrenerX.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfilePage : ContentPage
    {
        public UserProfilePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            BindingContext = App.myUser;

            base.OnAppearing();
        }

        private async void GoBackButton(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(state: "//login");
        }
        private async void UpdateButton(object sender, EventArgs e)
        {
            var user = (User)BindingContext;
            App.dataBase.UsersUpdate(user);
        }
    }
}