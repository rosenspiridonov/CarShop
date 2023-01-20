using System.ComponentModel.DataAnnotations;

namespace CarShop.Web.Data.Models
{
    public class Image
    {
        public int Id { get; set; }

        [Required]
        public string Url { get; set; }
    }
}
