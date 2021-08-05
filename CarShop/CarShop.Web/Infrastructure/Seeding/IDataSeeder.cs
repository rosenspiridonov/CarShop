namespace CarShop.Web.Infrastructure.Seeding
{
    using System.Threading.Tasks;

    public interface IDataSeeder
    {
        Task PopulateDb();
    }
}
