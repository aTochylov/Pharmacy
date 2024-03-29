﻿using Pharmacy.Models;
using Pharmacy.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace Pharmacy.ViewModels
{
    public class MedicinesViewModel : BaseViewModel
    {
        private Medicine _selectedMedicine;

        public ObservableCollection<Medicine> Medicines { get; }
        public Command LoadMedicinesCommand { get; }
        public Command AddMedicineCommand { get; }
        public Command<Medicine> MedicineTapped { get; }

        public string Title { get; set; }

        public MedicinesViewModel()
        {
            Title = "Browse medicine";
            Medicines = new ObservableCollection<Medicine>();
            LoadMedicinesCommand = new Command(() => ExecuteLoadMedicinesCommand());

            MedicineTapped = new Command<Medicine>(OnMedicineSelected);

            AddMedicineCommand = new Command(OnAddMedicine);
        }

        void ExecuteLoadMedicinesCommand()
        {
            IsBusy = true;

            try
            {
                Medicines.Clear();
                var medicines = App.MedicineRepo.GetItems();
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

        public void OnSearchTextChanged(string query)
        {
            Medicines.Clear();
            var results = App.MedicineRepo.GetSearchResults(query);
            foreach (var r in results)
                Medicines.Add(r);
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
            await Shell.Current.GoToAsync($"{nameof(MedicineDetailPage)}?{nameof(MedicineDetailViewModel.MedicineId)}={Medicine.Id}");
        }
    }
}