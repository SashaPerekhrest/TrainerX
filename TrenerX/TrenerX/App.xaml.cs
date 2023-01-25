using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TrenerX.Data;
using TrenerX.Models;
using System.IO;

namespace TrenerX
{
    public partial class App : Application
    {
        public static PostgreSQLdb trainersDB;
        public static PostUsersDB usersDB;
        public static User myUser;

        public static PostItemTrener[] trainersDays;

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            trainersDB = new PostgreSQLdb();
            usersDB = new PostUsersDB();
            usersDB.Select();
            trainersDB.Select();
            
            trainersDays = new PostItemTrener[7];
            LoadTrainersDays();
        }

        public static void LoadTrainersDays()
        {
            var trainers = trainersDB.GetMyTrainers(myUser);
            foreach (var trainer in trainers)
            {
                var days = trainer.TrainingCount.Split(' ');
                foreach (var day in days)
                {
                    switch (day)
                    {
                        case "пн":
                            trainersDays[0] = trainer;
                            break;
                        case "вт":
                            trainersDays[1] = trainer;
                            break;
                        case "ср":
                            trainersDays[2] = trainer;
                            break;
                        case "чт":
                            trainersDays[3] = trainer;
                            break;
                        case "пт":
                            trainersDays[4] = trainer;
                            break;
                        case "сб":
                            trainersDays[5] = trainer;
                            break;
                        case "вс":
                            trainersDays[6] = trainer;
                            break;
                    }
                }
            }
        }

        public static void UpdateTrainersDays()
        {
            trainersDays = new PostItemTrener[7];
            LoadTrainersDays();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
