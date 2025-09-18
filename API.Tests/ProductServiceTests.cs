using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using API.Models;
using API.services;
using API.Repository;
using System.Threading.Tasks;

namespace API.Tests;

[TestClass]
public class ProductServiceTests
{
    private Mock<IProductRepository> _mockRepo = null!;
    private ProductsService _service = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockRepo = new Mock<IProductRepository>();
        _service = new ProductsService(_mockRepo.Object);
    }

    [TestMethod]
    public async Task GetProductByIdAsync_ShouldReturnProduct_WhenProductExists()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Test Laptop", Price = 100 };
        _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(product);

        // Act
        var result = await _service.GetProductByIdAsync(1);

        // Assert 
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(product);
    }

    [TestMethod]
    public async Task GetProductByIdAsync_ShouldReturnNull_WhenProductDoesNotExist()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetByIdAsync(99)).ReturnsAsync((Product?)null);

        // Act
        var result = await _service.GetProductByIdAsync(99);

        // Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public async Task AddProductAsync_ShouldReturnNewProduct_WhenCalledWithDto()
    {
        // Arrange
        var createDto = new ProductDto { Name = "New Mouse", Price = 25 };
        var expectedProduct = new Product { Id = 1, Name = "New Mouse", Price = 25 };

        _mockRepo.Setup(repo => repo.AddAsync(It.IsAny<Product>()))
                 .Callback<Product>(p => p.Id = 1)
                 .Returns(Task.CompletedTask);

        // Act
        var result = await _service.AddProductAsync(createDto);

        // Assert
        result.Should().BeEquivalentTo(expectedProduct, options => options.Excluding(p => p.Id));
        _mockRepo.Verify(repo => repo.AddAsync(It.IsAny<Product>()), Times.Once);
    }
}