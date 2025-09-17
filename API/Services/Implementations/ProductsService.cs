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

    public async Task<Product?> UpdateProductAsync(int id, ProductDto productDto)
    {
        var existingProduct = await _productRepository.GetByIdAsync(id);
        if (existingProduct == null)
        {
            return null; // Not found
        }

        // Update properties
        existingProduct.Name = productDto.Name;
        existingProduct.Price = productDto.Price;

        await _productRepository.UpdateAsync(existingProduct);
        return existingProduct;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        return await _productRepository.DeleteAsync(id);
    }
}