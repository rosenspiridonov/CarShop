using System.Collections.Generic;

namespace CarShop.Data.Models
{
    public class Brand
    {
        public Brand()
        {
            Models = new HashSet<Model>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Model> Models { get; set; }
    }
}