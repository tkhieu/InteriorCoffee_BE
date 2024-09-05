using InteriorCoffee.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InteriorCoffee.Infrastructure.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        public Task<List<Account>> GetAccountListAsync(Expression<Func<Account, bool>> predicate = null, Expression<Func<Account, object>> orderBy = null);
        public Task<Account> GetAccountAsync(Expression<Func<Account, bool>> predicate = null, Expression<Func<Account, object>> orderBy = null);

        public Task<List<Account>> GetAccountList();
        public Task<Account> GetAccountById(string id);
        public Task UpdateAccount(Account account);
        public Task CreateAccount(Account account);
        public Task DeleteAccount(string id);
    }
}
