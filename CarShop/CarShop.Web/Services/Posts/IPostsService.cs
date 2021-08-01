namespace CarShop.Services.Posts
{
    using System.Threading.Tasks;

    using CarShop.Web.Data.Models;

    public interface IPostsService
    {
        Task CreatePostAsync(string ownerId, Car car);
    }
}
