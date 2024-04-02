using System.Collections;
using RestaurantAPI.Models;

namespace RestaurantAPI.Data;

public class RestaurantSeed
{
    private readonly RestaurantDbContext _context;
    public RestaurantSeed(RestaurantDbContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        if (_context.Database.CanConnect())
        {
                var restaurants = GetRestaurants();
                _context.Restaurants.AddRange(restaurants);
                _context.SaveChanges();
        }
    }

    private IEnumerable<Restaurant> GetRestaurants()
    {
        var restaurants = new List<Restaurant>()
        {
            new Restaurant()
            {
                Name = "KFC",
                Category = "Fast Food",
                Decsription = "KFC (short for Kentucky Fried Chicked)",
                IsDelivery = true,
                Email = "sds@gmail,com",
                ContactNumber = "12121",
                Dishes = new List<Dish>()
                {
                    new Dish()
                    {
                        Name = "Nashville Hot Chicken",
                        Price = 10.30M,
                        Description = "sdsds",
                    },

                    new Dish()
                    {
                        Name = "Chicken Nuggets",
                        Price = 10.30M,
                        Description = "sdsds",
                    },
                },
                Address = new Address()
                {
                    City = "Kraków",
                    Street = "Długa 5",
                    PostalCode = "30-001"
                },
            },
        };
        return restaurants;

    }
}