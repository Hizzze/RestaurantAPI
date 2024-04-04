using System.Collections;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestaurantAPI.Abstractions;
using RestaurantAPI.Data;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers;


[Route("api/restaurant")]
[ApiController]
public class RestaurantController : ControllerBase
{

    private readonly IRestaurantService _restaurantService;

    public RestaurantController(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }
    [HttpGet]
    public ActionResult<IEnumerable<RestaurantDto>> GetAll()
    {
        var restaurantDtos = _restaurantService.GetAll();
        return Ok(restaurantDtos);


    }

    [HttpGet("{id}")]
    public ActionResult<RestaurantDto> GetById(int id)
    {
        var result = _restaurantService.GetById(id);

        if (result is null) return NotFound();
        
        return Ok(result);
    }
    

    [HttpPost]
    public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var id = _restaurantService.Create(dto);
        return Created($"/api/restaurant{id}", null);

    }
    
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var IsDeleted = _restaurantService.Delete(id);

        if (IsDeleted) return NoContent();
        return NotFound();
    }

    [HttpPut("{id}")]
    public ActionResult Update(UpdateRestaurantDto dto, int id)
    {

        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var isUpdated = _restaurantService.Update(dto,id);
        if (isUpdated) return Ok();
        return NotFound();
    }
}