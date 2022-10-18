using Pharmacy.Data;
using Pharmacy.Models;
using Pharmacy.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace Pharmacy.ViewModels
{
    public class ManufacturersViewModel : BaseViewModel
    {
        private readonly UnitOfWork Data;
        private Manufacturer _selectedManufacturer;

        public ObservableCollection<Manufacturer> Manufacturers { get; }
        public Command LoadManufacturersCommand { get; }
        public Command AddManufacturerCommand { get; }
        public Command<Manufacturer> ManufacturerTapped { get; }

        public string Title { get; set; }

        public ManufacturersViewModel()
        {
            Data = UnitOfWork.GetUnitOfWork();
            Title = "Browse Manufacturer";
            Manufacturers = new ObservableCollection<Manufacturer>();
            LoadManufacturersCommand = new Command(() => ExecuteLoadManufacturersCommand());

            ManufacturerTapped = new Command<Manufacturer>(OnManufacturerSelected);

            AddManufacturerCommand = new Command(OnAddManufacturer);
        }

        async void ExecuteLoadManufacturersCommand()
        {
            IsBusy = true;

            try
            {
                Manufacturers.Clear();
                var manufacturers = await Data.ManufacturerRepository.GetAll();
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
            await Shell.Current.GoToAsync($"{nameof(ManufacturerDetailPage)}?{nameof(ManufacturerDetailViewModel.ManufacturerId)}={Manufacturer.ManufacturerId}");
        }

        public void OnSearchTextChanged(string query)
        {
            Manufacturers.Clear();
            var results = Data.ManufacturerRepository.GetAll().Result
                .Where(i => i.Title.ToLower().Contains(query.ToLower()) 
                || i.Phone.ToLower().Contains(query.ToLower()) 
                || i.Address.ToLower().Contains(query.ToLower()) 
                || i.Email.ToLower().Contains(query.ToLower()));
            foreach (var r in results)
                Manufacturers.Add(r);
        }
    }
}
