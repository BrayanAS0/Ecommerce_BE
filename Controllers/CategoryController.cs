using Microsoft.AspNetCore.Mvc;

namespace ApiEcommerce.Controllers;
[ApiController]
[Route("api/[controller]")]
    public class CategoryController:ControllerBase
    {
    [HttpGet]
    public IActionResult ObtenerCategorias()
    {
        return Ok(new { message = "aqui va eaks categorias" });
    }




    }

