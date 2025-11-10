namespace BSCS.Models
{
    /// <summary>
    /// Shopping cart item.
    /// </summary>
    public class CartItem
    {
        /// <summary>
        /// Cart item identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The product ID for this cart item.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Product name.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Product price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// How many of this product in cart.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Product image URL.
        /// </summary>
        public string Image { get; set; } = string.Empty;
    }
}
