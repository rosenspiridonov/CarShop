namespace CarShop.Web.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;

    public class Post
    {
        public Post()
        {
            IsActive = true;
            PublishedOn = DateTime.UtcNow;
        }

        public int Id { get; set; }

        public DateTime PublishedOn { get; set; }

        public Car Car { get; set; }
        public int CarId { get; set; }

        public IdentityUser Owner { get; set; }
        public string OwnerId { get; set; }

        public bool IsActive { get; set; }
    }
}
