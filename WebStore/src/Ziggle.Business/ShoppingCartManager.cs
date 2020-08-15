using System.Linq;
using Ziggle.Repository;

namespace Ziggle.Business
{
    public interface IShoppingCartManager
    {
        ShoppingCartModel Add(int userId, int productId, int quantity);
        bool Remove(int userId, int productId);
        ShoppingCartModel[] GetAll(int userId);
    }

    public class ShoppingCartModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
    }

    public class ShoppingCartManager : IShoppingCartManager
    {
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly IProductRepository productRepository;

        public ShoppingCartManager(IShoppingCartRepository shoppingCartRepository, IProductRepository productRepository)
        {
            this.productRepository = productRepository;
            this.shoppingCartRepository = shoppingCartRepository;
        }

        public ShoppingCartModel Add(int userId, int productId, int quantity)
        {
            var item = shoppingCartRepository.Add(userId, productId, quantity);

            return new ShoppingCartModel
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity
            };
        }

        public ShoppingCartModel[] GetAll(int userId)
        {
            var shoppingCartItem = shoppingCartRepository.GetAll(userId)
                .Select(t =>
                {
                    var product = productRepository.GetProduct(t.ProductId);
                    return new ShoppingCartModel
                    {
                        ProductId = t.ProductId,
                        ProductName = product.Name,
                        ProductPrice = product.Price,
                        Quantity = t.Quantity
                    };
                })
                .ToArray();

            return shoppingCartItem;
        }

        public bool Remove(int userId, int productId)
        {
            return shoppingCartRepository.Remove(userId, productId);
        }
    }
}