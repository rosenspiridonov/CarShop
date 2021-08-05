namespace CarShop.Web.Models.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static CarShop.Web.DataConstants;

    public class CarInputModel
    {
        public int Id { get; set; }

        [Display(Name = "Brand")]
        public int BrandId { get; set; }

        [Display(Name = "Model")]
        public int ModelId { get; set; }

        [StringLength(CarModificationMaxLength)]
        public string Modification { get; set; }

        [Range(CarPriceMinValue, int.MaxValue)]
        public decimal Price { get; set; }

        [StringLength(CarDescriptionMaxLength)]
        public string Description { get; set; }

        [Range(0, 2022)]
        [Display(Name = "Produce year")]
        public int ProduceYear { get; set; }

        [Display(Name = "Engine type")]
        public int EngineTypeId { get; set; }

        [Range(CarHorsePowerMinValue, int.MaxValue)]
        [Display(Name = "Horse power")]
        public int? HorsePower { get; set; }

        [Display(Name = "Euro standard")]
        public int? EuroStandardId { get; set; }

        [Display(Name = "Transmission type")]
        public int TransmisionTypeId { get; set; }

        [Display(Name = "Coupe type")]
        public int CoupeTypeId { get; set; }

        [Range(CarTravelledDistanceMinValue, int.MaxValue)]
        [Display(Name = "Travelled distance")]
        public int? TravelledDistance { get; set; }

        [StringLength(CarColorMaxLength, MinimumLength = CarColorMinLength)]
        public string Color { get; set; }

        [Required]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [Display(Name = "Safety")]
        public List<int> SafetyPropertiesIds { get; set; }

        [Display(Name = "Comfort")]
        public List<int> ComfortPropertiesIds { get; set; }

        [Display(Name = "Other")]
        public List<int> OtherPropertiesIds { get; set; }

        [Display(Name = "Exterior")]
        public List<int> ExteriorPropertiesIds { get; set; }

        [Display(Name = "Interior")]
        public List<int> InteriorPropertiesIds { get; set; }

        [Display(Name = "Protection")]
        public List<int> ProtectionPropertiesIds { get; set; }

        [Display(Name = "Special")]
        public List<int?> SpecialPropertiesIds { get; set; }

        public string OwnerId { get; set; }
    }
}
