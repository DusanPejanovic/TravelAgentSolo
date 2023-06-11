using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.Entities;
using System;

namespace SoloTravelAgent.Model.Service
{
    public class AuthService
    {
        private readonly ClientService _clientService;
        private readonly AgentService _agentService;

        public AuthService(TravelSystemDbContext dbContext)
        {
            _clientService = new ClientService(dbContext);
            _agentService = new AgentService(dbContext);
        }

        public User Login(string username, string password)
        {
            User user = _clientService.Login(username, password);

            if (user == null)
            {
                user = _agentService.Login(username, password);
            }

            return user;
        }

        public bool IsEmailTaken(string email)
        {
            bool isTakenInClients = _clientService.IsEmailTaken(email);
            bool isTakenInAgents = _agentService.IsEmailTaken(email);
            return isTakenInClients || isTakenInAgents;
        }

        public User Register(string name, string email, string phoneNumber, string password)
        {
            return _clientService.Register(name, email, phoneNumber, password);
        }
    }
}
