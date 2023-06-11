using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.Model.Repository.Implements;
using SoloTravelAgent.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

        public Client Login(string email, string password)
        {
            return _clientRepository.GetAll().SingleOrDefault(c => c.Email == email && c.Password == password);
        }

        public bool IsEmailTaken(string email)
        {
            return _clientRepository.GetAll().Any(client => client.Email == email);
        }

        public Client Register(string name, string email, string phoneNumber, string password)
        {
            var client = new Client { Name = name, Email = email, PhoneNumber = phoneNumber, Password = password };
            AddClient(client);
            return client;
        }
    }
}
