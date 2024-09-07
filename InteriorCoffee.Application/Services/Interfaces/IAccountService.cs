using InteriorCoffee.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InteriorCoffee.Application.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<Account> GetAccountByIdAsync(string id);
        Task CreateAccountAsync(Account account);
        Task UpdateAccountAsync(string id, Account account);
        Task DeleteAccountAsync(string id);
    }
}
