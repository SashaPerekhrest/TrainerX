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
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            BindingContext = App.myTrener;
            base.OnAppearing();
            var feedbacks = App.dataBase.GetTrenersFeedbacks(App.myTrener);
            feedbackView.ItemsSource = feedbacks;
            feedbackView.HeightRequest = 50 + feedbacks.Count * 130;
        }

        private async void UpdateTrener(object sender, EventArgs e)
        {
            var trainer = (PostItemTrener)BindingContext;
            App.dataBase.TrenersUpdate(trainer);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            App.myTrener = new PostItemTrener();
            await Shell.Current.GoToAsync(state: "//login");
        }
    }
}