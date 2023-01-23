using CarShop.Web.Controllers;
using CarShop.Web.Services.Cars.Models;
using CarShop.Web.Models.Cars;

using Xunit;
using MyTested.AspNetCore.Mvc;

using static CarShop.Web.WebConstants;
using static CarShop.Tests.Data.Cars;
using Microsoft.AspNetCore.Identity;

namespace CarShop.Tests.ControllerTests
{
    public class CarsControllerTests
    {
        [Fact]
        public void SearchShouldReturnCorrectViewWithModel()
            => MyController<CarsController>
                .Instance()
                .Calling(c => c.SearchAsync())
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
                .Calling(c => c.CreateAsync())
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
                .Calling(c => c.CreateAsync(ValidCarFormModel))
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
                    .To<CarsController>(c => c.DetailsAsync(With.Any<int>())));

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
                .Calling(c => c.DetailsAsync(TestCar.Id))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<CarDetailsViewModel>());

        [Fact]
        public void GetEditShouldBeForAuthorizedUsersAndReturnView()
            => MyController<CarsController>
                .Instance(instance => instance
                    .WithUser(DealerRoleName)
                    .WithData(TestCar))
                .Calling(c => c.EditAsync(TestCar.Id))
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
                .Calling(c => c.EditAsync(ValidCarFormModel))
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
                .Calling(c => c.DeleteAsync(TestCar.Id))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(DealerRoleName + ", " + AdminRoleName))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<HomeController>(c => c.IndexAsync()));
    }
}
