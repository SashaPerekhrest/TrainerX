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
            //App.trainersDB.Select();
            collectionView.ItemsSource = App.trainersDB.treners;

            Console.WriteLine("/////////////////////////////////////");
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
                var trainer = (PostItemTrener)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync(
                    $"{nameof(TrainerAddingPage)}?{nameof(TrainerAddingPage.ItemId)}={trainer.ID}");
            }
        }

        private async void GetDetailed(object sender, EventArgs e)
        {
            var trainerID = ((Button)sender).ClassId.ToString();
            await Shell.Current.GoToAsync(
                     $"{nameof(DetailedTrainerPage)}?{nameof(DetailedTrainerPage.ItemId)}={trainerID}");
        }

        private void GetSportCategory(object sender, EventArgs e)
            => collectionView.ItemsSource = App.trainersDB.GetTrainersAtCategory(1);

        private void GetCyberSportCategory(object sender, EventArgs e)
            => collectionView.ItemsSource = App.trainersDB.GetTrainersAtCategory(2);
        private void GetEducationCategory(object sender, EventArgs e)
            => collectionView.ItemsSource = App.trainersDB.GetTrainersAtCategory(3);
    }
}