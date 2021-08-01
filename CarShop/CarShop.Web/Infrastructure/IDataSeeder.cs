namespace CarShop.Infrastructure
{
    using System.Threading.Tasks;

    public interface IDataSeeder
    {
        Task PopulateDb();
    }
}
