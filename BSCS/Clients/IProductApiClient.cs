using BSCS.Models;

namespace BSCS.Clients
{
    /// <summary>
    /// Interface for Product API Client
    /// </summary>
    public interface IProductApiClient
    {
        /// <summary>
        /// Gets all products from the API
        /// </summary>
        /// <returns>Collection of products</returns>
        Task<IEnumerable<Product>> GetAllProductsAsync();

        /// <summary>
        /// Gets a specific product by ID
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>Product if found, null otherwise</returns>
        Task<Product?> GetProductByIdAsync(int id);

        /// <summary>
        /// Searches products with optional filters
        /// </summary>
        /// <param name="title">Product title filter</param>
        /// <param name="categoryId">Category ID filter</param>
        /// <param name="priceMin">Minimum price filter</param>
        /// <param name="priceMax">Maximum price filter</param>
        /// <returns>Filtered collection of products</returns>
        Task<IEnumerable<Product>> SearchProductsAsync(string? title = null, int? categoryId = null, decimal? priceMin = null, decimal? priceMax = null);

        /// <summary>
        /// Gets all categories from the API
        /// </summary>
        /// <returns>Collection of categories</returns>
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
    }
}
