using RestaurantAPI.Models;

namespace RestaurantAPI.Abstractions;

public interface IRestaurantService
{
    RestaurantDto GetById(int id);
    IEnumerable<RestaurantDto> GetAll();
    int Create(CreateRestaurantDto dto);
    void Delete(int id);
    void Update(UpdateRestaurantDto dto, int id);

}