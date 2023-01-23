using Xunit;

using MyTested.AspNetCore.Mvc;
using CarShop.Web.Controllers;
using CarShop.Web.Models.Cars;

namespace CarShop.Tests.RoutingTests
{
    public class CarsRountingTests
    {
        [Fact]
        public void SearchShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Cars/Search")
                .To<CarsController>(c => c.SearchAsync());

        [Fact]
        public void GetCreateShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Cars/Create")
                .To<CarsController>(c => c.CreateAsync());

        [Fact]
        public void PostCreateShouldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap(request => request
                   .WithPath("/Cars/Create")
                   .WithMethod(HttpMethod.Post))
                .To<CarsController>(c => c.CreateAsync(With.Any<CarFormModel>()));

        [Fact]
        public void DetailsShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Cars/Details")
                    .WithQuery("id", With.Any<int>().ToString()))
                .To<CarsController>(c => c.DetailsAsync(With.Any<int>()));

        [Fact]
        public void GetEditShouldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap(request => request
                   .WithPath("/Cars/Edit")
                   .WithQuery("id", With.Any<int>().ToString()))
               .To<CarsController>(c => c.EditAsync(With.Any<int>()));

        [Fact]
        public void PostEditShouldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap(request => request
                   .WithPath("/Cars/Edit")
                   .WithMethod(HttpMethod.Post))
                .To<CarsController>(c => c.EditAsync(With.Any<CarFormModel>()));

        [Fact]
        public void DeleteShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap(request => request
                  .WithPath("/Cars/Delete")
                  .WithQuery("id", With.Any<int>().ToString()))
              .To<CarsController>(c => c.DeleteAsync(With.Any<int>()));

    }
}
