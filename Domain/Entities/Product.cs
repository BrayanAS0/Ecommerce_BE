namespace Ecommerce_BE.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }                   
        public string Name { get; set; }           
        public string Description { get; set; }     
        public decimal Price { get; set; }          
        public string SKU { get; set; }       
        public int StockQuantity { get; set; }     
        public int CategoryId { get; set; }           
        public string ImageUrl { get; set; }        
        public bool IsActive { get; set; }          
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Category Category { get; set; }
        public ICollection<ProductRating> Ratings { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }

}
