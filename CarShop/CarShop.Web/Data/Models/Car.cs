using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static CarShop.Web.DataConstants;

namespace CarShop.Web.Data.Models
{
    public class Car : BaseDeletableModel
    {
        public Car()
        {
            IsActive = true;
            PublishedOn = DateTime.UtcNow;

            SafetyProperties = new HashSet<Safety>();
            ComfortProperties = new HashSet<Comfort>();
            OtherProperties = new HashSet<Other>();
            ExteriorProperties = new HashSet<Exterior>();
            InteriorProperties = new HashSet<Interior>();
            ProtectionProperties = new HashSet<Protection>();
            SpecialProperties = new HashSet<Special>();
        }

        public int Id { get; set; }

        public Brand Brand { get; set; }
        public int BrandId { get; set; }

        public Model Model { get; set; }
        public int ModelId { get; set; }

        [MaxLength(CarModificationMaxLength)]
        public string Modification { get; set; }

        public decimal Price { get; set; }

        [MaxLength(CarDescriptionMaxLength)]
        public string Description { get; set; }

        public int ProduceYear { get; set; }

        public virtual Engine EngineType { get; set; }
        public int EngineTypeId { get; set; }

        public int? HorsePower { get; set; }

        public virtual EuroStandard EuroStandard { get; set; }
        public int? EuroStandardId { get; set; }

        public virtual Transmision Transmision { get; set; }
        public int TransmisionId { get; set; }

        public virtual Coupe CoupeType { get; set; }
        public int CoupeTypeId { get; set; }

        public int? TravelledDistance { get; set; }

        [MaxLength(CarColorMaxLength)]
        public string Color { get; set; }

        public virtual Image Image { get; set; }
        public int ImageId { get; set; }

        public IdentityUser Owner { get; set; }
        [Required]
        public string OwnerId { get; set; }

        public bool IsActive { get; set; }

        public DateTime PublishedOn { get; set; }

        public virtual ICollection<Safety> SafetyProperties { get; set; }
                
        public virtual ICollection<Comfort> ComfortProperties { get; set; }
                
        public virtual ICollection<Other> OtherProperties { get; set; }
                
        public virtual ICollection<Exterior> ExteriorProperties { get; set; }
                
        public virtual ICollection<Interior> InteriorProperties { get; set; }
                
        public virtual ICollection<Protection> ProtectionProperties { get; set; }
                
        public virtual ICollection<Special> SpecialProperties { get; set; }
    }
}
