namespace CarShop.Web.Services.Cars.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CarShop.Web.Data.Models;

    using static DataConstants;

    public class CarSearchServiceModel
    {
        public int? BrandId { get; set; }

        public int? ModelId { get; set; }

        [Range(CarPriceMinValue, int.MaxValue)]
        public decimal? PriceFrom { get; set; }

        [Range(CarPriceMinValue, int.MaxValue)]
        public decimal? PriceTo { get; set; }

        [Range(CarYearMinValue, CarYearMaxValue)]
        public int? YearFrom { get; set; }

        [Range(CarYearMinValue, CarYearMaxValue)]
        public int? YearTo { get; set; }

        [Range(CarHorsePowerMinValue, int.MaxValue)]
        public int? HorsePowerFrom { get; set; }

        [Range(CarHorsePowerMinValue, int.MaxValue)]
        public int? HorsePowerTo { get; set; }

        public int? EngineTypeId { get; set; }

        public int? TransmisionTypeId { get; set; }

        public int? EuroStandardId { get; set; }

        public int? CoupeTypeId { get; set; }

        [Range(CarTravelledDistanceMinValue, int.MaxValue)]
        public int? MaxTravelledDistance { get; set; }

        public IEnumerable<int> SafetyProperties { get; set; }

        public IEnumerable<int> ComfortProperties { get; set; }

        public IEnumerable<int> OtherProperties { get; set; }

        public IEnumerable<int> ExteriorProperties { get; set; }

        public IEnumerable<int> InteriorProperties { get; set; }

        public IEnumerable<int> ProtectionProperties { get; set; }

        public IEnumerable<int> SpecialProperties { get; set; }

        public CarFormData ViewData { get; set; }
    }
}
