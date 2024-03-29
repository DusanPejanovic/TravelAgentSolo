﻿using Microsoft.EntityFrameworkCore;
using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SoloTravelAgent.Model.Repository.Implements
{

    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly TravelSystemDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(TravelSystemDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public T Get(int id)
        {
            return _dbSet.Find(id);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Remove(T entity)
        {
            var trip = entity as Trip;

            if (trip != null)
            {
                trip.Restaurants.Clear();
                trip.Accommodations.Clear();
                trip.TripTouristAttractions.Clear();
                _context.SaveChanges();
            }

            _dbSet.Remove(entity);
            _context.SaveChanges();
        }
    }
}
