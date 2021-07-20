using System.Collections.Generic;

namespace CarShop.Data.Models
{
    public class Other
    {
        public Other()
        {
            Cars = new HashSet<Car>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}