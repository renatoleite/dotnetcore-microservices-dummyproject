using System.Threading.Tasks;

namespace Dummy.Common.Mongo
{
    public interface IDatabaseInitializer
    {
        Task InitializeAsync();
    }
}
