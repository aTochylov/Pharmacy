using Pharmacy.ViewModels;
using System.Globalization;
using Xamarin.Forms;

namespace Pharmacy.Views
{
    public partial class MedicineDetailPage : ContentPage
    {

        public MedicineDetailPage()
        {
            CultureInfo myCurrency = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = myCurrency;

            InitializeComponent();
            BindingContext = new MedicineDetailViewModel();

        }
    }
}