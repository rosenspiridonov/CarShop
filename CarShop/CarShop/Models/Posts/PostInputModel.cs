using CarShop.Models.Cars;

namespace CarShop.Models.Posts
{
    public class PostInputModel
    {
        public CreateCarViewData Data { get; set; }

        public CarInputModel Car { get; set; }

        public string OwnerId { get; set; }
    }
}
