using System.Threading.Tasks;

namespace Core.Api
{
    public interface ISeeder
    {
        Task Seed();
    }
}
