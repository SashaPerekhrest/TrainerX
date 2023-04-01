using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrenerX.Views
{
    public partial class TimetablePage : ContentPage
    {
        public TimetablePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            App.LoadTrainersDays();
            labalFullName_1.Text = App.trainersDays[0] == null ? "Пустой день": App.trainersDays[0].FullName;
            labalFullName_2.Text = App.trainersDays[1] == null ? "Пустой день" : App.trainersDays[1].FullName;
            labalFullName_3.Text = App.trainersDays[2] == null ? "Пустой день" : App.trainersDays[2].FullName;
            labalFullName_4.Text = App.trainersDays[3] == null ? "Пустой день" : App.trainersDays[3].FullName;
            labalFullName_5.Text = App.trainersDays[4] == null ? "Пустой день" : App.trainersDays[4].FullName;
            labalFullName_6.Text = App.trainersDays[5] == null ? "Пустой день" : App.trainersDays[5].FullName;
            labalFullName_7.Text = App.trainersDays[6] == null ? "Пустой день" : App.trainersDays[6].FullName;

            labalDir_1.Text = App.trainersDays[0] == null ? "" : App.trainersDays[0].DirOfTraining;
            labalDir_2.Text = App.trainersDays[1] == null ? "" : App.trainersDays[1].DirOfTraining;
            labalDir_3.Text = App.trainersDays[2] == null ? "" : App.trainersDays[2].DirOfTraining;
            labalDir_4.Text = App.trainersDays[3] == null ? "" : App.trainersDays[3].DirOfTraining;
            labalDir_5.Text = App.trainersDays[4] == null ? "" : App.trainersDays[4].DirOfTraining;
            labalDir_6.Text = App.trainersDays[5] == null ? "" : App.trainersDays[5].DirOfTraining;
            labalDir_7.Text = App.trainersDays[6] == null ? "" : App.trainersDays[6].DirOfTraining;

            base.OnAppearing();
        }
    }
}