using ApiEcommerce.Data;
using ApiEcommerce.Models;
using ApiEcommerce.Models.Dtos;
using ApiEcommerce.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ApiEcommerce.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductRepository(ApplicationDbContext dbcontext) {
       _dbContext = dbcontext;
        }
    public    bool BuyProduct(string name, int quantity)
        {
            if (string.IsNullOrWhiteSpace(name) || quantity <= 0) return false;

            if (!ProductExists(name)) return false;

            var product = _dbContext.Products.First(p => p.Name.ToLower() == name.ToLower());

            if(product.Stock> quantity) return false;

            product.Stock = product.Stock- quantity;
            _dbContext.Products.Update(product);
            return save();

        }

        public bool CreateProduct(Product product)
        {
            if(product== null) return false;
            _dbContext.Add(product);
            return save();
        }

        public bool DelteProduct(Product createProductDto)
        {
            if (ProductExists(createProductDto.ProductId) || createProductDto ==null) return false;
            _dbContext.Remove(createProductDto);
            return save();
        }

        public Product? GetProduct(int id)
        {
            if (id <= 0) return null;
          return   _dbContext.Products.Include(p=> p.Category).First(x => x.ProductId == id);
        }

        public ICollection<Product> GetProducts()
        {
            return _dbContext.Products.Include(p => p.Category).ToList();
        }

        public ICollection<Product> GetProductsForCategory(int CategoryId)
        {
            return _dbContext.Products.Where(x => x.CategoryId == CategoryId).ToList();
        }

        public bool ProductExists(string name)
        {
            return _dbContext.Products.Any(product => product.Name.ToLower().Trim() == name.ToLower().Trim());
        }

        public bool ProductExists(int id)
        {
            return _dbContext.Products.Any(product => product.ProductId == id);
        }

        public bool save()
        {
           return _dbContext.SaveChanges() >0 ?true :false;
        }

        public bool UpdateProduct(Product createProductDto)
        {
            createProductDto.UpdateDate = DateTime.Now;
            _dbContext.Products.Update(createProductDto);
            return save();
        }

   public      ICollection<Product> SearchProducts(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm)) return _dbContext.Products.ToList();
          return   _dbContext.Products
                .Where(x=> x.Name.ToLower()
                .Trim()
                .Contains(searchTerm.ToLower()
                .Trim()) || x.Description.ToLower().Trim().Contains(searchTerm.ToLower().Trim()
                
                )
                )
                .Include(p=> p.Category)
                .ToList();
        }
    }
}
