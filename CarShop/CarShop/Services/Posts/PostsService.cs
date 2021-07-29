namespace CarShop.Services.Posts
{
    using System.Linq;
    using System.Threading.Tasks;
    using CarShop.Data;
    using CarShop.Data.Models;
    using CarShop.Models.Cars;

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

        public CreateCarViewData GetCarEntities() 
            => new CreateCarViewData
            {
                Brands = db.Brands.ToList(),
                Models = db.Models.ToList(),
                EngineTypes = db.EngineTypes.ToList(),
                EuroStandards = db.EuroStandards.ToList(),
                TransmisionTypes = db.TransmisionTypes.ToList(),
                CoupeTypes = db.CoupeTypes.ToList(),
                SafetyProperties = db.SafetyProperties.ToList(),
                ComfortProperties = db.ComfortProperties.ToList(),
                OtherProperties = db.OtherProperties.ToList(),
                ExteriorProperties = db.ExteriorProperties.ToList(),
                InteriorProperties = db.InteriorProperties.ToList(),
                ProtectionProperties = db.ProtectionProperties.ToList(),
                SpecialProperties = db.SpecialProperties.ToList(),
            };
    }
}
