namespace CarShop.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Model
    {
        public Model()
        {
            Cars = new HashSet<Car>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(ModelNameMaxLength)]
        public string Name { get; set; }

        public virtual Brand Brand { get; set; }
        public int BrandId { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}