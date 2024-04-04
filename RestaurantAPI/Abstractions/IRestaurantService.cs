using RestaurantAPI.Models;

namespace RestaurantAPI.Abstractions;

public interface IRestaurantService
{
    RestaurantDto GetById(int id);
    IEnumerable<RestaurantDto> GetAll();
    int Create(CreateRestaurantDto dto);
    bool Delete(int id);
    bool Update(UpdateRestaurantDto dto, int id);

}