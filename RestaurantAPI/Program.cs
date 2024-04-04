using Microsoft.EntityFrameworkCore;
using RestaurantAPI;
using RestaurantAPI.Data;
using System.Linq;
using AutoMapper;
using RestaurantAPI.Abstractions;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<RestaurantDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(RestaurantDbContext)));
});

builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<RestaurantSeed>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


// Configure the HTTP request pipeline.
 

// Apply all migrations and create the database if it doesn't exist

app.Services.CreateScope().ServiceProvider.GetRequiredService<RestaurantSeed>().Seed();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();