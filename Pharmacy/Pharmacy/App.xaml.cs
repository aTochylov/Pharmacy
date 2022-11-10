using Pharmacy.Views;
using System.IdentityModel.Tokens.Jwt;
using Xamarin.Forms;

namespace Pharmacy
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
