using BlazorAppShoppingCart.Models;

namespace BlazorAppShoppingCart.Services
{
    public interface ICartService
    {
        IList<Product> Cart { get; }

        int Total { get; set; }

        event Action OnChange;

        void AddProduct(Product product);

        void DeleteProduct(Product product);
    }
}
