﻿using Xunit;

using MyTested.AspNetCore.Mvc;
using CarShop.Web.Controllers;
using CarShop.Web.Models.Dealers;

namespace CarShop.Tests.RoutingTests
{
    public class DealersRoutingTests
    {
        [Fact]
        public void GetBecomeShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Dealers/Become")
                .To<DealersController>(c => c.Become());

        [Fact]
        public void PostBecomeShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Dealers/Become")
                    .WithMethod(HttpMethod.Post))
                 .To<DealersController>(c => c.BecomeAsync(With.Any<DealerFormModel>()));

        [Fact]
        public void ThankYouShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Dealers/ThankYou")
                .To<DealersController>(c => c.ThankYou());
    }
}
