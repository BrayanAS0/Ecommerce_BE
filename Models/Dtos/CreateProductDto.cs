using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiEcommerce.Models.Dtos
{
    public class CreateProductDto
    {
        public required int CategoryId { set; get; }

        public string Name { get; set; } =string.Empty;
        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }
        public string ImgUrl { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public int Stock { get; set; }
        public DateTime? UpdateDate { get; set; } = null;
    }
}
