namespace CarShop.Web.Services.Cars.Models
{
    public class CarListingServiceModel
    {
        public int Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Modification { get; set; }

        public decimal Price { get; set; }

        public int Year { get; set; }

        public int TravelledDistance { get; set; }

        public string ImageUrl { get; set; }

        public string OwnerId { get; set; }
    }
}
