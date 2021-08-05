using System.ComponentModel.DataAnnotations;

namespace CarShop.Web.Models.Dealers
{
    public class DealerFormModel
    {
        [Required]
        [RegularExpression(@"[0-9]+", ErrorMessage = "Phone number must contain digits only.")]
        public string PhoneNumber { get; set; }
    }
}
