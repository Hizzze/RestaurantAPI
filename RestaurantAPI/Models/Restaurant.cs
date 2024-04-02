namespace RestaurantAPI.Models;

public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Decsription { get; set; }
    public string Category { get; set; }
    public bool IsDelivery { get; set; }
    public string Email { get; set; }
    public string ContactNumber { get; set; }

    public int AddressId { get; set; }
    public virtual Address Address { get; set; }
    public virtual List<Dish> Dishes { get; set; }
}