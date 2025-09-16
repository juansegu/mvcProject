namespace API.services;

using API.Models;
using API.Repository;


public class ProductsService : IProductsService
{
    private readonly IProductRepository _productRepository;
    public ProductsService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await _productRepository.GetAllAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _productRepository.GetByIdAsync(id);
    }

    public async Task<Product> AddProductAsync(ProductDto productDto)
    {
        var newProduct = new Product
        {
            // The database will generate the ID, so we don't set it here.
            Name = productDto.Name,
            Price = productDto.Price
        };
        await _productRepository.AddAsync(newProduct);
        return newProduct;
    }

    public Product UpdateProduct(ProductDto productDTO)
    {
        /*var existingProduct = GetProductById(productDTO.Id ?? 0);
        if (existingProduct == null)
        {
            throw new KeyNotFoundException("Product not found");
        }

        existingProduct.Name = productDTO.Name;
        existingProduct.Price = productDTO.Price;
        return existingProduct;*/
        return null;
    }

    public void DeleteProduct(int id)
    {
        /*var product = GetProductById(id);
        if (product == null)
        {
            throw new KeyNotFoundException("Product not found");
        }

        _products.Remove(product);*/

    }
}