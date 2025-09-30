using Ecommerce_BE.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce_BE.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }                    
        public string UserId { get; set; }           
        public string OrderNumber { get; set; }      
        public decimal TotalAmount { get; set; }     
        public OrderStatus Status { get; set; }      
        //public PaymentStatus PaymentStatus { get; set; }
        public DateTime CreatedAt { get; set; }        
        public DateTime? DeliveredAt { get; set; }     

        public IdentityUser User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }

}
