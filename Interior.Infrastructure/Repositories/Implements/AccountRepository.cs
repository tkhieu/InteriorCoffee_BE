using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InteriorCoffee.Domain.Models;
using InteriorCoffee.Infrastructure.Repositories.Interfaces;
using InteriorCoffee.Infrastructure.Repositories.Base;

namespace InteriorCoffee.Infrastructure.Repositories.Implements
{
    public class AccountRepository : BaseRepository<AccountRepository>, IAccountRepository
    {
        private readonly IMongoCollection<Account> _accounts;
        public AccountRepository(IOptions<MongoDBContext> setting, IMongoClient client) : base(setting, client)
        {
            _accounts = _database.GetCollection<Account>("Account");
        }

        public async Task<List<Account>> GetAccountList()
        {
            return await _accounts.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Account> GetAccountById(string id)
        {
            return await _accounts.Find(c => c._id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAccount(Account account)
        {
            await _accounts.ReplaceOneAsync(a => a._id == account._id, account);
        }

        public async Task CreateAccount(Account account)
        {
            await _accounts.InsertOneAsync(account);
        }

        public async Task DeleteAccount(string id)
        {
            FilterDefinition<Account> filterDefinition = Builders<Account>.Filter.Eq("_id", id);
            await _accounts.DeleteOneAsync(filterDefinition);
            return;
        }
    }
}
