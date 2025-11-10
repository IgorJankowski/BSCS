using BSCS.Models;

namespace BSCS.Services
{
    public interface ICartService
    {
        CartSummary GetCart();
        void AddToCart(Product product, int quantity = 1);
        void RemoveFromCart(int productId);
        void UpdateQuantity(int productId, int quantity);
        void ClearCart();
    }
}
