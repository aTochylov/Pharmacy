using Pharmacy.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Pharmacy.ViewModels
{
    public class NewMedicineViewModel : BaseViewModel
    {
        private string title;
        private string barcode;
        private int manufacturerId;
        private string packaging;
        private decimal price;
        private bool onPrescription;
        private int quantity;
        private Manufacturer selectedManufacturer;
        private ObservableCollection<Manufacturer> manufacturers;
        private DateTime dateOfManufacture;
        private DateTime expirationDate;

        public NewMedicineViewModel()
        {
            Manufacturers = new ObservableCollection<Manufacturer>(Task.Run(async()=>await data.ManufacturerRepository.GetAll()).Result);
            DateOfManufacture = DateTime.Today;
            ExpirationDate = DateTime.Today;
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(title)
                && !String.IsNullOrWhiteSpace(barcode)
                //&& selectedManufacturer != null
                && !Task.Run(async () => await data.MedicineRepository.GetAll()).Result.Any(m => m.Barcode == Barcode);
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public string Barcode
        {
            get => barcode;
            set => SetProperty(ref barcode, value);
        }

        public int ManufacturerId
        {
            get => manufacturerId;
            set => SetProperty(ref manufacturerId, value);
        }
        public string Packaging
        {
            get => packaging;
            set => SetProperty(ref packaging, value);
        }
        public decimal Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }
        public bool OnPrescription
        {
            get => onPrescription;
            set => SetProperty(ref onPrescription, value);
        }
        public DateTime DateOfManufacture
        {
            get => dateOfManufacture.Date;
            set => SetProperty(ref dateOfManufacture, value);
        }
        public DateTime ExpirationDate
        {
            get => expirationDate.Date;
            set => SetProperty(ref expirationDate, value);
        }
        public int Quantity
        {
            get => quantity;
            set => SetProperty(ref quantity, value);
        }
        public ObservableCollection<Manufacturer> Manufacturers
        {
            get => manufacturers;
            set => SetProperty(ref manufacturers, value);
        }
        public Manufacturer SelectedManufacturer
        {
            get => selectedManufacturer;
            set => SetProperty(ref selectedManufacturer, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel() => await Shell.Current.GoToAsync("..");

        private async void OnSave()
        {
            Medicine newMedicine = new Medicine()
            {
                Title = Title,
                Barcode = Barcode,
                ManufacturerId = SelectedManufacturer.ManufacturerId,
                Packaging = Packaging,
                Price = Price,
                OnPrescription = OnPrescription,
                DateOfManufacture = DateOfManufacture,
                ExpirationDate = ExpirationDate,
                Quantity = Quantity
            };
            await data.MedicineRepository.Insert(newMedicine);
            await Shell.Current.GoToAsync("..");
        }
    }
}
