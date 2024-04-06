using Microsoft.EntityFrameworkCore;
using RestaurantAPI;
using RestaurantAPI.Data;
using System.Linq;
using AutoMapper;
using NLog;
using NLog.Web;
using RestaurantAPI.Abstractions;
using RestaurantAPI.Middleware;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
// Add services to the container.

    builder.Services.AddControllers();
    builder.Services.AddDbContext<RestaurantDbContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(RestaurantDbContext)));
    });

    builder.Services.AddScoped<IRestaurantService, RestaurantService>();
    builder.Services.AddScoped<RestaurantSeed>();
    // Middleware logger
    builder.Services.AddScoped<ErrorHandlingMiddleware>();
    builder.Services.AddScoped<RequestTimeMiddleware>();
    builder.Services.AddAutoMapper(typeof(Program).Assembly);




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();


    var app = builder.Build();


// Configure the HTTP request pipeline.

// Middleware logger
    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseMiddleware<RequestTimeMiddleware>();
// Apply all migrations and create the database if it doesn't exist

    app.Services.CreateScope().ServiceProvider.GetRequiredService<RestaurantSeed>().Seed();


    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}
