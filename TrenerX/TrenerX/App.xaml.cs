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
        static TrainerDB trainerDB;

        public static TrainerDB TrainerDB
        {
            get
            {
                if (trainerDB == null)
                    trainerDB = new TrainerDB(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "TrainersDatabase.db3"));
                return trainerDB;
            }
        }

        static TrainerDB myTrainerDB;

        public static TrainerDB MyTrainerDB
        {
            get
            {
                if (myTrainerDB == null)
                    myTrainerDB = new TrainerDB(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "MyTrainersDatabase.db3"));
                return myTrainerDB;
            }
        }

        public static ItemTrainer[] trainersDays;

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            trainersDays = new ItemTrainer[7];
            LoadTrainersDays();
        }

        public static async void LoadTrainersDays()
        {
            var trainers = await MyTrainerDB.GetTrainersAsync();
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

        public static void UpdateTrainersDays(string[] days)
        {
            foreach (var day in days)
            {
                switch (day)
                {
                    case "пн":
                        trainersDays[0] = null;
                        break;
                    case "вт":
                        trainersDays[1] = null;
                        break;
                    case "ср":
                        trainersDays[2] = null;
                        break;
                    case "чт":
                        trainersDays[3] = null;
                        break;
                    case "пт":
                        trainersDays[4] = null;
                        break;
                    case "сб":
                        trainersDays[5] = null;
                        break;
                    case "вс":
                        trainersDays[6] = null;
                        break;
                }
            }
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
