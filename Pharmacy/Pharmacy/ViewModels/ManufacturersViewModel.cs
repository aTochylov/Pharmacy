using Pharmacy.Models;
using Pharmacy.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace Pharmacy.ViewModels
{
    public class ManufacturersViewModel : BaseViewModel
    {
        private Manufacturer _selectedManufacturer;

        public ObservableCollection<Manufacturer> Manufacturers { get; }
        public Command LoadManufacturersCommand { get; }
        public Command AddManufacturerCommand { get; }
        public Command<Manufacturer> ManufacturerTapped { get; }

        public string Title { get; set; }

        public ManufacturersViewModel()
        {
            Title = "Browse Manufacturer";
            Manufacturers = new ObservableCollection<Manufacturer>();
            LoadManufacturersCommand = new Command(() => ExecuteLoadManufacturersCommand());

            ManufacturerTapped = new Command<Manufacturer>(OnManufacturerSelected);

            AddManufacturerCommand = new Command(OnAddManufacturer);
        }

        void ExecuteLoadManufacturersCommand()
        {
            IsBusy = true;

            try
            {
                Manufacturers.Clear();
                var manufacturers = App.ManufacturerRepo.GetItems();
                foreach (var Manufacturer in manufacturers)
                {
                    Manufacturers.Add(Manufacturer);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedManufacturer = null;
        }

        public Manufacturer SelectedManufacturer
        {
            get => _selectedManufacturer;
            set
            {
                SetProperty(ref _selectedManufacturer, value);
                OnManufacturerSelected(value);
            }
        }

        private async void OnAddManufacturer(object obj) => await Shell.Current.GoToAsync(nameof(NewManufacturerPage));

        async void OnManufacturerSelected(Manufacturer Manufacturer)
        {
            if (Manufacturer == null)
                return;
            await Shell.Current.GoToAsync($"{nameof(ManufacturerDetailPage)}?{nameof(ManufacturerDetailViewModel.ManufacturerId)}={Manufacturer.Id}");
        }

        public void OnSearchTextChanged(string query)
        {
            Manufacturers.Clear();
            var results = App.ManufacturerRepo.GetSearchResults(query);
            foreach (var r in results)
                Manufacturers.Add(r);
        }
    }
}
