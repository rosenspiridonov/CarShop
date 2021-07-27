using System.Collections.Generic;

namespace CarShop.Models.Cars
{
    public class CarInputModel
    {
        public int BrandId { get; set; }//

        public int ModelId { get; set; }//

        public string Modification { get; set; }//

        public decimal Price { get; set; }//

        public string Description { get; set; }//

        public int ProduceYear { get; set; }//

        public int EngineTypeId { get; set; }//

        public int? HorsePower { get; set; }//

        public int EuroStandardId { get; set; }//

        public int TransmisionTypeId { get; set; }//

        public int CoupeTypeId { get; set; }//

        public int? TravelledDistance { get; set; }//

        public string Color { get; set; }//

        public string ImageUrl { get; set; }//

        public ICollection<int> SafetyProperties { get; set; }//

        public ICollection<int> ComfortProperties { get; set; }//

        public ICollection<int> OtherProperties { get; set; }//

        public ICollection<int> ExteriorProperties { get; set; }//

        public ICollection<int> InteriorProperties { get; set; }//

        public ICollection<int> ProtectionProperties { get; set; }//

        public ICollection<int> SpecialProperties { get; set; }//
    }
}
