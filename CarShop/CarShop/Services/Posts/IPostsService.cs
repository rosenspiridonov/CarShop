namespace CarShop.Services.Posts
{
    using CarShop.Models.Cars;

    public interface IPostsService
    {
        CreateCarViewData GetCarEntities();
    }
}
