using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using API.Data;
using API.Models;
using API.services;
using API.Repository;
using System.Threading.Tasks;

namespace Api.Tests;

[TestClass]
public class ProductRepositoryTests
{
    private async Task<ProductDbContext> GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ProductDbContext>()
            .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString()) // Unique DB for each test
            .Options;
        var dbContext = new ProductDbContext(options);
        dbContext.Database.EnsureCreated();
        return dbContext;
    }

    [TestMethod]
    public async Task GetAllAsync_ShouldReturnAllProducts()
    {
        // Arrange
        var dbContext = await GetDbContext();
        var repository = new ProductRepository(dbContext);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(3); // Based on the seed data in our DbContext
    }

    [TestMethod]
    public async Task AddAsync_ShouldAddProductAndSaveChanges()
    {
        // Arrange
        var dbContext = await GetDbContext();
        var repository = new ProductRepository(dbContext);
        var newProduct = new Product { Name = "New Gadget", Price = 99.99m };

        // Act
        await repository.AddAsync(newProduct);

        // Assert
        // The seeded data has 3 items, so the new count should be 4
        dbContext.Products.Should().HaveCount(4);
        // The database assigns the ID, so it should no longer be 0
        newProduct.Id.Should().NotBe(0);
    }

    [TestMethod]
    public async Task DeleteAsync_ShouldRemoveProduct_WhenProductExists()
    {
        // Arrange
        var dbContext = await GetDbContext();
        var repository = new ProductRepository(dbContext);
        var productIdToDelete = 1;

        // Act
        var result = await repository.DeleteAsync(productIdToDelete);

        // Assert
        result.Should().BeTrue();
        var deletedProduct = await dbContext.Products.FindAsync(productIdToDelete);
        deletedProduct.Should().BeNull(); // Confirm it's gone from the DB
        dbContext.Products.Should().HaveCount(2); // Started with 3, now should be 2
    }

    [TestMethod]
    public async Task DeleteAsync_ShouldReturnFalse_WhenProductDoesNotExist()
    {
        // Arrange
        var dbContext = await GetDbContext();
        var repository = new ProductRepository(dbContext);
        var nonExistentId = 99;

        // Act
        var result = await repository.DeleteAsync(nonExistentId);

        // Assert
        result.Should().BeFalse();
    }
}