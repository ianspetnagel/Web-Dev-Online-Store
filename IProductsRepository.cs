using ProductsAppRPSpetnagel.Models;

namespace ProductsAppRPSpetnagel.Repositories
{
    public interface IProductsRepository
    {
        bool AddProduct(Product pr);
        bool ApplyDiscount(int prodid, double amt);
        bool DeleteProduct(int prodid);
        List<Category> GetCategories();
        List<Product> GetProductsByCategory(int catid);
        Product GetProductById(int prodid);
        bool IncreasePrice(int prodid, double amt);
        bool UpdateProduct(Product pr);
    }
}
