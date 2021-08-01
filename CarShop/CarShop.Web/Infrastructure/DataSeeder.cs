namespace CarShop.Infrastructure
{
    using CarShop.Web.Data;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class DataSeeder : IDataSeeder
    {
        private readonly ApplicationDbContext db;
        private readonly RoleManager<IdentityRole> roleManager;

        public DataSeeder(ApplicationDbContext db, RoleManager<IdentityRole> roleManager)
        {
            this.db = db;
            this.roleManager = roleManager;
        }

        public async Task PopulateDb()
        {
            this.SeedCars();
            await this.SeedRoles();
        }

        private void SeedCars()
        {
            if (db.Cars.Any())
            {
                return;
            }

            var carsSeeder = new CarsSeeder(db);
            carsSeeder.ProcessCars();
        }

        private async Task SeedRoles()
        {
            if (db.Roles.Any())
            {
                return;
            }

            await roleManager.CreateAsync(new IdentityRole() { Name = "User" });
            await roleManager.CreateAsync(new IdentityRole() { Name = "Dealer" });
            await roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
        }
    }
}
