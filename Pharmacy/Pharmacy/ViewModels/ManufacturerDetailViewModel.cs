using Pharmacy.Models;
using System;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace Pharmacy.ViewModels
{
    [QueryProperty(nameof(ManufacturerId), nameof(ManufacturerId))]
    public class ManufacturerDetailViewModel : BaseViewModel
    {
        private int manufacturerId;
        private string title;
        private string address;
        private string phone;
        private string email;

        public string PageTitle { get; set; }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public string Address
        {
            get => address;
            set => SetProperty(ref address, value);
        }

        public string Phone
        {
            get => phone;
            set => SetProperty(ref phone, value);
        }
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        public int ManufacturerId
        {
            get => manufacturerId;
            set
            {
                manufacturerId = value;
                LoadManufacturerId(value);
            }
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command DeleteCommand { get; }

        public ManufacturerDetailViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            DeleteCommand = new Command(OnDelete);
        }

        private bool ValidateSave()
        {
            var items = App.ManufacturerRepo.GetItems();
            return !String.IsNullOrWhiteSpace(title)
                && !String.IsNullOrWhiteSpace(phone)
                && !String.IsNullOrWhiteSpace(email)
                && !items.Any(m => (m.Id != manufacturerId) && (m.Title == Title))
                && !items.Any(m => (m.Id != manufacturerId) && (m.Phone == Phone))
                && !items.Any(m => (m.Id != manufacturerId) && m.Email == email);
        }

        private async void OnCancel() => await Shell.Current.GoToAsync("..");

        private async void OnSave()
        {
            Manufacturer newManufacturer = new Manufacturer()
            {
                Id = ManufacturerId,
                Title = Title,
                Address = Address,
                Phone = Phone,
                Email = Email
            };
            App.ManufacturerRepo.Update(newManufacturer);
            await Shell.Current.GoToAsync("..");
        }

        private async void OnDelete()
        {
            App.ManufacturerRepo.Delete(App.ManufacturerRepo.Get(ManufacturerId));
            await Shell.Current.GoToAsync("..");
        }

        public void LoadManufacturerId(int ManufacturerId)
        {
            try
            {
                var Manufacturer = App.ManufacturerRepo.Get(ManufacturerId);
                Title = Manufacturer.Title;
                Address = Manufacturer.Address;
                Phone = Manufacturer.Phone;
                Email = Manufacturer.Email;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Manufacturer");
            }
        }
    }
}
