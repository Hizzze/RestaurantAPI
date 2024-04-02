using Microsoft.AspNetCore.Mvc;

namespace RestaurantAPI.Abstractions;

public interface IWeatherForecastService
{
    IEnumerable<WeatherForecast> Get();
    IActionResult Delete(int index);
}