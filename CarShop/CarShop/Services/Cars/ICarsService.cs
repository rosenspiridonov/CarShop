namespace CarShop.Services.Cars
{
    using System.Collections.Generic;

    public interface ICarsService
    {
        IEnumerable<CarServiceModel> GetCars(int start, int count);
    }
}
