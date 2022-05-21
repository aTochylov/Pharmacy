using Pharmacy.Models;
using Pharmacy.ViewModels;
using System.Globalization;
using Xamarin.Forms;

namespace Pharmacy.Views
{
    public partial class NewMedicinePage : ContentPage
    {
        public Medicine Medicine { get; set; }

        public NewMedicinePage()
        {
            CultureInfo myCurrency = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = myCurrency;
            InitializeComponent();
            BindingContext = new NewMedicineViewModel();
        }
    }
}