namespace Pharmacy.Data
{
    public class UnitOfWork
    {
        private static UnitOfWork _unitOfWork;
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
                    this.medicineRepository = new MedicineRepository();
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
                    this.manufacturerRepository = new ManufacturerRepository();
                }
                return manufacturerRepository;
            }
        }
    }
}
