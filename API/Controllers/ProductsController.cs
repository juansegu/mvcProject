using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.services;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductsService _productsService;

    public ProductsController(IProductsService productsService)
    {
        this._productsService = productsService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var products = await _productsService.GetProductsAsync();

        return Ok(products);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProductById(int id)
    {
        var product = await _productsService.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }
    [HttpPost]
    public async Task<ActionResult<Product>> AddProduct(ProductDto productDto)
    {
        var newProduct = await _productsService.AddProductAsync(productDto);
        return CreatedAtAction(
            nameof(GetProductById),
            new { id = newProduct.Id },
            newProduct);
    }
    [HttpPut]
    public ActionResult<Product> UpdateProduct(ProductDto productDto)
    {
        var product = _productsService.UpdateProduct(productDto);

        return Ok(product);
    }
    [HttpDelete("{id}")]
    public ActionResult DeleteProduct(int id)
    {
        _productsService.DeleteProduct(id);

        return Ok();
    }

}

