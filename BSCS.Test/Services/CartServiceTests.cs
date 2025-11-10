using BSCS.Models;
using BSCS.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BSCS.Tests.Services
{
    /// <summary>
    /// Unit tests for CartService
    /// </summary>
    public class CartServiceTests
    {
        private readonly CartService _cartService;
        private readonly Mock<ILogger<CartService>> _mockLogger;

        public CartServiceTests()
        {
            _mockLogger = new Mock<ILogger<CartService>>();
            _cartService = new CartService(_mockLogger.Object);
        }

        [Fact]
        public void AddToCart_WithNewProduct_ShouldAddItemToCart()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Title = "Test Product",
                Price = 50,
                Description = "Test Description",
                Images = new List<string> { "http://test.jpg" }
            };

            // Act
            _cartService.AddToCart(product, 1);
            var cart = _cartService.GetCart();

            // Assert
            Assert.NotEmpty(cart.Items);
            Assert.Single(cart.Items);
            Assert.Equal(1, cart.Items[0].ProductId);
            Assert.Equal("Test Product", cart.Items[0].Title);
            Assert.Equal(1, cart.TotalItems);
            Assert.Equal(50, cart.TotalPrice);
        }

        [Fact]
        public void AddToCart_WithExistingProduct_ShouldIncreaseQuantity()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Title = "Test Product",
                Price = 50,
                Description = "Test Description",
                Images = new List<string> { "http://test.jpg" }
            };

            // Act
            _cartService.AddToCart(product, 1);
            _cartService.AddToCart(product, 2);
            var cart = _cartService.GetCart();

            // Assert
            Assert.Single(cart.Items);
            Assert.Equal(3, cart.Items[0].Quantity);
            Assert.Equal(3, cart.TotalItems);
            Assert.Equal(150, cart.TotalPrice);
        }

        [Fact]
        public void RemoveFromCart_ShouldRemoveProduct()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Title = "Test Product",
                Price = 50,
                Description = "Test Description",
                Images = new List<string> { "http://test.jpg" }
            };

            _cartService.AddToCart(product, 1);

            // Act
            _cartService.RemoveFromCart(1);
            var cart = _cartService.GetCart();

            // Assert
            Assert.Empty(cart.Items);
            Assert.Equal(0, cart.TotalItems);
            Assert.Equal(0, cart.TotalPrice);
        }

        [Fact]
        public void UpdateQuantity_ShouldChangeQuantity()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Title = "Test Product",
                Price = 50,
                Description = "Test Description",
                Images = new List<string> { "http://test.jpg" }
            };

            _cartService.AddToCart(product, 1);

            // Act
            _cartService.UpdateQuantity(1, 5);
            var cart = _cartService.GetCart();

            // Assert
            Assert.Single(cart.Items);
            Assert.Equal(5, cart.Items[0].Quantity);
            Assert.Equal(5, cart.TotalItems);
            Assert.Equal(250, cart.TotalPrice);
        }

        [Fact]
        public void ClearCart_ShouldRemoveAllItems()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Title = "Test Product",
                Price = 50,
                Description = "Test Description",
                Images = new List<string> { "http://test.jpg" }
            };

            _cartService.AddToCart(product, 2);

            // Act
            _cartService.ClearCart();
            var cart = _cartService.GetCart();

            // Assert
            Assert.Empty(cart.Items);
            Assert.Equal(0, cart.TotalItems);
            Assert.Equal(0, cart.TotalPrice);
        }
    }
}
