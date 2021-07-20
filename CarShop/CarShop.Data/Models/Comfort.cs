using System.Collections.Generic;

namespace CarShop.Data.Models
{
    public class Comfort
    {
        public Comfort()
        {
            Cars = new HashSet<Car>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}