namespace CarShop.Tests.Mocks
{
    using System.Collections.Generic;

    using CarShop.Web.Data;
    using CarShop.Web.Services.Admin;
    using CarShop.Web.Services.Dealers;

    using MyTested.AspNetCore.Mvc;

    public class AdminServiceMock : AdminService
    {
        public AdminServiceMock(ApplicationDbContext db, IDealersService dealersService)
            : base(db, dealersService)
        {
        }

        public override IEnumerable<DealerServiceModel> DealersPendingRequests()
        {
            return new List<DealerServiceModel>()
            {
                new DealerServiceModel()
                {
                    Id = TestUser.Identifier,
                    Email = "test@test.com",
                    Name = TestUser.Username,
                    PhoneNumber = "0123456789"
                }
            };
        }

        public override void ApproveDealer(string userId)
        {
            // Do nothing
        }
    }
}
