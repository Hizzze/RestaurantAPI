using System.Collections;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Abstractions;
using RestaurantAPI.Data;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services;

public class RestaurantService : IRestaurantService
{
    private readonly RestaurantDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public RestaurantService(RestaurantDbContext context, IMapper mapper, ILogger<RestaurantService> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }
    public RestaurantDto GetById(int id)
    {
        
        var rest = _context.Restaurants
            .Include(r => r.Address)
            .Include(r => r.Dishes)
            .FirstOrDefault(r => r.Id == id);

        if (rest is null)
        {
            throw new NotFoundException("Restaurant not found");
        }

        var result = _mapper.Map<RestaurantDto>(rest);
        return result;
    }

    public IEnumerable<RestaurantDto> GetAll()
    {
        var restaurants = _context
            .Restaurants
            .Include(r => r.Address)
            .Include(r => r.Dishes)
            .ToList();

        var restaurantsDtos = _mapper.Map<List<RestaurantDto>>(restaurants);
        return restaurantsDtos;
    }

    public int Create(CreateRestaurantDto dto)
    {
        var restaurant = _mapper.Map<Restaurant>(dto);
        _context.Restaurants.Add(restaurant);
        _context.SaveChanges();

        return restaurant.Id;

    }

    public void Delete(int id)
    {
        _logger.LogError($"Restaurant with id: {id} DELETE action invoked");
        var rest = _context.Restaurants
            .FirstOrDefault(r => r.Id == id);

        if (rest is null)
        {
            throw new NotFoundException("Restaurant not found");
        }
 
         _context.Restaurants.Remove(rest);
         _context.SaveChanges();
    }

    public void Update(UpdateRestaurantDto dto, int id)
     {
        var restaurant = _context.Restaurants.FirstOrDefault(r => r.Id == id);

        if (restaurant is null)
        {
            throw new NotFoundException("Restaurant not found");
        }

        restaurant.Name = dto.Name;
        restaurant.Decsription = dto.Description;
        restaurant.IsDelivery = dto.IsDelivery;
        
        _context.SaveChanges();
    }
}