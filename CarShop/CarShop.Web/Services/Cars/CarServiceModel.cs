namespace CarShop.Web.Services.Cars
{
    using System.Collections.Generic;

    public class CarServiceModel
    {
        public int Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Modification { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int ProduceYear { get; set; }

        public string EngineType { get; set; }

        public int? HorsePower { get; set; }

        public string EuroStandard { get; set; }

        public string TransmisionType { get; set; }

        public string CoupeType { get; set; }

        public int? TravelledDistance { get; set; }

        public string Color { get; set; }

        public string ImageUrl { get; set; }

        public string OwnerId { get; set; }

        public bool IsActive { get; set; }

        public IEnumerable<string> SafetyProperties { get; set; }

        public IEnumerable<string> ComfortProperties { get; set; }

        public IEnumerable<string> OtherProperties { get; set; }

        public IEnumerable<string> ExteriorProperties { get; set; }

        public IEnumerable<string> InteriorProperties { get; set; }

        public IEnumerable<string> ProtectionProperties { get; set; }

        public IEnumerable<string> SpecialProperties { get; set; }
    }
}
