using Pharmacy.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Pharmacy.ViewModels
{
    [QueryProperty(nameof(MedicineId), nameof(MedicineId))]
    public class MedicineDetailViewModel : BaseViewModel
    {
        private int medicineId;
        private string title;
        private string barcode;
        private string manufacturer;
        private int manufacturerId;
        private string packaging;
        private decimal price;
        private bool onPrescription;
        private DateTime dateOfManufacture;
        private DateTime expirationDate;
        private int quantity;

        public string PageTitle { get; set; }

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

        public string Manufacturer
        {
            get => manufacturer;
            set => SetProperty(ref manufacturer, value);
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

        public int MedicineId
        {
            get => medicineId;
            set
            {
                medicineId = value;
                LoadMedicineId(value);
            }
        }


        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command DeleteCommand { get; }

        public MedicineDetailViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            DeleteCommand = new Command(OnDelete);
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(title)
                && !String.IsNullOrWhiteSpace(barcode)
                && !Task.Run(async() => await data.MedicineRepository.GetAll()).Result.Any(m => (m.MedicineId != MedicineId) && (m.Barcode == Barcode));
        }

        private async void OnCancel() => await Shell.Current.GoToAsync("..");

        private async void OnSave()
        {
            Medicine newMedicine = new Medicine()
            {
                MedicineId = MedicineId,
                Title = Title,
                Barcode = Barcode,
                ManufacturerId = manufacturerId,
                Packaging = Packaging,
                Price = Price,
                OnPrescription = OnPrescription,
                DateOfManufacture = DateOfManufacture,
                ExpirationDate = ExpirationDate,
                Quantity = Quantity
            };
            await data.MedicineRepository.Update(newMedicine);
            await Shell.Current.GoToAsync("..");
        }

        private async void OnDelete()
        {
            await data.MedicineRepository.Delete(MedicineId);
            await Shell.Current.GoToAsync("..");
        }

        public async void LoadMedicineId(int MedicineId)
        {
            try
            {
                var Medicine = await data.MedicineRepository.GetById(MedicineId);
                Title = Medicine.Title;
                Barcode = Medicine.Barcode;
                Manufacturer = Medicine.Manufacturer.Title;
                manufacturerId = Medicine.ManufacturerId;
                Packaging = Medicine.Packaging;
                Price = Medicine.Price;
                OnPrescription = Medicine.OnPrescription;
                DateOfManufacture = Medicine.DateOfManufacture;
                ExpirationDate = Medicine.ExpirationDate;
                Quantity = Medicine.Quantity;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Medicine");
            }
        }
    }
}
