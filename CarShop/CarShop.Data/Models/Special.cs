namespace CarShop.Data.Models
{
    using System.Collections.Generic;

    public class Special
    {
        public Special()
        {
            Cars = new HashSet<Car>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}