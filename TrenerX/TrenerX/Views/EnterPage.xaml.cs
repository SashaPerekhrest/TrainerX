using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrenerX.Views
{
    public partial class EnterPage : ContentPage
    {
        public EnterPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var login = loginPlace.Text;
            Console.WriteLine(login);
            var password = App.usersDB.LoginCheck(login);
            if (login != null && password != null && password == passwordPlace.Text)
            {
                App.myUser = App.usersDB.GetUser(login);
                App.myUser.SetTrenersID();
                Console.WriteLine(App.myUser.Id);
                App.LoadTrainersDays();
                await Shell.Current.GoToAsync(state: "//main");
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(RegistrationPage));
        }
    }
}