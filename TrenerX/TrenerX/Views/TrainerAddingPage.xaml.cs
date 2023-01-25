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

            BindingContext = new PostItemTrener();
        }

        private async void LoadTrener(string value)
        {
            try
            {
                var id = Convert.ToInt32(value);

                var trainer = App.trainersDB.GetTrainer(id);

                BindingContext = trainer;
            }
            catch { }
        }

        private async void OnSaveButton_Clicked(object sender, EventArgs e)
        {
            var trainer = (PostItemTrener)BindingContext;

            if (trainer.ID != 0)
                App.trainersDB.Update(trainer);
            else
                App.trainersDB.Insert(trainer);
            await Shell.Current.GoToAsync("..");
        }

        private async void OnDeleteButton_Clicked(object sender, EventArgs e)
        {
            var trainer = (PostItemTrener)BindingContext;

            Console.WriteLine("--------");
            Console.WriteLine(trainer.ID);
            App.trainersDB.Delete(trainer);

            await Shell.Current.GoToAsync("..");
        }
    }
}