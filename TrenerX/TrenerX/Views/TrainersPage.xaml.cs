using System;
using System.Linq;
using Xamarin.Forms;
using TrenerX.Models;

namespace TrenerX.Views
{
    public partial class TrainersPage : ContentPage
    {
        public TrainersPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            collectionView.ItemsSource = await App.TrainerDB.GetTrainersAsync();

            base.OnAppearing();
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(TrainerAddingPage));
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                ItemTrainer trainer = (ItemTrainer)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync(
                    $"{nameof(TrainerAddingPage)}?{nameof(TrainerAddingPage.ItemId)}={trainer.ID.ToString()}");
            }
        }

        private async void GetDetailed(object sender, EventArgs e)
        {
            var trainerID = ((Button)sender).ClassId.ToString();
            await Shell.Current.GoToAsync(
                     $"{nameof(DetailedTrainerPage)}?{nameof(DetailedTrainerPage.ItemId)}={trainerID}");
        }
    }
}