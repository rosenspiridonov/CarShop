namespace CarShop.Services.Posts
{
    using CarShop.Data;
    using CarShop.Models.Cars;
    using System.Linq;

    public class PostsService : IPostsService
    {
        private readonly ApplicationDbContext db;

        public PostsService(ApplicationDbContext db)
        {
            this.db = db;
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
