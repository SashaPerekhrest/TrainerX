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
        public Color ModelColor { get; set; }
        private bool IsStudent { get; set; }
        public EnterPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            //App.trainersDB.Select();
            ModelColor = Color.FromRgb(116, 192, 68);
            BindingContext = this;
            Console.WriteLine("/////////////////////" + BindingContext.ToString());

            App.dataBase.TrenersSelect();
            App.dataBase.UsersSelect();
            App.dataBase.RequestSelect();

            IsStudent = true;

            base.OnAppearing();
        }

        private async void GoToUserShell(object sender, EventArgs e)
        {
            var login = loginPlace.Text;
            Console.WriteLine(login);
            var password = App.dataBase.UsersLoginCheck(login);
            if (login != null && password != null && password == passwordPlace.Text)
            {
                App.myUser = App.dataBase.GetUser(login);
                App.myUser.SetTrenersID();
                Console.WriteLine(App.myUser.Id);
                App.LoadTrainersDays();
                await Shell.Current.GoToAsync(state: "//main");
            }
        }

        private async void GoButton(object sender, EventArgs e)
        {
            if(IsStudent)
                GoToUserShell(sender, e);
            else
                GoToTrenerShell(sender, e);
        }

        private async void GoToTrenerShell(object sender, EventArgs e)
        {
            var login = loginPlace.Text;
            Console.WriteLine(login);
            var password = App.dataBase.TrenersLoginCheck(login);
            if (login != null && password != null && password == passwordPlace.Text)
            {
                App.myTrener = App.dataBase.GetTrainerLogin(login);
                //App.myUser.SetTrenersID();
                Console.WriteLine(App.myTrener.ID);
                //App.LoadTrainersDays();
                await Shell.Current.GoToAsync(state: "//mainTreners");
            }
        }

        private async void GoToRegistration(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(RegistrationPage));
        }

        private void ChangeOfDirection(object sender, EventArgs e)
        {
            Device.StartTimer(TimeSpan.FromSeconds(0.25), () =>
            {
                var button = (Button)sender;
                button.Text = IsStudent ? "Я - ученик":"Я - тренер";
                ModelColor = IsStudent ? Color.FromRgb(255, 168, 18) : Color.FromRgb(116, 192, 68);
                BindingContext = null;
                BindingContext = this;
                IsStudent = !IsStudent;
                Console.WriteLine(IsStudent +"/////////////////////" + BindingContext.ToString());
                return false;
            });
        }
    }
}