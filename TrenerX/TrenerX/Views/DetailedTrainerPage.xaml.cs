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
            feedbackView.ItemsSource = null;
        }

        private void LoadTrener(string value)
        {
            try
            {
                var id = Convert.ToInt32(value);

                var trainer = App.dataBase.GetTrainer(id);

                BindingContext = trainer;
                var feedbacks = App.dataBase.GetTrenersFeedbacks(trainer);
                feedbackView.ItemsSource = feedbacks;
                feedbackView.HeightRequest =50 + feedbacks.Count * 130;
            }
            catch { }
        }

        private async void AddToMyTainers(object sender, EventArgs e)
        {
            var trainer = (PostItemTrener)BindingContext;
            if (App.dataBase.GetRequest(trainer.ID, App.myUser.Id) == null)
            {
                App.dataBase.RequestInsert(trainer.ID, App.myUser.Id, 0);
                App.LoadTrainersDays();
            }
            await Shell.Current.GoToAsync("..");
        }
    }
}