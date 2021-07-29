namespace CarShop.Services.Posts
{
    using System.Threading.Tasks;

    using CarShop.Data.Models;
    using CarShop.Models.Cars;

    public interface IPostsService
    {
        CreateCarViewData GetCarEntities();

        Task CreatePostAsync(string ownerId, Car car);
    }
}
