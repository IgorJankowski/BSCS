namespace BSCS.Models
{
    /// <summary>
    /// Product from the external API.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Unique product identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Product name or title.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// URL-friendly slug for the product.
        /// </summary>
        public string Slug { get; set; } = string.Empty;

        /// <summary>
        /// Product price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Detailed product description.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Product category object.
        /// </summary>
        public Category? Category { get; set; }

        /// <summary>
        /// List of product images.
        /// </summary>
        public List<string> Images { get; set; } = new List<string>();

        /// <summary>
        /// Primary product image (first from images list or empty).
        /// </summary>
        public string Image { get; set; } = string.Empty;
    }
}
