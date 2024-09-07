using InteriorCoffee.Domain.Models;
using InteriorCoffee.Infrastructure.Repositories.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace InteriorCoffee.Infrastructure.Repositories.Implements
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(IMongoDatabase database, ILogger<ProductRepository> logger)
        {
            _products = database.GetCollection<Product>("Product");
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            try
            {
                return await _products.Find(product => true).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all products.");
                throw;
            }
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            try
            {
                return await _products.Find<Product>(product => product._id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting product with id {id}.");
                throw;
            }
        }

        public async Task CreateProductAsync(Product product)
        {
            try
            {
                await _products.InsertOneAsync(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a product.");
                throw;
            }
        }

        public async Task UpdateProductAsync(string id, Product product)
        {
            try
            {
                await _products.ReplaceOneAsync(prod => prod._id == id, product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating product with id {id}.");
                throw;
            }
        }

        public async Task DeleteProductAsync(string id)
        {
            try
            {
                await _products.DeleteOneAsync(product => product._id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting product with id {id}.");
                throw;
            }
        }
    }
}
