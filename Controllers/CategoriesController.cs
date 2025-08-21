using ApiEcommerce.Models;
using ApiEcommerce.Models.Dtos;
using ApiEcommerce.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ApiEcommerce.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;


    public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    [HttpGet(Name = "GetCategories")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetCategories()
    {
        var categories = _categoryRepository.GetCategories();
        var categoriesDto = new List<CategoryDto>();
        foreach (var category in categories)
        {
            categoriesDto.Add(_mapper.Map<CategoryDto>(category));
        }
        return Ok(categoriesDto);
    }



    [HttpGet("{id:int}", Name = "GetCategory")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetCategory(int id)
    {
        var category = _categoryRepository.GetCategory(id);
        if (category == null)
        {
            return NotFound($"La categoria del {id} no existe");
        }
        var categoryDto = _mapper.Map<CategoryDto>(category);
        return Ok(categoryDto);
    }
    [HttpPost]
    public IActionResult CreateCategory(CreateCategoryDto createCaretgoryDto)
    {
        if (createCaretgoryDto == null) return BadRequest("");

        if (_categoryRepository.CategoryExits(createCaretgoryDto.Name))
        {
            ModelState.AddModelError("CustomError", "La categoria ya existe");
            return BadRequest(ModelState);
        }
        var category = _mapper.Map<Category>(createCaretgoryDto);
        if (!_categoryRepository.CreateCategory(category)) return BadRequest("");
            return CreatedAtRoute("GetCategory", new { id = category.Id }, category);
    }

    [HttpPatch("{id:int}",Name ="updateCategory")]
    public IActionResult UpdateCategory(int id,[FromBody] CreateCategoryDto categoryDto) {
        if (categoryDto == null)
        {
            return BadRequest();
        }
        if (!_categoryRepository.CategoryExits(id))
        {
            return NotFound();
        }
        var category = _mapper.Map<Category>(categoryDto);
        category.Id = id;
        if (!_categoryRepository.UpdateCategory(category)) return BadRequest();

        return CreatedAtRoute("GetCategory", new { id = category.Id }, category);
    }

    [HttpDelete]
    public IActionResult DeleteCategory(int id)
    {
        if (!_categoryRepository.CategoryExits(id)) return NotFound();
var category=_categoryRepository.GetCategory(id);
        if (category == null) return NotFound($"la categoria con {id} no existe");

        if (!_categoryRepository.DeleteCategory(category))
        {
            ModelState.AddModelError("CustoErro", $"Algo salio mal al eliminar el registo {category.Name}");
            return StatusCode(500,ModelState);
        }
        return Ok($"El registro {category.Name} fue borrado existoamente");
        
            }
}

