using BSCS.Clients;
using BSCS.Models;
using BSCS.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BSCS.Tests.Services
{
    /// <summary>
    /// Unit tests for ProductService
    /// </summary>
    public class ProductServiceTests
    {
        private readonly ProductService _productService;
        private readonly Mock<IProductApiClient> _mockApiClient;
        private readonly Mock<ILogger<ProductService>> _mockLogger;

        public ProductServiceTests()
        {
            _mockApiClient = new Mock<IProductApiClient>();
            _mockLogger = new Mock<ILogger<ProductService>>();
            _productService = new ProductService(_mockApiClient.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAllProductsAsync_ShouldReturnProducts()
        {
            // Arrange
            var mockProducts = new List<Product>
            {
                new Product { Id = 1, Title = "Product 1", Price = 50 },
                new Product { Id = 2, Title = "Product 2", Price = 100 }
            };

            _mockApiClient.Setup(x => x.GetAllProductsAsync())
                .ReturnsAsync(mockProducts);

            // Act
            var result = await _productService.GetAllProductsAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal("Product 1", result.First().Title);
        }

        [Fact]
        public async Task GetProductByIdAsync_WithValidId_ShouldReturnProduct()
        {
            // Arrange
            var mockProduct = new Product { Id = 1, Title = "Product 1", Price = 50 };

            _mockApiClient.Setup(x => x.GetProductByIdAsync(1))
                .ReturnsAsync(mockProduct);

            // Act
            var result = await _productService.GetProductByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Product 1", result.Title);
        }

        [Fact]
        public async Task GetProductByIdAsync_WithInvalidId_ShouldReturnNull()
        {
            // Arrange
            _mockApiClient.Setup(x => x.GetProductByIdAsync(999))
                .ReturnsAsync((Product?)null);

            // Act
            var result = await _productService.GetProductByIdAsync(999);

            // Assert
            Assert.Null(result);
        }
    }
}
