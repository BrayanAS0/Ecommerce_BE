using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce_BE.Domain.Entities
{
    public class ProductRating
    {
        public int Id { get; set; }
        public int ProductId { get; set; }            
        public string UserId { get; set; }
        [Range(1,5)]
        public int Rating { get; set; }                
        public string ReviewText { get; set; }        
        public DateTime CreatedAt { get; set; }     
        public bool IsApproved { get; set; }          

        public Product Product { get; set; }
        public IdentityUser User { get; set; }
    }

}
