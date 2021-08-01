namespace CarShop.Services.Posts
{
    using System.Threading.Tasks;
    using CarShop.Web.Data.Models;
    using CarShop.Web.Data;

    public class PostsService : IPostsService
    {
        private readonly ApplicationDbContext db;

        public PostsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task CreatePostAsync(string ownerId, Car car)
        {
            var post = new Post
            {
                Car = car,
                OwnerId = ownerId
            };

            await db.Posts.AddAsync(post);
            await db.SaveChangesAsync();
        }
    }
}
