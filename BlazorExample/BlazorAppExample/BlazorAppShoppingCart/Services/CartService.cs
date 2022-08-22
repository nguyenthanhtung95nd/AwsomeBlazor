using BlazorAppShoppingCart.Models;

namespace BlazorAppShoppingCart.Services
{
    public class CartService : ICartService
    {
        public IList<Product> Cart { get; private set; }
        public int Total { get; set; }

        public event Action OnChange;

        public CartService() { Cart = new List<Product>(); }

        /// <summary>
        /// In the preceding code, the OnChange event is invoked when the NotifyStateChanged method is called.
        /// </summary>
        private void NotifyStateChanged() => OnChange?.Invoke();

        /// <summary>
        /// The preceding code adds the indicated product to the list of products and increments the total.It also calls the NotifyStateChanged method.
        /// </summary>
        /// <param name="product"></param>
        public void AddProduct(Product product)
        {
            Cart.Add(product);
            Total += product.Price;
            NotifyStateChanged();
        }

        /// <summary>
        /// The preceding code adds the indicated product to the list of products and increments the total.It also calls the NotifyStateChanged method.
        /// </summary>
        /// <param name="product"></param>
        public void DeleteProduct(Product product)
        {
            Cart.Remove(product);
            Total -= product.Price;
            NotifyStateChanged();
        }
    }
}
