using Xunit;

using MyTested.AspNetCore.Mvc;
using CarShop.Web.Controllers;

using static CarShop.Web.WebConstants;
using CarShop.Web.Models.Dealers;

namespace CarShop.Tests.Pipeline
{
    public class DealersControllerTests
    {
        //[Fact]
        public void BecomeShouldBeForAuthorizesUsersAndReturnView()
            => MyMvc
                .Pipeline()
                .ShouldMap(request => request
                    .WithPath("/Dealers/Become")
                    .WithUser(u => u
                        .WithIdentifier(TestUser.Identifier)
                        .InRole(UserRoleName)))
                .To<DealersController>(c => c.Become())
                .Which()
                .ShouldHave()
                .ActionAttributes(a => a
                    .RestrictingForAuthorizedRequests(UserRoleName))
                .AndAlso()
                .ShouldReturn()
                .View();

        //[Theory]
        //[InlineData("01111111111")]
        public void PostBecomeShouldBeForAuthorizedUsersAndShouldReturnRedirect(string phoneNumber)
            => MyMvc
                .Pipeline()
                .ShouldMap(request => request
                    .WithPath("/Dealers/Become")
                    .WithMethod(HttpMethod.Post)
                    .WithUser(user => user
                        .WithIdentifier(TestUser.Identifier)
                        .InRole(UserRoleName))
                    .WithFormFields(new
                    {
                        PhoneNumber = phoneNumber
                    })
                    .WithAntiForgeryToken())
                .To<DealersController>(c => c.Become())
                .Which(controller => controller
                    .WithData(data => new DealerFormModel 
                    {
                        PhoneNumber = phoneNumber
                    }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(UserRoleName)
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<DealersController>(c => c.ThankYou()));
    }
}
