namespace BSCS.Models
{
    /// <summary>
    /// Represents a summary of the shopping cart including items and totals.
    /// </summary>
    public class CartSummary
    {
        /// <summary>
        /// Items in the cart.
        /// </summary>
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        /// <summary>
        /// Total price of all items (price × quantity for each).
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Total number of items in cart (sum of all quantities).
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// Recalculates the total price and total items based on current cart items.
        /// </summary>
        public void RecalculateTotals()
        {
            TotalItems = Items.Sum(x => x.Quantity);
            TotalPrice = Items.Sum(x => x.Price * x.Quantity);
        }
    }
}
