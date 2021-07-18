namespace CarShop.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Car
    {
        public Car()
        {
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

        public int HorsePower { get; set; }

        public virtual EuroStandard EuroStandard { get; set; }
        public int EuroStandardId { get; set; }

        public virtual Transmision Transmision { get; set; }
        public int TransmisionId { get; set; }

        public virtual Coupe CoupeType { get; set; }
        public int CoupeTypeId { get; set; }

        public int TravelledDistance { get; set; }

        public string Color { get; set; }

        public string ImageUrl { get; set; }

        public virtual IEnumerable<Safety> SafetyProperties { get; set; }
                
        public virtual IEnumerable<Comfort> ComfortProperties { get; set; }
                
        public virtual IEnumerable<Other> OtherProperties { get; set; }
                
        public virtual IEnumerable<Exterior> ExteriorProperties { get; set; }
                
        public virtual IEnumerable<Interior> InteriorProperties { get; set; }
                
        public virtual IEnumerable<Protection> ProtectionProperties { get; set; }
                
        public virtual IEnumerable<Special> SpecialProperties { get; set; }
    }
}
