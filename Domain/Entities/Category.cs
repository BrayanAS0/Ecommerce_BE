namespace Ecommerce_BE.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }                   
        public string Name { get; set; }              
        public string Description { get; set; }  
        public int? ParentCategoryId { get; set; }     
        public bool IsActive { get; set; }          
        public DateTime CreatedAt { get; set; }

        public Category ParentCategory { get; set; }          
        public ICollection<Category> SubCategories { get; set; }  
        public ICollection<Product> Products { get; set; }    
    }

}
