using SQLite;
using System;

namespace Pharmacy.Models
{
    [Table("Medicine")]
    public class Medicine
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int Id { get; set; }

        public string Title { get; set; }

        [Unique]
        public string Barcode { get; set; }

        [NotNull]
        public int ManufacturerId { get; set; }
        public string Packaging { get; set; }
        public decimal Price { get; set; }
        public bool OnPrescription { get; set; }
        public DateTime DateOfManufacture { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Quantity { get; set; }
    }
}
