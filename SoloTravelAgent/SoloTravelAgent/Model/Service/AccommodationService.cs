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
    public class AccommodationService
    {
        private readonly IRepository<Accommodation> _accommodationRepository;

        public AccommodationService(TravelSystemDbContext dbContext)
        {
            _accommodationRepository = new Repository<Accommodation>(dbContext);
        }

        public IEnumerable<Accommodation> GetAllAccommodations()
        {
            return _accommodationRepository.GetAll();
        }

        public Accommodation GetAccommodation(int id)
        {
            return _accommodationRepository.Get(id);
        }

        public void AddAccommodation(Accommodation accommodation)
        {
            _accommodationRepository.Add(accommodation);
        }

        public void UpdateAccommodation(Accommodation accommodation)
        {
            _accommodationRepository.Update(accommodation);
        }

        public void RemoveAccommodation(Accommodation accommodation)
        {
            _accommodationRepository.Remove(accommodation);
        }
    }
}
