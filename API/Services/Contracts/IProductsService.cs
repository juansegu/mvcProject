namespace API.services;

using API.Models;

public interface IProductsService
{
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
    Task<Product> AddProductAsync(ProductDto productDto);
    Product UpdateProduct(ProductDto productDTO);
    void DeleteProduct(int id);
}