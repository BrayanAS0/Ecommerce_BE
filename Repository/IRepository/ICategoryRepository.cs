using ApiEcommerce.Models;

namespace ApiEcommerce.Repository.IRepository;
    public interface ICategoryRepository
    {
    ICollection<Category> GetCategories();
    Category GetCategory(int id);
    bool CategoryExists(int id);
    bool CategoryExits(string name);
    bool CreateCategory(Category category);
    bool UpdateCategory(Category category);
    bool DeeleteCategory(Category category);
    bool Save();



    }

