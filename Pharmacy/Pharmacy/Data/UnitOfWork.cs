using Pharmacy.Data.Abstract;
using Pharmacy.Models;
using System;
using System.Threading.Tasks;

namespace Pharmacy.Data
{
    internal class UnitOfWork : IDisposable
    {
        private static UnitOfWork _unitOfWork;
        private readonly PharmacyDbContext context = new PharmacyDbContext();
        private MedicineRepository medicineRepository;
        private ManufacturerRepository manufacturerRepository;

        private UnitOfWork() { }

        public static UnitOfWork GetUnitOfWork()
        {
            if (_unitOfWork is null)
                _unitOfWork = new UnitOfWork();
            return _unitOfWork;
        }

        public MedicineRepository MedicineRepository
        {
            get
            {
                if (this.medicineRepository == null)
                {
                    this.medicineRepository = new MedicineRepository(context);
                }
                return medicineRepository;
            }
        }

        public ManufacturerRepository ManufacturerRepository
        {
            get
            {
                if (this.manufacturerRepository == null)
                {
                    this.manufacturerRepository = new ManufacturerRepository(context);
                }
                return manufacturerRepository;
            }
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
