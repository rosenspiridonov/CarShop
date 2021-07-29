namespace CarShop.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Engine
    {
        public Engine()
        {
            Cars = new HashSet<Car>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(EngineTypeNameMaxLength)]
        public string Name { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}