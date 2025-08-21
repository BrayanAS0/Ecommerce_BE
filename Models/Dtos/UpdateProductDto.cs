namespace ApiEcommerce.Models.Dtos
{
    public class UpdateProductDto
    {
        public int CategoryId { set; get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImgUrl { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public int Stock { get; set; }
        public DateTime UpdateDate { get; set; }= DateTime.Now;
    }
}
