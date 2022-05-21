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
    public partial class NewManufacturerPage : ContentPage
    {
        public NewManufacturerPage()
        {
            InitializeComponent();
            BindingContext = new NewManufacturerViewModel();
        }
    }
}