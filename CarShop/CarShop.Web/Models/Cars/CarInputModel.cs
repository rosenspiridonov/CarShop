namespace CarShop.Web.Models.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static CarShop.DataConstants;

    public class CarInputModel
    {
        public int BrandId { get; set; }//

        public int ModelId { get; set; }//

        [StringLength(CarModificationMaxLength)]
        public string Modification { get; set; }//

        [Range(CarPriceMinValue, int.MaxValue)]
        public decimal Price { get; set; }//

        [StringLength(CarDescriptionMaxLength)]
        public string Description { get; set; }//

        [Range(0, 2022)]
        public int ProduceYear { get; set; }//

        public int EngineTypeId { get; set; }//

        [Range(CarHorsePowerMinValue, int.MaxValue)]
        public int? HorsePower { get; set; }//

        public int? EuroStandardId { get; set; }//

        public int TransmisionTypeId { get; set; }//

        public int CoupeTypeId { get; set; }//

        [Range(CarTravelledDistanceMinValue, int.MaxValue)]
        public int? TravelledDistance { get; set; }//

        [StringLength(CarColorMaxLength, MinimumLength = CarColorMinLength)]
        public string Color { get; set; }//

        [Required]
        [Url]
        public string ImageUrl { get; set; }//

        public List<int> SafetyPropertiesIds { get; set; }//

        public List<int> ComfortPropertiesIds { get; set; }//

        public List<int> OtherPropertiesIds { get; set; }//

        public List<int> ExteriorPropertiesIds { get; set; }//

        public List<int> InteriorPropertiesIds { get; set; }//

        public List<int> ProtectionPropertiesIds { get; set; }//

        public List<int?> SpecialPropertiesIds { get; set; }//
    }
}
