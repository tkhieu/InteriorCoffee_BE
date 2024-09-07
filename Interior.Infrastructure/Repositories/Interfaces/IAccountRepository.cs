using InteriorCoffee.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InteriorCoffee.Infrastructure.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAccountList();
        Task<Account> GetAccountById(string id);
        Task CreateAccount(Account account);
        Task UpdateAccount(Account account);
        Task DeleteAccount(string id);
    }
}
