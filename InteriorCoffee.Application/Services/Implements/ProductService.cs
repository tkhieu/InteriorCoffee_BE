using InteriorCoffee.Application.Services.Interfaces;
using InteriorCoffee.Domain.Models;
using InteriorCoffee.Infrastructure.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace InteriorCoffee.Application.Services.Implements
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            try
            {
                return await _productRepository.GetAllProductsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all products.");
                throw;
            }
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _logger.LogWarning("Invalid product ID.");
                throw new ArgumentException("Product ID cannot be null or empty.");
            }

            try
            {
                return await _productRepository.GetProductByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting product with id {id}.");
                throw;
            }
        }

        public async Task CreateProductAsync(Product product)
        {
            if (product == null)
            {
                _logger.LogWarning("Invalid product data.");
                throw new ArgumentException("Product cannot be null.");
            }

            try
            {
                await _productRepository.CreateProductAsync(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a product.");
                throw;
            }
        }

        public async Task UpdateProductAsync(string id, Product product)
        {
            if (string.IsNullOrEmpty(id) || product == null)
            {
                _logger.LogWarning("Invalid product ID or data.");
                throw new ArgumentException("Product ID and data cannot be null or empty.");
            }

            try
            {
                await _productRepository.UpdateProductAsync(id, product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating product with id {id}.");
                throw;
            }
        }

        public async Task DeleteProductAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _logger.LogWarning("Invalid product ID.");
                throw new ArgumentException("Product ID cannot be null or empty.");
            }

            try
            {
                await _productRepository.DeleteProductAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting product with id {id}.");
                throw;
            }
        }
    }
}
