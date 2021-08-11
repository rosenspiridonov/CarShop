namespace CarShop.Web.Services.Images
{
    using System;
    using System.Net;

    public class ImagesService : IImagesService
    {
        public bool IsValid(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
