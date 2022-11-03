using Pharmacy.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pharmacy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManufacturersPage : ContentPage
    {
        private Task searchTask;
        private short timeDelay = 1000;

        ManufacturersViewModel _viewModel;

        public ManufacturersPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ManufacturersViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchTask == null || searchTask.IsCompleted)
            {
                searchTask = Task.Run(async () =>
                {
                    await Task.Delay(timeDelay);
                    _viewModel.OnSearchTextChanged(((SearchBar)sender).Text);
                });
            }

        }
    }
}