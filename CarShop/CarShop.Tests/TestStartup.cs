namespace CarShop.Tests
{
    using CarShop.Tests.Mocks;
    using CarShop.Web;
    using CarShop.Web.Data;
    using CarShop.Web.Services.Admin;
    using CarShop.Web.Services.Dealers;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using MyTested.AspNetCore.Mvc;

    using static Data.Cars;

    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) 
            : base(configuration)
        {
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.ReplaceTransient<IDealersService, DealersServiceMock>();
            services.ReplaceTransient<IAdminService, AdminServiceMock>();
        }
    }
}
