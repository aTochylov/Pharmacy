using Pharmacy.ViewModels;
using Pharmacy.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Pharmacy
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MedicineDetailPage), typeof(MedicineDetailPage));
            Routing.RegisterRoute(nameof(NewMedicinePage), typeof(NewMedicinePage));
            Routing.RegisterRoute(nameof(ManufacturerDetailPage), typeof(ManufacturerDetailPage));
            Routing.RegisterRoute(nameof(NewManufacturerPage), typeof(NewManufacturerPage));
        }

        //private async void OnMenuItemClicked(object sender, EventArgs e)
        //{
        //    await Shell.Current.GoToAsync("..");
        //}
    }
}
