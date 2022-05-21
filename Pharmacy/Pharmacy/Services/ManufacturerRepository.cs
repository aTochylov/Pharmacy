using Pharmacy.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Services
{
    public class ManufacturerRepository : IRepository<Manufacturer>
    {
        readonly SQLiteConnection database;

        public ManufacturerRepository(SQLiteConnection db)
        {
            database = db;
            database.CreateTable<Manufacturer>(CreateFlags.ImplicitPK | CreateFlags.AutoIncPK);
        }

        public int Add(Manufacturer item)
        {
            return database.Insert(item);
        }

        public int Delete(Manufacturer item)
        {
            foreach (var med in database.Table<Medicine>().Where(m => m.ManufacturerId == item.Id))
                database.Delete(med);
            return database.Delete(item);
        }

        public Manufacturer Get(int id)
        {
            return database.Get<Manufacturer>(id);
        }

        public IEnumerable<Manufacturer> GetItems()
        {
            return database.Table<Manufacturer>().ToList();
        }

        public int Update(Manufacturer item)
        {
            return database.Update(item);
        }

        public IEnumerable<Manufacturer> GetSearchResults(string query)
        {
            return GetItems().Where(i => i.Title.ToLower().Contains(query.ToLower()) || i.Phone.ToLower().Contains(query.ToLower()) || i.Address.ToLower().Contains(query.ToLower()) || i.Email.ToLower().Contains(query.ToLower()));
        }
    }
}
