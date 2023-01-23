using System.Collections.Generic;
using System.Threading.Tasks;

using CarShop.Web.Data;
using CarShop.Web.Services.Admin;
using CarShop.Web.Services.Dealers;

using MyTested.AspNetCore.Mvc;

namespace CarShop.Tests.Mocks
{
    public class AdminServiceMock : AdminService
    {
        public AdminServiceMock(ApplicationDbContext db, IDealersService dealersService)
            : base(db, dealersService)
        {
        }

        public override async Task<IEnumerable<DealerServiceModel>> DealersPendingRequestsAsync()
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

        public override async Task ApproveDealerAsync(string userId)
        {
            // Do nothing
        }
    }
}
