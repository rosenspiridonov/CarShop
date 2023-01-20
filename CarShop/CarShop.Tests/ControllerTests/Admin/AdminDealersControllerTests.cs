using Xunit;
using MyTested.AspNetCore.Mvc;
using CarShop.Web.Areas.Admin.Controllers;

using static CarShop.Web.WebConstants;
using CarShop.Web.Services.Dealers;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CarShop.Tests.ControllerTests.Admin
{
    public class AdminRequestControllerTests
    {
        [Fact]
        public void GetRequestShouldBeForAdminsAndReturnView()
            => MyController<DealersController>
                .Instance(instance => instance
                    .WithUser(AdminRoleName))
                .Calling(c => c.Requests())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<DealerServiceModel>>());

        [Fact]
        public void PostRequestShouldBeForAdminsAndReturnView()
            => MyController<DealersController>
                .Instance(instance => instance
                    .WithData(new IdentityUser()
                    {
                        Id = TestUser.Identifier,
                        UserName = TestUser.Username,
                        PhoneNumber = "1234567890"
                    })
                    .WithData(new IdentityRole()
                    {
                        Id = "TestDealerRole",
                        Name = DealerRoleName,
                        NormalizedName = DealerRoleName.ToUpper()
                    })
                    .WithUser(AdminRoleName))
                .Calling(c => c.Requests(TestUser.Identifier))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<DealerServiceModel>>());
    }
}
