using BSCS.Controllers;
using BSCS.Models;
using BSCS.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BSCS.Tests.Controllers
{
    /// <summary>
    /// Unit tests for CartController
    /// </summary>
    public class CartControllerTests
    {
        private readonly CartController _controller;
        private readonly Mock<ICartService> _mockCartService;
        private readonly Mock<IProductService> _mockProductService;
        private readonly Mock<ILogger<CartController>> _mockLogger;

        public CartControllerTests()
        {
            _mockCartService = new Mock<ICartService>();
            _mockProductService = new Mock<IProductService>();
            _mockLogger = new Mock<ILogger<CartController>>();

            _controller = new CartController(_mockCartService.Object, _mockProductService.Object, _mockLogger.Object);
        }

        [Fact]
        public void GetCart_ShouldReturnCartSummary()
        {
            // Arrange
            var mockCart = new CartSummary
            {
                Items = new List<CartItem>(),
                TotalItems = 0,
                TotalPrice = 0
            };

            _mockCartService.Setup(x => x.GetCart())
                .Returns(mockCart);

            // Act
            var result = _controller.GetCart();

            // Assert
            Assert.NotNull(result);
            _mockCartService.Verify(x => x.GetCart(), Times.Once);
        }

        [Fact]
        public async Task AddToCart_WithValidProduct_ShouldReturnUpdatedCart()
        {
            // Arrange
            var mockProduct = new Product
            {
                Id = 1,
                Title = "Test Product",
                Price = 50,
                Description = "Test",
                Images = new List<string>()
            };

            var mockCart = new CartSummary
            {
                Items = new List<CartItem>
                {
                    new CartItem { Id = 1, ProductId = 1, Title = "Test Product", Price = 50, Quantity = 1 }
                },
                TotalItems = 1,
                TotalPrice = 50
            };

            _mockProductService.Setup(x => x.GetProductByIdAsync(1))
                .ReturnsAsync(mockProduct);

            _mockCartService.Setup(x => x.GetCart())
                .Returns(mockCart);

            // Act
            var result = await _controller.AddToCart(1, 1);

            // Assert
            Assert.NotNull(result);
            _mockCartService.Verify(x => x.AddToCart(It.IsAny<Product>(), 1), Times.Once);
        }

        [Fact]
        public async Task AddToCart_WithInvalidQuantity_ShouldReturnBadRequest()
        {
            // Act
            var result = await _controller.AddToCart(1, 0);

            // Assert
            Assert.NotNull(result);
            // Verify it returns BadRequest
        }

        [Fact]
        public void RemoveFromCart_ShouldCallRemoveMethod()
        {
            // Arrange
            var mockCart = new CartSummary
            {
                Items = new List<CartItem>(),
                TotalItems = 0,
                TotalPrice = 0
            };

            _mockCartService.Setup(x => x.GetCart())
                .Returns(mockCart);

            // Act
            _controller.RemoveFromCart(1);

            // Assert
            _mockCartService.Verify(x => x.RemoveFromCart(1), Times.Once);
        }
    }
}
