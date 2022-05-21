using Pharmacy.Models;
using Pharmacy.ViewModels;
using Pharmacy.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pharmacy.Views
{
    public partial class MedicinesPage : ContentPage
    {
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

        public void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.OnSearchTextChanged(((SearchBar)sender).Text);
        }
    }
}