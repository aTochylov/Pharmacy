using Pharmacy.Models;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Services
{
    public class MedicineRepository : IRepository<Medicine>
    {
        readonly SQLiteConnection database;

        public MedicineRepository(SQLiteConnection db)
        {
            database = db;
            database.CreateTable<Medicine>();

        }

        public int Add(Medicine item)
        {
            return database.Insert(item);
        }


        public int Delete(Medicine item)
        {
            return database.Delete(item);
        }

        public Medicine Get(int id)
        {
            return database.Get<Medicine>(id);
        }

        public IEnumerable<Medicine> GetItems()
        {
            return database.Table<Medicine>().ToList();
        }

        public int Update(Medicine item)
        {
            return database.Update(item);
        }

        public IEnumerable<Medicine> GetSearchResults(string query)
        {
            var items = GetItems().Join(App.ManufacturerRepo.GetItems(), med => med.ManufacturerId, manuf => manuf.Id,
                        (med, manuf) => new { Id = med.Id, MedTitle = med.Title, Barcode = med.Barcode, ManufTitle = manuf.Title, Address = manuf.Address })
                .Where(i => i.MedTitle.ToLower().Contains(query.ToLower())
            || i.Barcode.ToLower().Contains(query.ToLower())
            || i.ManufTitle.ToLower().Contains(query.ToLower())
            || i.Address.ToLower().Contains(query.ToLower()));
            foreach (var i in items)
            {
                yield return Get(i.Id);
            }

        }
    }
}
