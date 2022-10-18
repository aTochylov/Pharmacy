using Microsoft.EntityFrameworkCore;
using Pharmacy.Data.Abstract;
using Pharmacy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Data
{
    internal class MedicineRepository : IRepository<Medicine>
    {
        readonly PharmacyDbContext context;
        public MedicineRepository(PharmacyDbContext context)
        {
            this.context = context;
        }

        public async Task Delete(int id)
        {
            context.Medicines.Remove(await GetById(id));
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Medicine>> GetAll()
        {
            return await context.Set<Medicine>().ToListAsync();
        }

        public async Task<Medicine> GetById(int id)
        {
            return await context.Medicines.FindAsync(id);
        }

        public async Task Insert(Medicine obj)
        {
            await context.Medicines.AddAsync(obj);
            await context.SaveChangesAsync();
        }

        public async Task Update(Medicine obj)
        {
            try
            {
                var existingMed = context.Medicines.Local.SingleOrDefault(m => m.MedicineId == obj.MedicineId);
                if (existingMed != null)
                    context.Entry(existingMed).State = EntityState.Detached;
                context.Attach(obj);
                context.Entry(obj).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"******\n{e.InnerException.Message}\n******");
            }
        }
    }
}
