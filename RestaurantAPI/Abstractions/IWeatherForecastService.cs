using Microsoft.AspNetCore.Mvc;

namespace RestaurantAPI.Abstractions;

public interface IWeatherForecastService
{
    IEnumerable<WeatherForecast> Get(int take, int minTemp, int maxTemp);
}