namespace MobileBgDataScraper.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static CarShop.Data.DataConstants;

    public class CarDto
    {
        public CarDto()
        {
            SafetyProperties = new List<string>();
            ComfortProperties = new List<string>();
            OtherProperties = new List<string>();
            ExteriorProperties = new List<string>();
            InteriorProperties = new List<string>();
            ProtectionProperties = new List<string>();
            SpecialProperties = new List<string>();
        }

        public int Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        [MaxLength(CarModificationMaxLength)]
        public string Modification { get; set; }

        public decimal Price { get; set; }

        [MaxLength(CarDescriptionMaxLength)]
        public string Description { get; set; }
       
        public int ProduceYear { get; set; }

        public string EngineType { get; set; }

        public int? HorsePower { get; set; }
        
        public string EuroStandard { get; set; }

        public string Transmision { get; set; }

        public string CoupeType { get; set; }

        public int? TravelledDistance { get; set; }

        public string Color { get; set; }

        public string ImageUrl { get; set; }

        public virtual IEnumerable<string> SafetyProperties { get; set; }

        public virtual IEnumerable<string> ComfortProperties { get; set; }

        public virtual IEnumerable<string> OtherProperties { get; set; }

        public virtual IEnumerable<string> ExteriorProperties { get; set; }

        public virtual IEnumerable<string> InteriorProperties { get; set; }

        public virtual IEnumerable<string> ProtectionProperties { get; set; }

        public virtual IEnumerable<string> SpecialProperties { get; set; }
    }
}
