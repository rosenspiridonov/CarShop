namespace CarShop.Web.Models.Cars
{
    public class CarViewModel
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
    }
}
