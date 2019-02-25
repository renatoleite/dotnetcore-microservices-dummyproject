using System.Threading.Tasks;

namespace Dummy.Common.Mongo
{
    public interface IDatabaseSeeder
    {
        Task SeedAsync();
    }
}
