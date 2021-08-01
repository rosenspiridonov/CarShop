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
        private readonly UserManager<IdentityUser> userManager;

        public DataSeeder(
            ApplicationDbContext db,
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager)
        {
            this.db = db;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task PopulateDb()
        {
            await SeedRoles();
            await SeedAdministrators();
            await SeedCarsAsync();
        }

        private async Task SeedCarsAsync()
        {
            if (db.Cars.Any())
            {
                return;
            }

            var carsSeeder = new CarsSeeder(db, userManager);
            await carsSeeder.ProcessCars();
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

        private async Task SeedAdministrators()
        {
            if (db.Users.Any())
            {
                return;
            }

            await userManager.CreateAsync(new IdentityUser
            {
                Email = "admin@carshop.com",
                UserName = "admin@carshop.com"
            }, "adminadmin");

            var admin = await userManager.FindByEmailAsync("admin@carshop.com");
            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}
