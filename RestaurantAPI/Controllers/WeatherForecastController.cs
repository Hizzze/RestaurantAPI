using System.Collections;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Abstractions;

namespace RestaurantAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherForecastService _service;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService service)
    {
        _logger = logger;
        _service = service;
    }
    
    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        var result = _service.Get();
        return result;
    }

    [HttpDelete]
    public IActionResult Delete(int index)
    {
        var result = _service.Delete(index);
        return result;
    }
}