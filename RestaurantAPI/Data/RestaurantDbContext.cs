using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;

namespace RestaurantAPI.Data;

public class RestaurantDbContext : DbContext
{
    public RestaurantDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Dish> Dishes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Restaurant>()
            .Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(25);

        modelBuilder.Entity<Dish>()
            .Property(d => d.Name)
            .IsRequired();
    }

}