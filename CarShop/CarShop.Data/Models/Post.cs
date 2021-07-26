namespace CarShop.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;

    public class Post
    {
        public Post()
        {
            IsActive = true;
        }

        public int Id { get; set; }

        public DateTime PublishedOn { get; set; }

        public Car Car { get; set; }
        public int CarId { get; set; }

        public IdentityUser Owner { get; set; }
        public int OwnerId { get; set; }

        public bool IsActive { get; set; }
    }
}
