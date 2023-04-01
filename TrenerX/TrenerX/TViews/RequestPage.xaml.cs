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
    public partial class RequestPage : ContentPage
    {
        public RequestPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            collectionView.ItemsSource = App.dataBase.GetMyUsersR(App.myTrener);

            base.OnAppearing();
        }

        private void Accept(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var request = App.dataBase.GetRequest(App.myTrener.ID, Convert.ToInt32(button.ClassId));
            request.Confirmation = 1;
            App.dataBase.RequestUpdate(request);

        }

        private void Reject(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var request = App.dataBase.GetRequest(App.myTrener.ID, Convert.ToInt32(button.ClassId));
            App.dataBase.RequestDelete(request);
        }
    }
}