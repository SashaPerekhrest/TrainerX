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

            BindingContext = new PostItemTrener();
        }

        private void LoadTrener(string value)
        {
            try
            {
                var id = Convert.ToInt32(value);

                var trainer = App.dataBase.GetTrainer(id);

                BindingContext = trainer;
            }
            catch { }
        }

        private async void AddToMyTainers(object sender, EventArgs e)
        {
            var trainer = (PostItemTrener)BindingContext;
            if (!App.myUser.TrenersID.Contains(trainer.ID))
            {
                App.myUser.TrainingCount += " " + trainer.ID;
                App.dataBase.UsersUpdate(App.myUser);
                App.myUser.TrenersID.Add(trainer.ID);
                App.LoadTrainersDays();
                App.dataBase.RequestInsert( trainer.ID, App.myUser.Id, 0 );
            }
            await Shell.Current.GoToAsync("..");
        }
    }
}