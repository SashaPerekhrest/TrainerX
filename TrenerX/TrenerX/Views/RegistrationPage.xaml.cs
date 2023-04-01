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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            BindingContext = new User();

            InitializeComponent();
        }

        private async void OnSaveButton_Clicked(object sender, EventArgs e)
        {
            var user = (User)BindingContext;
            user.TrainingCount = "";
            App.dataBase.UserInsert(user);
            App.dataBase.TrenersSelect();
            await Shell.Current.GoToAsync("..");
        }
    }
}