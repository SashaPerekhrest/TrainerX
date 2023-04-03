﻿using System;
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
            IsStudent = true;
            ModelColor = Color.FromRgb(116, 192, 68);
            BindingContext = this;
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            //App.trainersDB.Select();
            Console.WriteLine("/////////////////////" + BindingContext.ToString());

            App.dataBase.TrenersSelect();
            App.dataBase.UsersSelect();
            App.dataBase.RequestSelect();

            base.OnAppearing();
        }

        private async void GoToUserShell(object sender, EventArgs e)
        {
            var login = loginPlace.Text;
            Console.WriteLine(login);
            var password = App.dataBase.UsersLoginCheck(login);
            if (password != null && password == passwordPlace.Text)
            {
                App.myUser = App.dataBase.GetUser(login);
                App.myUser.SetTrenersID();
                Console.WriteLine(App.myUser.Id);
                App.UpdateTrainersDays();
                await Shell.Current.GoToAsync(state: "//main");
            }
        }

        private void GoButton(object sender, EventArgs e)
        {
            if (passwordPlace.Text != null && loginPlace.Text != null)
            {
                if (IsStudent)
                    GoToUserShell(sender, e);
                else
                    GoToTrenerShell(sender, e);
            }
        }

        private async void GoToTrenerShell(object sender, EventArgs e)
        {
            var login = loginPlace.Text;
            Console.WriteLine(login);
            var password = App.dataBase.TrenersLoginCheck(login);
            if (password != null && password == passwordPlace.Text)
            {
                App.myTrener = App.dataBase.GetTrainerLogin(login);
                Console.WriteLine(App.myTrener.ID);
                await Shell.Current.GoToAsync(state: "//mainTreners");
            }
        }

        private async void GoToRegistration(object sender, EventArgs e)
        {
            if (IsStudent)
                await Shell.Current.GoToAsync(nameof(RegistrationPage));
            else
                await Shell.Current.GoToAsync(nameof(TViews.TrenerRegistrationPage));
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