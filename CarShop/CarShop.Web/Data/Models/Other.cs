using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static CarShop.Web.DataConstants;

namespace CarShop.Web.Data.Models
{
    public class Other
    {
        public Other()
        {
            Cars = new HashSet<Car>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(OtherNameMaxLength)]
        public string Name { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}