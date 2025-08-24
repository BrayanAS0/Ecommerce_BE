using ApiEcommerce.Mapping;
using ApiEcommerce.Models;
using ApiEcommerce.Models.Dtos;
using ApiEcommerce.Repository;
using ApiEcommerce.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiEcommerce.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController:ControllerBase
    {
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    public ProductController(IProductRepository productRepository, IMapper mapper,ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }
    [HttpGet("{productId:int}", Name = "GetProduct")]
    public IActionResult GetProduct(int productId)
    {
        var product = _productRepository.GetProduct(productId);
        if (product == null)
        {
            return NotFound($"El producto con el id {productId} no existe");
        }
        var productDto = _mapper.Map<ProductDto>(product);
        return Ok(productDto);
    }


    [HttpGet("",Name ="GetProducts")]
    public IActionResult GetProducts() {
        var products = _productRepository.GetProducts();
        var productsDto = _mapper.Map<List<ProductDto>>(products);
        return Ok(productsDto);
    }


    [HttpPatch("{id:int}" , Name ="UpdateProduct")]
    public IActionResult UpdateProduct(int id,UpdateProductDto updateProductDto)
    {
        if (updateProductDto == null) return NoContent();
        if (id <= 0) return BadRequest();
        if (!_productRepository.ProductExists(id)) return NoContent();

        var product = _mapper.Map<Product>(updateProductDto);
        product.ProductId = id;
        return Ok( _productRepository.UpdateProduct(product));
    }

    [HttpPost]
    public IActionResult CreateProduct(CreateProductDto ProductDto)
    {
        if (ProductDto == null) return NoContent();
        if (!_categoryRepository.CategoryExits(ProductDto.CategoryId)) return NoContent();
        if (_productRepository.ProductExists(ProductDto.Name)) return BadRequest("Product with that name alredy exits");
        var product = _mapper.Map<Product>(ProductDto);
        return Ok(_productRepository.CreateProduct(product));

    }

    [HttpDelete("DeleteProduct{productId:int}", Name ="DeleteProduct")]
    public IActionResult DeleteProduct(int productId)
    {
        if (productId <= 0) return BadRequest();
        var product = _productRepository.GetProduct(productId);
        if (product == null) return NoContent();
        return Ok( _productRepository.DelteProduct(product));
    }
    [HttpGet("searchProductByCategory/{categoryId:int}", Name = "GetProductsForCategory")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetProductsForCategory(int categoryId)
    { 
    if(categoryId <=0)return BadRequest("The Category Id sended is wrong");
    var category = _productRepository.GetProductsForCategory(categoryId);
    if (category == null) return NoContent();
        var products = _mapper.Map<List<ProductDto>>(category);
            return Ok(products);

    }
    [HttpGet("searchProductByNameDescription/{searchTerm}", Name = "SearchProducts")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult SearchProducts(string searchTerm)
    {
        if (searchTerm == null) return BadRequest("No content");

        var products = _productRepository.SearchProducts(searchTerm);
        if(products.Count()==0) return NotFound();
        var productsDto = _mapper.Map<List<ProductDto>>(products);
        return Ok(productsDto);
    }
    [HttpPatch("buyProduct/{name}/{quantity:int}", Name = "BuyProduct")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult BuyProduct(string name, int quantity)
    {
        if (string.IsNullOrWhiteSpace(name)) return BadRequest("Producto no valido");
        if(quantity <= 0) return BadRequest("La cantidad no es valida");
        if(!_productRepository.ProductExists(name)) return NotFound();
        if(!_productRepository.BuyProduct(name, quantity)) return NotFound();
        return Ok($"Compro {quantity}  de {name}");
    
    }


    }

