using Microsoft.EntityFrameworkCore;
using Pharmacy.Data.Abstract;
using Pharmacy.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Data
{
    internal class ManufacturerRepository : IRepository<Manufacturer>
    {
        readonly PharmacyDbContext context;
        public ManufacturerRepository(PharmacyDbContext context)
        {
            this.context = context;
        }

        public async Task Delete(int id)
        {
            context.Manufacturers.Remove(await GetById(id));
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Manufacturer>> GetAll()
        {
            return await context.Set<Manufacturer>().ToListAsync();
        }

        public async Task<Manufacturer> GetById(int id)
        {
            return await context.Manufacturers.FindAsync(id);
        }

        public async Task Insert(Manufacturer obj)
        {
            await context.Manufacturers.AddAsync(obj);
            await context.SaveChangesAsync();
        }

        public async Task Update(Manufacturer obj)
        {
            var existingManuf = context.Manufacturers.Local.SingleOrDefault(m => m.ManufacturerId == obj.ManufacturerId);
            if (existingManuf != null)
                context.Entry(existingManuf).State = EntityState.Detached;

            context.Manufacturers.Update(obj);
            await context.SaveChangesAsync();
        }
    }
}
