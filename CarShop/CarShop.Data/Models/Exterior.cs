namespace CarShop.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Exterior
    {
        public Exterior()
        {
            Cars = new HashSet<Car>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(ExteriorNameMaxLength)]
        public string Name { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}