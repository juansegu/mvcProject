using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class ProductDto
{
    [Required]
    [StringLength(100)]
    public string? Name { get; set; }

    [Range(0.01, 10000.00)]
    public decimal Price { get; set; }
}