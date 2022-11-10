using Pharmacy.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pharmacy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPage : ContentPage
    {
        UserViewModel viewModel;
        public UserPage()
        {
            viewModel = new UserViewModel();
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}