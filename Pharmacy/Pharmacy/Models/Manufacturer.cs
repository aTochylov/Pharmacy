using System.Collections.Generic;

namespace Pharmacy.Models
{
        public class Manufacturer
        {
            public int ManufacturerId { get; set; }
            public string Title { get; set; }
            public string Address { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }

            public List<Medicine> Medicines { get; set; } = new List<Medicine>();
            public override string ToString() => Title;
        }

}
