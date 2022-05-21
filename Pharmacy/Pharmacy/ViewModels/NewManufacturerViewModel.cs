using Pharmacy.Models;
using System;
using System.Linq;
using Xamarin.Forms;

namespace Pharmacy.ViewModels
{
    public class NewManufacturerViewModel : BaseViewModel
    {
        private string title;
        private string address;
        private string phone;
        private string email;

        public NewManufacturerViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            var items = App.ManufacturerRepo.GetItems();
            return !String.IsNullOrWhiteSpace(title)
                && !String.IsNullOrWhiteSpace(phone)
                && !String.IsNullOrWhiteSpace(email)
                && !items.Any(m => m.Phone == phone)
                && !items.Any(m => m.Email == email);
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public string Phone
        {
            get => phone;
            set => SetProperty(ref phone, value);
        }

        public string Address
        {
            get => address;
            set => SetProperty(ref address, value);
        }
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel() => await Shell.Current.GoToAsync("..");

        private async void OnSave()
        {
            Manufacturer newManufacturer = new Manufacturer()
            {
                Title = Title,
                Phone = Phone,
                Address = Address,
                Email = Email
            };
            App.ManufacturerRepo.Add(newManufacturer);
            await Shell.Current.GoToAsync("..");
        }
    }
}
