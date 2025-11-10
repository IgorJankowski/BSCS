using BSCS.Models;
using BSCS.Services;
using Microsoft.AspNetCore.Mvc;

namespace BSCS.Controllers
{
    /// <summary>
    /// Cart controller for managing shopping cart operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly ILogger<CartController> _logger;

        public CartController(ICartService cartService, IProductService productService, ILogger<CartController> logger)
        {
            _cartService = cartService;
            _productService = productService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves the current shopping cart with all items and summary.
        /// </summary>
        /// <returns>Returns the current cart containing items and total information.</returns>
        [HttpGet]
        public ActionResult<CartSummary> GetCart()
        {
            try
            {
                var cart = _cartService.GetCart();
                return Ok(cart);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving cart");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Adds a product to the shopping cart or increases quantity if already present.
        /// </summary>
        /// <param name="productId">The ID of the product to add.</param>
        /// <param name="quantity">Optional: The quantity to add (default is 1). Must be greater than 0.</param>
        /// <returns>Returns the updated cart.</returns>
        /// <example>
        /// POST /api/cart/add/5?quantity=2
        /// </example>
        [HttpPost("add/{productId}")]
        public async Task<ActionResult<CartSummary>> AddToCart(int productId, [FromQuery] int quantity = 1)
        {
            try
            {
                if (quantity <= 0)
                {
                    return BadRequest("Quantity must be greater than 0");
                }

                var product = await _productService.GetProductByIdAsync(productId);
                if (product == null)
                {
                    return NotFound("Product not found");
                }

                _cartService.AddToCart(product, quantity);
                var updatedCart = _cartService.GetCart();
                return Ok(updatedCart);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding product {ProductId} to cart", productId);
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Removes a product entirely from the shopping cart.
        /// </summary>
        /// <param name="productId">The ID of the product to remove.</param>
        /// <returns>Returns the updated cart after removal.</returns>
        /// <example>
        /// DELETE /api/cart/remove/5
        /// </example>
        [HttpDelete("remove/{productId}")]
        public ActionResult<CartSummary> RemoveFromCart(int productId)
        {
            try
            {
                _cartService.RemoveFromCart(productId);
                var updatedCart = _cartService.GetCart();
                return Ok(updatedCart);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing product {ProductId} from cart", productId);
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Updates the quantity of a product in the cart.
        /// </summary>
        /// <param name="productId">The ID of the product to update.</param>
        /// <param name="quantity">The new quantity. If 0, the product will be removed from cart.</param>
        /// <returns>Returns the updated cart.</returns>
        /// <example>
        /// PUT /api/cart/update/5?quantity=3
        /// </example>
        [HttpPut("update/{productId}")]
        public ActionResult<CartSummary> UpdateQuantity(int productId, [FromQuery] int quantity)
        {
            try
            {
                if (quantity < 0)
                {
                    return BadRequest("Quantity cannot be negative");
                }

                _cartService.UpdateQuantity(productId, quantity);
                var updatedCart = _cartService.GetCart();
                return Ok(updatedCart);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product {ProductId} quantity", productId);
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Clears all items from the shopping cart.
        /// </summary>
        /// <returns>No content on success.</returns>
        /// <example>
        /// DELETE /api/cart/clear
        /// </example>
        [HttpDelete("clear")]
        public ActionResult ClearCart()
        {
            try
            {
                _cartService.ClearCart();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error clearing cart");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
