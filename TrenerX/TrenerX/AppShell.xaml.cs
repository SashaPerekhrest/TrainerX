using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrenerX.Views;
using TrenerX.TViews;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrenerX
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(TrainerAddingPage), typeof(TrainerAddingPage));
            Routing.RegisterRoute(nameof(DetailedTrainerPage), typeof(DetailedTrainerPage));
            Routing.RegisterRoute(nameof(MyDetailedTrainer), typeof(MyDetailedTrainer));
            Routing.RegisterRoute(nameof(EnterPage), typeof(EnterPage));
            Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
            Routing.RegisterRoute(nameof(TrenerRegistrationPage), typeof(TrenerRegistrationPage));
        }
    }
}