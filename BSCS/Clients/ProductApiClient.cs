using BSCS.Models;
using System.Text.Json;

namespace BSCS.Clients
{
    public class ProductApiClient : IProductApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProductApiClient> _logger;

        public ProductApiClient(HttpClient httpClient, ILogger<ProductApiClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("products");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var products = JsonSerializer.Deserialize<List<Product>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                _logger.LogInformation("Successfully fetched all products");
                return products ?? new List<Product>();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching all products from API");
                return new List<Product>();
            }
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"products/{id}");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var product = JsonSerializer.Deserialize<Product>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                _logger.LogInformation("Successfully fetched product with id {ProductId}", id);
                return product;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching product with id {ProductId} from API", id);
                return null;
            }
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string? title = null, int? categoryId = null, decimal? priceMin = null, decimal? priceMax = null)
        {
            try
            {
                var queryParams = new List<string>();

                if (!string.IsNullOrWhiteSpace(title))
                {
                    queryParams.Add($"title={Uri.EscapeDataString(title)}");
                }

                if (categoryId.HasValue)
                {
                    queryParams.Add($"categoryId={categoryId}");
                }

                if (priceMin.HasValue)
                {
                    queryParams.Add($"price_min={priceMin}");
                }

                if (priceMax.HasValue)
                {
                    queryParams.Add($"price_max={priceMax}");
                }

                var query = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";
                var url = $"products{query}";

                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var products = JsonSerializer.Deserialize<List<Product>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                _logger.LogInformation("Successfully fetched products with search filters");
                return products ?? new List<Product>();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error searching products from API");
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("categories");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var categories = JsonSerializer.Deserialize<List<Category>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                _logger.LogInformation("Successfully fetched all categories");
                return categories ?? new List<Category>();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching categories from API");
                return new List<Category>();
            }
        }
    }
}
