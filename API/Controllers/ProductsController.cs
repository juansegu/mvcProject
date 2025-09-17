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
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, ProductDto productDto)
    {
        var updatedProduct = await _productsService.UpdateProductAsync(id, productDto);
        if (updatedProduct == null)
        {
            return NotFound(); // Returns 404 Not Found
        }
        return NoContent(); // Returns 204 No Content on successful update
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var success = await _productsService.DeleteProductAsync(id);
        if (!success)
        {
            return NotFound(); // Returns 404 Not Found
        }
        return NoContent(); // Returns 204 No Content on successful delete
    }

}

