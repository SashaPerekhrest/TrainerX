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
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            BindingContext = App.myTrener;
            InitializeComponent();
        }

        private async void UpdateTrener(object sender, EventArgs e)
        {
            var trainer = (PostItemTrener)BindingContext;
            App.dataBase.TrenersUpdate(trainer);
        }
    }
}