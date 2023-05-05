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
    public class TouristAttractionService
    {
        private readonly IRepository<TouristAttraction> _attractionRepository;

        public TouristAttractionService(TravelSystemDbContext dbContext)
        {
            _attractionRepository = new Repository<TouristAttraction>(dbContext);
        }

        public IEnumerable<TouristAttraction> GetAllAttractions()
        {
            return _attractionRepository.GetAll();
        }

        public TouristAttraction GetAttraction(int id)
        {
            return _attractionRepository.Get(id);
        }

        public void AddAttraction(TouristAttraction attraction)
        {
            _attractionRepository.Add(attraction);
        }
        public void UpdateAttraction(TouristAttraction attraction)
        {
            _attractionRepository.Update(attraction);
        }

        public void RemoveAttraction(TouristAttraction attraction)
        {
            _attractionRepository.Remove(attraction);
        }
    }
}
