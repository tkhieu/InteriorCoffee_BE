using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InteriorCoffee.Domain.Models;

namespace InteriorCoffee.Infrastructure.Repositories.Base
{
    public abstract class BaseRepository<T> where T : class
    {
        protected IMongoClient _client;
        protected IMongoDatabase _database;

        public BaseRepository(IOptions<MongoDBContext> setting, IMongoClient client)
        {
            _client = client;
            _database = _client.GetDatabase(setting.Value.DatabaseName);
        }
    }
}
