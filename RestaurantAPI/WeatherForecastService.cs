using System.Collections;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Abstractions;

namespace RestaurantAPI.Controllers;

public class WeatherForecastService : IWeatherForecastService
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public static readonly List<WeatherForecast> _weatherForecasts = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
        .ToList();
    
    public IEnumerable<WeatherForecast> Get()
    {
        return _weatherForecasts;
    }

    public IActionResult Delete(int index)
    {

        if (index < 0 || index >= _weatherForecasts.Count)
        {
            return new NotFoundResult();
        }

        _weatherForecasts.RemoveAt(index);
        return new NoContentResult();
    }
    
}