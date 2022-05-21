using Pharmacy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pharmacy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManufacturersPage : ContentPage
    {
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

        public void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.OnSearchTextChanged(((SearchBar)sender).Text);
        }
    }
}