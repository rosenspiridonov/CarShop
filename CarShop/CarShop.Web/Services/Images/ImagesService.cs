using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CarShop.Web.Services.Images
{
    public class ImagesService : IImagesService
    {
        public async Task<bool> IsValidAsync(string url)
        {
            using var client = new HttpClient();
            try
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
