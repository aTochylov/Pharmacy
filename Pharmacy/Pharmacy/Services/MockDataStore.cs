//using Pharmacy.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Pharmacy.Services
//{
//    public class MockDataStore
//    {
//        List<Medicine> Medicines;
//        List<Manufacturer> Manufacturers;

//        public MockDataStore()
//        {
//            Medicines = new List<Medicine>()
//            {
//                new Medicine { Id = 1, Title = "First Medicine", Barcode = "122", ManufacturerId = 1, Packaging = "10ml", DateOfManufacture = new DateTime(2008, 5, 1), ExpirationDate = new DateTime(2009, 5, 1), OnPrescription = true, Price = 100.0m, Quantity = 100},
//                new Medicine { Id = 2, Title = "Second Medicine", Barcode = "123", ManufacturerId = 1, Packaging = "20ml", DateOfManufacture = new DateTime(2008, 5, 1), ExpirationDate = new DateTime(2009, 5, 1), OnPrescription = true, Price = 100.0m, Quantity = 100},
//                new Medicine { Id = 3, Title = "Third Medicine", Barcode = "124", ManufacturerId = 1, Packaging = "30ml", DateOfManufacture = new DateTime(2008, 5, 1), ExpirationDate = new DateTime(2009, 5, 1), OnPrescription = true, Price = 100.0m, Quantity = 100},
//                new Medicine { Id = 4, Title = "Fourth Medicine", Barcode = "125", ManufacturerId = 2, Packaging = "40ml", DateOfManufacture = new DateTime(2008, 5, 1), ExpirationDate = new DateTime(2009, 5, 1), OnPrescription = true, Price = 100.0m, Quantity = 100},
//                new Medicine { Id = 5, Title = "Fifth Medicine", Barcode = "126", ManufacturerId = 2, Packaging = "50ml", DateOfManufacture = new DateTime(2008, 5, 1), ExpirationDate = new DateTime(2009, 5, 1), OnPrescription = true, Price = 100.0m, Quantity = 100},
//                new Medicine { Id = 6, Title = "Sixth Medicine", Barcode = "127", ManufacturerId = 2, Packaging = "60ml", DateOfManufacture = new DateTime(2008, 5, 1), ExpirationDate = new DateTime(2009, 5, 1), OnPrescription = true, Price = 100.0m, Quantity = 100},
//            };

//            Manufacturers = new List<Manufacturer>()
//            {
//                new Manufacturer{Id = 1, Title = "First manufacturer", Phone = "+380112223344", Address = "Kyiv", Email = "test1@mail.com"},
//                new Manufacturer{Id = 2, Title = "Second manufacturer", Phone = "+380112223354", Address = "Poltava", Email = "test2@mail.com"}
//            };

//            //Packagings = new List<Packaging>()
//            //{
//            //    new Packaging{ Id = 1, Quantity = 10, UnitOfMeasurement = "pill"},
//            //    new Packaging{ Id = 2, Quantity = 50, UnitOfMeasurement = "ml"}
//            //};
//        }

//        #region crud medicine 
//        public async Task<bool> AddAsync(Medicine medicine)
//        {
//            Medicines.Add(medicine);

//            return await Task.FromResult(true);
//        }

//        public async Task<bool> Update(Medicine medicine)
//        {
//            var oldMedicine = Medicines.FirstOrDefault(arg => arg.Id == medicine.Id);
//            Medicines.Remove(oldMedicine);
//            Medicines.Add(medicine);

//            return await Task.FromResult(true);
//        }

//        public async Task<bool> Delete(int id)
//        {
//            var oldMedicine = Medicines.FirstOrDefault((Medicine arg) => arg.Id == id);
//            Medicines.Remove(oldMedicine);

//            return await Task.FromResult(true);
//        }

//        public async Task<Medicine> Get(int id)
//        {
//            return await Task.FromResult(Medicines.FirstOrDefault(s => s.Id == id));
//        }

//        public async Task<IEnumerable<Medicine>> GetItems()
//        {
//            return await Task.FromResult(Medicines);
//        }

//        public IEnumerable<Medicine> GetSearchResults(string query)
//        {
//            var items = GetItems().Result.Join(GetManufacturersAsync().Result, med => med.ManufacturerId, manuf => manuf.Id,
//                        (med, manuf) => new { Id = med.Id, MedTitle = med.Title, Barcode = med.Barcode, ManufTitle = manuf.Title, Address = manuf.Address })
//                .Where(i => i.MedTitle.ToLower().Contains(query.ToLower())
//            || i.Barcode.ToLower().Contains(query.ToLower())
//            || i.ManufTitle.ToLower().Contains(query.ToLower())
//            || i.Address.ToLower().Contains(query.ToLower()));
//            foreach(var i in items)
//            {
//                yield return Get(i.Id).Result;
//            }
            
//        }
//        #endregion crud medicine 

//        #region crud manufacturer
//        public async Task<bool> AddManufacturerAsync(Manufacturer manufacturer)
//        {
//            Manufacturers.Add(manufacturer);

//            return await Task.FromResult(true);
//        }

//        public async Task<bool> UpdateManufacturerAsync(Manufacturer manufacturer)
//        {
//            var oldManufacturer = Manufacturers.FirstOrDefault((Manufacturer arg) => arg.Id == manufacturer.Id);
//            Manufacturers.Remove(oldManufacturer);
//            Manufacturers.Add(manufacturer);

//            return await Task.FromResult(true);
//        }

//        public async Task<bool> DeleteManufacturerAsync(int id)
//        {
//            var oldManufacturer = Manufacturers.FirstOrDefault((Manufacturer arg) => arg.Id == id);
//            Manufacturers.Remove(oldManufacturer);

//            return await Task.FromResult(true);
//        }

//        public async Task<Manufacturer> GetManufacturerAsync(int id)
//        {
//            return await Task.FromResult(Manufacturers.FirstOrDefault(s => s.Id == id));
//        }

//        public async Task<IEnumerable<Manufacturer>> GetManufacturersAsync()
//        {
//            return await Task.FromResult(Manufacturers);
//        }

//        public IEnumerable<Manufacturer> GetSearchResultsManufacturer(string query)
//        {
//            return GetManufacturersAsync().Result.Where(i => i.Title.ToLower().Contains(query.ToLower()) || i.Phone.ToLower().Contains(query.ToLower()) || i.Address.ToLower().Contains(query.ToLower()) || i.Email.ToLower().Contains(query.ToLower()));
//        }

//        #endregion  crud manufacturer

//    }
//}