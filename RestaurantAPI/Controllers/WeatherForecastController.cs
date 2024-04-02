using System.Collections;
using Microsoft.AspNetCore.Http.HttpResults;
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

    [HttpPost]
    public ActionResult<string> Hello(string name)
    {
        return NotFound($"Hello {name}");
    }

    [HttpPost("generate")]
    public ActionResult<IEnumerable<WeatherForecast>> Generate([FromQuery]int count,[FromBody] TemperatureRequest tempC)
    {
        if (count < 0 || tempC.Max < tempC.Min)
        {
            return BadRequest();
        }

        var result = _service.Get(count, tempC.Min, tempC.Max);
        return Ok(result);
    }


}