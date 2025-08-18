using ApiEcommerce.Data;
using ApiEcommerce.Models;
using ApiEcommerce.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ApiEcommerce.Repository;
    public class CategoryRepository:ICategoryRepository
    {

    private readonly ApplicationDbContext _DbContext;
    public CategoryRepository(ApplicationDbContext dbContext)
    {
        _DbContext = dbContext;
    }

public ICollection<Category> GetCategories()
    {
        return _DbContext.Categories.OrderBy(x=> x.Name).ToList();
    }

    public Category GetCategory(int id)
    {
        return _DbContext.Categories.FirstOrDefault(category => category.Id == id) ?? throw new  InvalidOperationException($"La categoria del {id} no existe");
            
    }

    public bool CategoryExists(int id)
    {
        return _DbContext.Categories.Any(category => category.Id==id);
    }

    public bool CategoryExits(string name)
    {
        return _DbContext.Categories.Any(category => category.Name.ToLower().Trim() == name.ToLower().Trim());
    }

    public  bool CreateCategory(Category category)
    {
        category.CreationDate = DateTime.Now;
          _DbContext.Categories.Add(category);
      return  Save();
    }

    public bool UpdateCategory(Category category)
    {
        category.CreationDate = DateTime.Now;
        _DbContext.Categories.Update(category);
        return Save();
    }

    public bool DeeleteCategory(Category category)
    {
        _DbContext.Categories.Remove(category);
        return Save();
    }

    public bool Save( )
    {
        return _DbContext.SaveChanges() > 0? true:false;
    }
}

