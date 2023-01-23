using System.Collections.Generic;

using CarShop.Web.Controllers;
using CarShop.Web.Models.Home;

using MyTested.AspNetCore.Mvc;

using Xunit;

using static CarShop.Tests.Data.Cars;

namespace CarShop.Tests.Pipeline
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectCarsCount()
            => MyMvc
                .Pipeline()
                .ShouldMap("/")
                .To<HomeController>(c => c.IndexAsync())
                .Which(c => c.WithData(FiveCars))
                .ShouldReturn()
                .View(v => v
                    .WithModelOfType<IndexViewModel>()
                    .Passing(m => m.CarsCount == 5));

        [Fact]
        public void ErrorShouldReturnCorrectView()
            => MyMvc
                .Pipeline()
                .ShouldMap("/Home/Error")
                .To<HomeController>(c => c.Error())
                .Which()
                .ShouldReturn()
                .View();
    }
}
