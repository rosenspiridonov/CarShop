using System.Collections.Generic;

namespace CarShop.Data.Models
{
    public class Interior
    {
        public Interior()
        {
            Cars = new HashSet<Car>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}