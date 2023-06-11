using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.Model.Repository.Implements;
using SoloTravelAgent.Model.Repository;
using System.Collections.Generic;

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

        //public IEnumerable<Restaurant> GetRestaurantsByTrip(int tripId)
        //{
        //    return _restaurantRepository.GetAll().Where(r => r.TripId == tripId);
        //}
    }
}
