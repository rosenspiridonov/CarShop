using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MobileBgDataScraper
{
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


        public string Brand { get; set; }

        public string Model { get; set; }

        public string Modification { get; set; }

        public decimal Price { get; set; }

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

        public virtual ICollection<string> SafetyProperties { get; set; }

        public virtual ICollection<string> ComfortProperties { get; set; }

        public virtual ICollection<string> OtherProperties { get; set; }

        public virtual ICollection<string> ExteriorProperties { get; set; }

        public virtual ICollection<string> InteriorProperties { get; set; }

        public virtual ICollection<string> ProtectionProperties { get; set; }

        public virtual ICollection<string> SpecialProperties { get; set; }
    }
}
