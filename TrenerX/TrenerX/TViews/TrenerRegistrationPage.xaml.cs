using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TrenerX.Models;

namespace TrenerX.TViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrenerRegistrationPage : ContentPage
    {
        public TrenerRegistrationPage()
        {
            BindingContext = new PostItemTrener();
            InitializeComponent();
        }

        private async void OnSaveButton_Clicked(object sender, EventArgs e)
        {
            var trainer = (PostItemTrener)BindingContext;
            App.dataBase.TrenersInsert(trainer);
            App.dataBase.TrenersSelect();
            await Shell.Current.GoToAsync("..");
        }
    }
}