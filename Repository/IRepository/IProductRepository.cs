using ApiEcommerce.Models;
using ApiEcommerce.Models.Dtos;

namespace ApiEcommerce.Repository.IRepository;
    public interface IProductRepository
    {
    Product? GetProduct(int id);
    ICollection<Product> GetProducts();
    ICollection<Product> GetProductsForCategory(int CategoryId);

    bool BuyProduct(string name, int quantity);
    ICollection<Product> SearchProducts(string searchTerm);
    bool CreateProduct(Product product);
    bool UpdateProduct(Product product);
    bool DelteProduct(Product product);
    bool ProductExists(string name);
    bool ProductExists(int id);
    bool save();


}

