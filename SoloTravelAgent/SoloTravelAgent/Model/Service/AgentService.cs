using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.Model.Repository.Implements;
using SoloTravelAgent.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SoloTravelAgent.Model.Service
{
    public class AgentService
    {
        private readonly IRepository<Agent> _agentRepository;

        public AgentService(TravelSystemDbContext dbContext)
        {
            _agentRepository = new Repository<Agent>(dbContext);
        }

        public IEnumerable<Agent> GetAllAgents()
        {
            return _agentRepository.GetAll();
        }

        public Agent GetAgent(int id)
        {
            return _agentRepository.Get(id);
        }

        public void AddAgent(Agent agent)
        {
            _agentRepository.Add(agent);
        }

        public void UpdateAgent(Agent agent)
        {
            _agentRepository.Update(agent);
        }

        public void RemoveAgent(Agent agent)
        {
            _agentRepository.Remove(agent);
        }

        public Agent Login(string email, string password)
        {
            return _agentRepository.GetAll().SingleOrDefault(c => c.Email == email && c.Password == password);
        }

        public bool IsEmailTaken(string email)
        {
            return _agentRepository.GetAll().Any(client => client.Email == email);
        }
    }
}
