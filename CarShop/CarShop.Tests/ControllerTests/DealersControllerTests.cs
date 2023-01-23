using System.Linq;

using CarShop.Web.Controllers;
using CarShop.Web.Data;
using CarShop.Web.Models.Dealers;
using CarShop.Web.Data.Models;

using Microsoft.AspNetCore.Identity;

using Xunit;
using MyTested.AspNetCore.Mvc;

using static CarShop.Web.WebConstants;
using CarShop.Web.Models.Sorting;
using System.Threading.Tasks;

namespace CarShop.Tests.ControllerTests
{
    public class DealersControllerTests
    {
        [Fact]
        public void GetBecomeShouldBeForAuthorizedUsersAndReturnView()
            => MyController<DealersController>
                .Instance(instance => instance
                    .WithUser(user => user
                        .WithIdentifier(TestUser.Identifier)
                        .InRole(UserRoleName)))
                .Calling(c => c.Become())
                .ShouldHave()
                .ActionAttributes(a => a
                    .RestrictingForAuthorizedRequests(UserRoleName))
                .AndAlso()
                .ShouldReturn()
                .View();

        [Theory]
        [InlineData("0123456789")]
        public void PostBecomeShouldBeForAuthorizedUsersAndReturnRedirect(string phoneNumber)
            => MyController<DealersController>
                .Instance(instance => instance
                    .WithUser())
                .Calling(c => c.BecomeAsync(new DealerFormModel 
                {
                    PhoneNumber = phoneNumber
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data => data
                    .WithSet<DealerRequest>(dr => dr
                        .Any(x => x
                            .UserId == TestUser.Identifier)))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<DealersController>(c => c.ThankYou()));

        //[Theory]
        //[InlineData(TestUser.Identifier)]
        //public async Task MyCarsShouldBeForAuthorizedUsersAndReturnCorrectView(string userId)
        //{
        //    var user = new IdentityUser()
        //    {
        //        Id = TestUser.Identifier,
        //    };

        //    db.Users.Add(user);
        //    db.SaveChanges();

        //    await userManager.AddToRoleAsync(user, DealerRoleName);

        //    MyController<DealersController>
        //        .Instance(instance => instance
        //            .WithUser(user => user
        //                .WithIdentifier(TestUser.Identifier)
        //                .InRole(DealerRoleName)))
        //        .Calling(actionCall: c => c.MyCars(userId));
        //}
    }
}
