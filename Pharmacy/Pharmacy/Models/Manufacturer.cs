using SQLite;

namespace Pharmacy.Models
{
    [Table("Manufacturer")]
    public class Manufacturer
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        [Unique]
        public string Phone { get; set; }
        [Unique]
        public string Email { get; set; }

        public override string ToString() => Title;
    }
}
