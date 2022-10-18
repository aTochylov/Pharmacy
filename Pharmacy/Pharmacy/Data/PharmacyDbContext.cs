using Microsoft.EntityFrameworkCore;
using Pharmacy.Data.Configurations;
using Pharmacy.Models;
using System.IO;
using Xamarin.Essentials;

namespace Pharmacy.Data
{
    public class PharmacyDbContext : DbContext
    {
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Medicine> Medicines { get; set; }

        public PharmacyDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "pharmacy.db3");

            optionsBuilder
                .UseSqlite($"Filename={dbPath}")
                .EnableSensitiveDataLogging(true);
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    new MedicineConfiguration().Configure(modelBuilder.Entity<Medicine>());
        //    new ManufacturerConfiguration().Configure(modelBuilder.Entity<Manufacturer>());
        //}
    }
}
