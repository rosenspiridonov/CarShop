using System.Threading.Tasks;

namespace CarShop.Web.Services.Images
{
    public interface IImagesService
    {
        Task<bool> IsValidAsync(string url);
    }
}
