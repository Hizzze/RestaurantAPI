using Microsoft.EntityFrameworkCore;
using RestaurantAPI;
using RestaurantAPI.Abstractions;
using RestaurantAPI.Controllers;
using RestaurantAPI.Data;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<RestaurantDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(RestaurantDbContext)));
});

builder.Services.AddScoped<RestaurantSeed>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IWeatherForecastService, WeatherForecastService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
 

// Apply all migrations and create the database if it doesn't exist

app.Services.CreateScope().ServiceProvider.GetRequiredService<RestaurantSeed>().Seed();

// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;
//     var seed = services.GetRequiredService<RestaurantSeed>();
//     seed.Seed();
// }


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();