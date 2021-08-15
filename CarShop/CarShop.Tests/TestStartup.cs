namespace CarShop.Tests
{
    using CarShop.Tests.Mocks;
    using CarShop.Web;
    using CarShop.Web.Services.Dealers;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using MyTested.AspNetCore.Mvc;

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
        }
    }
}
