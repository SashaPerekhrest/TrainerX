using System;
using Xamarin.Forms;
using TrenerX.Models;

namespace TrenerX.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class MyDetailedTrainer : ContentPage
    {
        public string ItemId
        {
            set
            {
                LoadTrener(value);
            }
        }

        public MyDetailedTrainer()
        {
            InitializeComponent();

            BindingContext = new ItemTrainer();
        }

        private async void LoadTrener(string value)
        {
            try
            {
                var id = Convert.ToInt32(value);

                var trainer = await App.MyTrainerDB.GetTrainerAsync(id);

                BindingContext = trainer;
            }
            catch { }
        }

        private async void DeleteFromMyTrainers(object sender, EventArgs e)
        {
            var myTrainer = (ItemTrainer)BindingContext;
            var trainer = await App.TrainerDB.GetTrainerAsync(myTrainer.PrevID);

            try 
            {
                trainer.IsMine = false;
                await App.TrainerDB.SaveTrainerAsync(trainer);
            }
            catch { }

            await App.MyTrainerDB.DeleteTrainerAsync(myTrainer);
            App.UpdateTrainersDays(myTrainer.TrainingCount.Split(' '));
            await Shell.Current.GoToAsync("..");
        }
    }
}