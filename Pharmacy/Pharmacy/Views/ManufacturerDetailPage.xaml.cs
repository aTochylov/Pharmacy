using Pharmacy.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pharmacy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManufacturerDetailPage : ContentPage
    {
        public ManufacturerDetailPage()
        {
            InitializeComponent();
            BindingContext = new ManufacturerDetailViewModel();
        }
    }
}