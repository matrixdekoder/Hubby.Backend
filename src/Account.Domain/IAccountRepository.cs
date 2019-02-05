using System.Threading.Tasks;
using Core.Domain;

namespace Account.Domain
{
    public interface IAccountRepository
    {
        Task<Account> GetByUserName(string username);
    }
}
