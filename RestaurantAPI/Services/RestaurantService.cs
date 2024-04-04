using System.Collections;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Abstractions;
using RestaurantAPI.Data;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services;

public class RestaurantService : IRestaurantService
{
    private readonly RestaurantDbContext _context;
    private readonly IMapper _mapper;

    public RestaurantService(RestaurantDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public RestaurantDto GetById(int id)
    {
        
        var rest = _context.Restaurants
            .Include(r => r.Address)
            .Include(r => r.Dishes)
            .FirstOrDefault(r => r.Id == id);

        if (rest is null) return null;

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

    public bool Delete(int id)
    {
        var rest = _context.Restaurants
            .FirstOrDefault(r => r.Id == id);

        if (rest is null) return false;

         _context.Restaurants.Remove(rest);
         _context.SaveChanges();
        return true;
    }

    public bool Update(UpdateRestaurantDto dto, int id)
    {
        var restaurant = _context.Restaurants.FirstOrDefault(r => r.Id == id);

        if (restaurant == null)
        {
            return false;
        }

        restaurant.Name = dto.Name;
        restaurant.Decsription = dto.Description;
        restaurant.IsDelivery = dto.IsDelivery;
        
        _context.SaveChanges();

        return true;
    }
}