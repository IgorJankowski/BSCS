using BSCS.Clients;
using BSCS.Models;

namespace BSCS.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductApiClient _apiClient;
        private readonly ILogger<ProductService> _logger;

        public ProductService(ProductApiClient apiClient, ILogger<ProductService> logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            try
            {
                return await _apiClient.GetAllProductsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all products");
                return Enumerable.Empty<Product>();
            }
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            try
            {
                return await _apiClient.GetProductByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching product with id {ProductId}", id);
                return null;
            }
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string? title = null, int? categoryId = null, decimal? priceMin = null, decimal? priceMax = null)
        {
            try
            {
                return await _apiClient.SearchProductsAsync(title, categoryId, priceMin, priceMax);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching products");
                return Enumerable.Empty<Product>();
            }
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            try
            {
                return await _apiClient.GetAllCategoriesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching categories");
                return Enumerable.Empty<Category>();
            }
        }
    }
}
