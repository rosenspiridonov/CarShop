namespace CarShop.Data.Models
{
    using System.Collections.Generic;

    public class EuroStandard
    {
        public EuroStandard()
        {
            Cars = new HashSet<Car>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}