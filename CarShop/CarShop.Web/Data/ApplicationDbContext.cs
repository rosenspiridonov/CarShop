namespace CarShop.Web.Data
{
    using CarShop.Web.Data.Models;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Comfort> ComfortProperties { get; set; }
        public DbSet<Coupe> CoupeTypes { get; set; }
        public DbSet<Engine> EngineTypes { get; set; }
        public DbSet<EuroStandard> EuroStandards { get; set; }
        public DbSet<Exterior> ExteriorProperties { get; set; }
        public DbSet<Interior> InteriorProperties { get; set; }
        public DbSet<Other> OtherProperties { get; set; }
        public DbSet<Protection> ProtectionProperties { get; set; }
        public DbSet<Safety> SafetyProperties { get; set; }
        public DbSet<Special> SpecialProperties { get; set; }
        public DbSet<Transmision> TransmisionTypes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Image> Images { get; set; }

        public DbSet<DealerRequest> DealerRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Car>().Property(x => x.Price).HasPrecision(14, 2);

            builder.Entity<Model>()
                .HasOne(x => x.Brand)
                .WithMany(x => x.Models)
                .HasForeignKey(x => x.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
