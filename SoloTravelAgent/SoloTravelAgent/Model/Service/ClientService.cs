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
    public class ClientService
    {
        private readonly IRepository<Client> _clientRepository;

        public ClientService(TravelSystemDbContext dbContext)
        {
            _clientRepository = new Repository<Client>(dbContext);
        }

        public IEnumerable<Client> GetAllClients()
        {
            return _clientRepository.GetAll();
        }

        public Client GetClient(int id)
        {
            return _clientRepository.Get(id);
        }

        public void AddClient(Client client)
        {
            _clientRepository.Add(client);
        }

        public void UpdateClient(Client client)
        {
            _clientRepository.Update(client);
        }

        public void RemoveClient(Client client)
        {
            _clientRepository.Remove(client);
        }
    }
}
