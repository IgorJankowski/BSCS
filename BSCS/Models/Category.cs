namespace BSCS.Models
{
    /// <summary>
    /// Represents a product category from the external API.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Category identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Category name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// URL-friendly category slug.
        /// </summary>
        public string Slug { get; set; } = string.Empty;

        /// <summary>
        /// Category image URL.
        /// </summary>
        public string Image { get; set; } = string.Empty;
    }
}
