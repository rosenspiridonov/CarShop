namespace CarShop.Web.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class DealerRequest
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public bool Pending { get; set; }

        public bool IsAccepted { get; set; }
    }
}
