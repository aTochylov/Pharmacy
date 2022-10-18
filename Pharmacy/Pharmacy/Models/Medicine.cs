using System;

namespace Pharmacy.Models
{
    public class Medicine
    {
        public int MedicineId { get; set; }
        public string Title { get; set; }
        public string Barcode { get; set; }
        public string Packaging { get; set; }
        public decimal Price { get; set; }
        public bool OnPrescription { get; set; }
        public DateTime DateOfManufacture { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Quantity { get; set; }

        public int ManufacturerId { get; set; }

        public override string ToString()
        {
            return $"{MedicineId} {Title} {Barcode} {Packaging} {Price} {OnPrescription} {DateOfManufacture} {ExpirationDate} {Quantity} {ManufacturerId} ";
        }
    }
}
