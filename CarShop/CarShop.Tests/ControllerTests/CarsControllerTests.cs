namespace CarShop.Tests.ControllerTests
{
    using CarShop.Web.Controllers;
    using CarShop.Web.Services.Cars.Models;
    using CarShop.Web.Models.Cars;

    using Xunit;
    using MyTested.AspNetCore.Mvc;

    using static CarShop.Web.WebConstants;
    using static Data.Cars;
    using Microsoft.AspNetCore.Identity;

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
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(DealerRoleName + ", " + AdminRoleName))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<CarFormModel>());

        [Fact]
        public void PostCreateShouldBeForAuthorizedAndReturnRedirec()
            => MyController<CarsController>
                .Instance(instance => instance
                    .WithUser(user => user
                        .WithIdentifier(TestUser.Identifier)
                        .InRole(DealerRoleName)))
                .Calling(c => c.Create(ValidCarFormModel))
                .ShouldHave()
                .ValidModelState()
                .AndAlso()
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests(DealerRoleName + ", " + AdminRoleName))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<CarsController>(c => c.Details(With.Any<int>())));

        [Fact]
        public void DetailsShouldReturnCorrectViewAndModel()
            => MyController<CarsController>
                .Instance(instance => instance
                    .WithData(TestCar)
                    .WithData(new IdentityUser()
                    {
                        Id = TestUser.Identifier,
                        UserName = TestUser.Username,
                        PhoneNumber = "1234567890"
                    })
                    .WithUser())
                .Calling(c => c.Details(TestCar.Id))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<CarDetailsViewModel>());

        [Fact]
        public void GetEditShouldBeForAuthorizedUsersAndReturnView()
            => MyController<CarsController>
                .Instance(instance => instance
                    .WithUser(DealerRoleName)
                    .WithData(TestCar))
                .Calling(c => c.Edit(TestCar.Id))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(DealerRoleName + ", " + AdminRoleName))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<CarFormModel>());

        [Fact]
        public void PostEditShouldBeForAuthorizedUserAndReturnRedirect()
            => MyController<CarsController>
                .Instance(instance => instance
                    .WithUser(DealerRoleName)
                    .WithData(TestCar))
                .Calling(c => c.Edit(ValidCarFormModel))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(DealerRoleName + ", " + AdminRoleName)
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("Details", new { id = TestCar.Id });

        [Fact]
        public void DeleteShouldBeForAuthorizedUsersAndReturnRedirect()
            => MyController<CarsController>
                .Instance(instance => instance
                    .WithUser(DealerRoleName)
                    .WithData(TestCar))
                .Calling(c => c.Delete(TestCar.Id))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(DealerRoleName + ", " + AdminRoleName))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<HomeController>(c => c.Index()));
    }
}
