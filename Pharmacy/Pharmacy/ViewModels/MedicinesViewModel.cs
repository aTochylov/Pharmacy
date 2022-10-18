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
    public class MedicinesViewModel : BaseViewModel
    {
        private readonly UnitOfWork Data;

        private Medicine _selectedMedicine;

        public ObservableCollection<Medicine> Medicines { get; }
        public Command LoadMedicinesCommand { get; }
        public Command AddMedicineCommand { get; }
        public Command<Medicine> MedicineTapped { get; }

        public string Title { get; set; }

        public MedicinesViewModel()
        {
            Data = UnitOfWork.GetUnitOfWork();
            Title = "Browse medicine";
            Medicines = new ObservableCollection<Medicine>();
            LoadMedicinesCommand = new Command(() => ExecuteLoadMedicinesCommand());

            MedicineTapped = new Command<Medicine>(OnMedicineSelected);

            AddMedicineCommand = new Command(OnAddMedicine);
        }

        async void ExecuteLoadMedicinesCommand()
        {
            IsBusy = true;

            try
            {
                Medicines.Clear();
                var medicines = await Data.MedicineRepository.GetAll();
                foreach (var medicine in medicines)
                {
                    Medicines.Add(medicine);
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

        public async void OnSearchTextChanged(string query)
        {
            Medicines.Clear();
            var results = Data.MedicineRepository.GetAll().Result.Join(await Data.ManufacturerRepository.GetAll(), med => med.ManufacturerId, manuf => manuf.ManufacturerId,
                        (med, manuf) => new { Id = med.MedicineId, MedTitle = med.Title, Barcode = med.Barcode, ManufTitle = manuf.Title, Address = manuf.Address })
                .Where(i => i.MedTitle.ToLower().Contains(query.ToLower())
            || i.Barcode.ToLower().Contains(query.ToLower())
            || i.ManufTitle.ToLower().Contains(query.ToLower())
            || i.Address.ToLower().Contains(query.ToLower()));
            foreach (var r in results)
                Medicines.Add(await Data.MedicineRepository.GetById(r.Id));
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedMedicine = null;
        }

        public Medicine SelectedMedicine
        {
            get => _selectedMedicine;
            set
            {
                SetProperty(ref _selectedMedicine, value);
                OnMedicineSelected(value);
            }
        }

        private async void OnAddMedicine(object obj) => await Shell.Current.GoToAsync(nameof(NewMedicinePage));

        async void OnMedicineSelected(Medicine Medicine)
        {
            if (Medicine == null)
                return;
            await Shell.Current.GoToAsync($"{nameof(MedicineDetailPage)}?{nameof(MedicineDetailViewModel.MedicineId)}={Medicine.MedicineId}");
        }
    }
}