namespace CarShop.Tests.ControllerTests
{
    using CarShop.Web.Controllers;
    using CarShop.Web.Services.Cars.Models;
    using CarShop.Web.Models.Cars;

    using Xunit;
    using MyTested.AspNetCore.Mvc;

    using static CarShop.Web.WebConstants;

    public class CarsControllerTests
    {
        [Fact]
        public void SearchShouldReturnCorrectViewWithModel()
            => MyController<CarsController>
                .Instance()
                .Calling(c => c.Search())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<CarSearchServiceModel>());

        [Fact]
        public void GetCreateShouldBeForAuthorizedUsersAndReturnView()
            => MyController<CarsController>
                .Instance(instance => instance
                    .WithUser(user => user
                        .WithIdentifier(TestUser.Identifier)
                        .InRole(DealerRoleName)))
                .Calling(c => c.Create())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<CarFormModel>());
    }
}
