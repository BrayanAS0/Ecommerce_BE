using System.ComponentModel.DataAnnotations;

namespace ApiEcommerce.Models.Dtos
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage ="El nombre es obligatorio")]
        [MaxLength(50,ErrorMessage ="error solo puede tener maximo 50 caracteres")]
        [MinLength(3,ErrorMessage ="Error solo puede tener minimo 3 caracteres")]
        public string Name { get; set; } = string.Empty;
    }
}
