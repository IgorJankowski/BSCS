using BSCS.Models;

namespace BSCS.Services
{
    public class CartService : ICartService
    {
        private readonly CartSummary _cart;
        private readonly ILogger<CartService> _logger;
        private int _nextCartItemId = 1;

        public CartService(ILogger<CartService> logger)
        {
            _logger = logger;
            _cart = new CartSummary();
        }

        public CartSummary GetCart()
        {
            _cart.RecalculateTotals();
            return _cart;
        }

        public void AddToCart(Product product, int quantity = 1)
        {
            try
            {
                var existingItem = _cart.Items.FirstOrDefault(x => x.ProductId == product.Id);

                if (existingItem != null)
                {
                    existingItem.Quantity += quantity;
                }
                else
                {
                    _cart.Items.Add(new CartItem
                    {
                        Id = _nextCartItemId++,
                        ProductId = product.Id,
                        Title = product.Title,
                        Price = product.Price,
                        Image = product.Image,
                        Quantity = quantity
                    });
                }

                _cart.RecalculateTotals();
                _logger.LogInformation("Added {Quantity} of product {Title} to cart", quantity, product.Title);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding product to cart");
            }
        }

        public void RemoveFromCart(int productId)
        {
            try
            {
                var item = _cart.Items.FirstOrDefault(x => x.ProductId == productId);
                if (item != null)
                {
                    _cart.Items.Remove(item);
                    _cart.RecalculateTotals();
                    _logger.LogInformation("Removed product {ProductId} from cart", productId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing product from cart");
            }
        }

        public void UpdateQuantity(int productId, int quantity)
        {
            try
            {
                var item = _cart.Items.FirstOrDefault(x => x.ProductId == productId);
                if (item != null)
                {
                    if (quantity <= 0)
                    {
                        RemoveFromCart(productId);
                    }
                    else
                    {
                        item.Quantity = quantity;
                        _cart.RecalculateTotals();
                        _logger.LogInformation("Updated quantity of product {ProductId} to {Quantity}", productId, quantity);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product quantity");
            }
        }

        public void ClearCart()
        {
            try
            {
                _cart.Items.Clear();
                _cart.RecalculateTotals();
                _logger.LogInformation("Cart cleared");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error clearing cart");
            }
        }
    }
}
