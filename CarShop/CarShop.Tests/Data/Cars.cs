namespace CarShop.Tests.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using CarShop.Web.Data.Models;

    public static class Cars
    {
        public static IEnumerable<Car> FiveCars
            => Enumerable.Range(0, 5).Select(x => new Car());
    }
}
