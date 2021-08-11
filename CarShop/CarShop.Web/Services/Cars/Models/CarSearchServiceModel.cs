using System.Collections.Generic;

using CarShop.Web.Data.Models;

namespace CarShop.Web.Services.Cars.Models
{
    public class CarSearchServiceModel
    {
        public int? BrandId { get; set; }

        public int? ModelId { get; set; }

        public decimal? PriceFrom { get; set; }

        public decimal? PriceTo { get; set; }

        public int? YearFrom { get; set; }

        public int? YearTo { get; set; }

        public int? HorsePowerFrom { get; set; }

        public int? HorsePowerTo { get; set; }

        public int? EngineTypeId { get; set; }

        public int? TransmisionTypeId { get; set; }

        public int? MaxTravelledDistance { get; set; }

        public IEnumerable<Safety> SafetyProperties { get; set; }

        public IEnumerable<Comfort> ComfortProperties { get; set; }

        public IEnumerable<Other> OtherProperties { get; set; }

        public IEnumerable<Exterior> ExteriorProperties { get; set; }

        public IEnumerable<Interior> InteriorProperties { get; set; }

        public IEnumerable<Protection> ProtectionProperties { get; set; }

        public IEnumerable<Special> SpecialProperties { get; set; }
    }
}
