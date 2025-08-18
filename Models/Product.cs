using System.ComponentModel.DataAnnotations;

namespace ApiEcommerce.Models;
public class Product
{
    [Key]
    public int ProductId { set; get; }
    public int CategoryId { set; get; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; } 
    public string ImgUrl { get; set; }
    public string SKU { get; set; }
    public int Stock { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime UpdateDate { get; set; }
        }

