using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using CarShop.Web.Data.Models;

using static CarShop.Web.DataConstants;

namespace CarShop.Web.Services.Cars.Models
{
    public class CarSearchServiceModel
    {
        [Display(Name = "Brand")]
        public int? BrandId { get; set; }

        [Display(Name = "Model")]
        public int? ModelId { get; set; }

        [Range(CarPriceMinValue, int.MaxValue)]
        [Display(Name = "Price from")]
        public decimal? PriceFrom { get; set; }

        [Range(CarPriceMinValue, int.MaxValue)]
        [Display(Name = "Price to")]
        public decimal? PriceTo { get; set; }

        [Range(CarYearMinValue, CarYearMaxValue)]
        [Display(Name = "Year from")]
        public int? YearFrom { get; set; }

        [Range(CarYearMinValue, CarYearMaxValue)]
        [Display(Name = "Year to")]
        public int? YearTo { get; set; }

        [Range(CarHorsePowerMinValue, int.MaxValue)]
        [Display(Name = "Horse power from")]
        public int? HorsePowerFrom { get; set; }

        [Range(CarHorsePowerMinValue, int.MaxValue)]
        [Display(Name = "Horse power to")]
        public int? HorsePowerTo { get; set; }

        [Display(Name = "Engine type")]
        public int? EngineTypeId { get; set; }

        [Display(Name = "Transmision type")]
        public int? TransmisionTypeId { get; set; }

        [Display(Name = "Euro standard")]
        public int? EuroStandardId { get; set; }

        [Display(Name = "Coupe type")]
        public int? CoupeTypeId { get; set; }

        [Range(CarTravelledDistanceMinValue, int.MaxValue)]
        [Display(Name = "Max travelled distance")]
        public int? MaxTravelledDistance { get; set; }

        [Display(Name = "Safety")]
        public IEnumerable<int> SafetyProperties { get; set; }

        [Display(Name = "Comfort")]
        public IEnumerable<int> ComfortProperties { get; set; }

        [Display(Name = "Other")]
        public IEnumerable<int> OtherProperties { get; set; }

        [Display(Name = "Exterior")]
        public IEnumerable<int> ExteriorProperties { get; set; }

        [Display(Name = "Interior")]
        public IEnumerable<int> InteriorProperties { get; set; }

        [Display(Name = "Protection")]
        public IEnumerable<int> ProtectionProperties { get; set; }

        [Display(Name = "Special")]
        public IEnumerable<int> SpecialProperties { get; set; }

        public CarFormData ViewData { get; set; }
    }
}
