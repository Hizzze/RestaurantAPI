namespace RestaurantAPI.Models;

public class RestaurantDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Decsription { get; set; }
    public string Category { get; set; }
    public bool IsDelivery { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string PostalCode { get; set; }

    public List<DishDto> Dishes { get; set; }
}