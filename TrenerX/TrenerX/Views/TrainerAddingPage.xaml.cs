using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TrenerX.Models;

namespace TrenerX.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class TrainerAddingPage : ContentPage
    {
        public string ItemId
        {
            set
            {
                LoadTrener(value);
            }
        }

        public TrainerAddingPage()
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

        private async void OnSaveButton_Clicked(object sender, EventArgs e)
        {
            var trainer = (ItemTrainer)BindingContext;

            if (!string.IsNullOrWhiteSpace(trainer.FullName) &&
                !string.IsNullOrWhiteSpace(trainer.Education))
            {
                trainer.IsMine = false;
                await App.TrainerDB.SaveTrainerAsync(trainer);
            }

            await Shell.Current.GoToAsync("..");
        }

        private async void OnDeleteButton_Clicked(object sender, EventArgs e)
        {
            var trainer = (ItemTrainer)BindingContext;

            await App.TrainerDB.DeleteTrainerAsync(trainer);

            await Shell.Current.GoToAsync("..");
        }
    }
}