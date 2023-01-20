using System.Threading.Tasks;

namespace CarShop.Web.Infrastructure.Seeding
{
    public interface IDataSeeder
    {
        Task PopulateDb();
    }
}
