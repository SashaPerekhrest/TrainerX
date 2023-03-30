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

            BindingContext = new PostItemTrener();
        }

        private  void LoadTrener(string value)
        {
            try
            {
                var id = Convert.ToInt32(value);

                var trainer = App.dataBase.GetTrainer(id);

                BindingContext = trainer;
            }
            catch { }
        }

        private async void DeleteFromMyTrainers(object sender, EventArgs e)
        {
            var myTrainer = (PostItemTrener)BindingContext;
            App.myUser.TrenersID.Remove(myTrainer.ID);
            App.myUser.TrainingCount = "";
            foreach (var item in App.myUser.TrenersID)
            {
                App.myUser.TrainingCount += item + " ";
            }
            App.dataBase.UsersUpdate(App.myUser);

            App.dataBase.RequestDelete(App.dataBase.GetRequestId(myTrainer.ID, App.myUser.Id));
            App.UpdateTrainersDays();

            await Shell.Current.GoToAsync("..");
        }
    }
}