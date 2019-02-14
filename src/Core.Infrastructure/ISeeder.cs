using System.Threading.Tasks;

namespace Core.Infrastructure
{
    public interface ISeeder
    {
        Task Seed();
    }
}
