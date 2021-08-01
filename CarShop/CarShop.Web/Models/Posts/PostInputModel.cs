namespace CarShop.Web.Models.Posts
{
    using CarShop.Web.Models.Cars;

    public class PostInputModel
    {
        public CreateCarViewData Data { get; set; }

        public CarInputModel Car { get; set; }

        public string OwnerId { get; set; }
    }
}
