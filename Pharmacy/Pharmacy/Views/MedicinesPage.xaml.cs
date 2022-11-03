using Pharmacy.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Pharmacy.Views
{
    public partial class MedicinesPage : ContentPage
    {
        private Task searchTask;
        private short timeDelay = 1000;
        MedicinesViewModel _viewModel;

        public MedicinesPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new MedicinesViewModel();
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