using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.Model.Repository.Implements;
using SoloTravelAgent.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoloTravelAgent.Model.Service
{
    public class RestaurantService
    {
        private readonly IRepository<Restaurant> _restaurantRepository;

        public RestaurantService(TravelSystemDbContext dbContext)
        {
            _restaurantRepository = new Repository<Restaurant>(dbContext);
        }

        public IEnumerable<Restaurant> GetAllRestaurants()
        {
            return _restaurantRepository.GetAll();
        }

        public Restaurant GetRestaurant(int id)
        {
            return _restaurantRepository.Get(id);
        }

        public void AddRestaurant(Restaurant restaurant)
        {
            _restaurantRepository.Add(restaurant);
        }

        public void UpdateRestaurant(Restaurant restaurant)
        {
            _restaurantRepository.Update(restaurant);
        }

        public void RemoveRestaurant(Restaurant restaurant)
        {
            _restaurantRepository.Remove(restaurant);
        }
    }
}
