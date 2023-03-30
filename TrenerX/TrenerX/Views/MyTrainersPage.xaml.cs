using System;
using System.Linq;
using Xamarin.Forms;
using TrenerX.Models;

namespace TrenerX.Views
{
    public partial class MyTrainersPage : ContentPage
    {
        public MyTrainersPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            collectionView.ItemsSource = App.dataBase.GetMyTrainersR(App.myUser);
    
            base.OnAppearing();
        }

        private async void GetDetailed(object sender, EventArgs e)
        {
            var trainerID = ((Button)sender).ClassId.ToString();
            await Shell.Current.GoToAsync(
                     $"{nameof(MyDetailedTrainer)}?{nameof(MyDetailedTrainer.ItemId)}={trainerID}");
        }
    }
}