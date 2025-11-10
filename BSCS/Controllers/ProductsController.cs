using BSCS.Models;
using BSCS.Services;
using Microsoft.AspNetCore.Mvc;

namespace BSCS.Controllers
{
    /// <summary>
    /// Products controller for managing product retrieval and search operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all available products.
        /// </summary>
        /// <returns>Returns a collection of all products from the API.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all products");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Retrieves a specific product by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the product.</param>
        /// <returns>Returns the product with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving product with id {ProductId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Searches for products with optional filters.
        /// </summary>
        /// <param name="title">Optional: Filter products by title (partial match).</param>
        /// <param name="categoryId">Optional: Filter products by category ID.</param>
        /// <param name="price_min">Optional: Minimum price filter (inclusive).</param>
        /// <param name="price_max">Optional: Maximum price filter (inclusive).</param>
        /// <returns>Returns list of products matching the search criteria.</returns>
        /// <example>
        /// GET /api/products/search?title=shirt&amp;price_min=67&amp;price_max=2137&amp;categoryId=1
        /// </example>
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProducts(
            [FromQuery] string? title = null,
            [FromQuery] int? categoryId = null,
            [FromQuery] decimal? price_min = 0,
            [FromQuery] decimal? price_max = decimal.MaxValue)
        {
            try
            {
                var products = await _productService.SearchProductsAsync(title, categoryId, price_min, price_max);
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching products");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Retrieves all available product categories.
        /// </summary>
        /// <returns>Returns a collection of all categories.</returns>
        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
        {
            try
            {
                var categories = await _productService.GetAllCategoriesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving categories");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
