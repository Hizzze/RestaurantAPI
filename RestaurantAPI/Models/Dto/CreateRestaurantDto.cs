using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models;

public class CreateRestaurantDto
{
    [Required]
    [MaxLength(25)]
    public string Name { get; set; }
    public string Decsription { get; set; }
    public string Category { get; set; }
    public bool IsDelivery { get; set; }
    public string Email { get; set; }
    public string ContactNumber { get; set; }
    [Required]
    [MaxLength(50)]
    public string City { get; set; }
    [Required]
    [MaxLength(50)]
    public string Street { get; set; }
    public string PostalCode { get; set; }
}