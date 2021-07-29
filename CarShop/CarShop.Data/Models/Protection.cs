namespace CarShop.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Protection
    {
        public Protection()
        {
            Cars = new HashSet<Car>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(ProtectionNameMaxLength)]
        public string Name { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}