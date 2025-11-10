using BSCS.Models;

namespace BSCS.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> SearchProductsAsync(string? title = null, int? categoryId = null, decimal? priceMin = null, decimal? priceMax = null);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
    }
}
