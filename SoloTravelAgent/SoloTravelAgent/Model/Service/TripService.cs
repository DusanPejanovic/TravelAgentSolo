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
    public class TripService
    {
        private readonly IRepository<Trip> _tripRepository;

        public TripService(TravelSystemDbContext dbContext)
        {
            _tripRepository = new Repository<Trip>(dbContext);
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            return _tripRepository.GetAll();
        }

        public Trip GetTrip(int id)
        {
            return _tripRepository.Get(id);
        }

        public void AddTrip(Trip trip)
        {
            _tripRepository.Add(trip);
        }

        public void UpdateTrip(Trip trip)
        {
            _tripRepository.Update(trip);
        }

        public void RemoveTrip(Trip trip)
        {
            _tripRepository.Remove(trip);
        }
    }

}
