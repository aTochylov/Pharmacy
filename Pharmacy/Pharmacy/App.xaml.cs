using Pharmacy.Models;
using Pharmacy.Services;
using Pharmacy.Views;
using SQLite;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pharmacy
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "pharmacy.db";
        private static SQLiteConnection database;

        private static MedicineRepository medicineRepo;
        private static ManufacturerRepository manufacturerRepo;

        public static MedicineRepository MedicineRepo
        {
            get
            {
                if (medicineRepo == null)
                {
                    medicineRepo = new MedicineRepository(database);
                }
                return medicineRepo;
            }
        }

        public static ManufacturerRepository ManufacturerRepo
        {
            get
            {
                if (manufacturerRepo == null)
                {
                    manufacturerRepo = new ManufacturerRepository(database);
                }
                return manufacturerRepo;
            }
        }

        public App()
        {
            InitializeComponent();
            database = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
