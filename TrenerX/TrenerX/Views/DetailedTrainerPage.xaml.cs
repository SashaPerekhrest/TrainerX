using System;
using Xamarin.Forms;
using TrenerX.Models;

namespace TrenerX.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class DetailedTrainerPage : ContentPage
    {
        public string ItemId
        {
            set
            {
                LoadTrener(value);
            }
        }

        public DetailedTrainerPage()
        {
            InitializeComponent();

            BindingContext = new ItemTrainer();
        }

        private async void LoadTrener(string value)
        {
            try
            {
                var id = Convert.ToInt32(value);

                var trainer = await App.TrainerDB.GetTrainerAsync(id);

                BindingContext = trainer;
            }
            catch { }
        }

        private async void AddToMyTainers(object sender, EventArgs e)
        {
            var trainer = (ItemTrainer)BindingContext;
            if (!trainer.IsMine)
            {
                trainer.IsMine = true;
                await App.TrainerDB.SaveTrainerAsync(trainer);

                var newTrainer = new ItemTrainer
                {
                    DirOfTraining = trainer.DirOfTraining,
                    Education = trainer.Education,
                    FullName = trainer.FullName,
                    Image = trainer.Image,
                    TrainingCount = trainer.TrainingCount,
                    Requirements = trainer.Requirements,
                    PrevID = trainer.ID
                };
                await App.MyTrainerDB.SaveTrainerAsync(newTrainer);
                App.LoadTrainersDays();
            }
            await Shell.Current.GoToAsync("..");
        }
    }
}