using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.Model.Repository.Implements;
using SoloTravelAgent.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using SoloTravelAgent.Model.DTO;

namespace SoloTravelAgent.Model.Service
{
    public class TripStatisticsService
    {
        private readonly IRepository<Trip> _tripRepository;
        private readonly IRepository<Booking> _bookingRepository;

        public TripStatisticsService(TravelSystemDbContext dbContext)
        {
            _tripRepository = new Repository<Trip>(dbContext);
            _bookingRepository = new Repository<Booking>(dbContext);
        }

        public IEnumerable<TripStatistics> GetTripStatisticsForMonth(int year, int month)
        {
            return _bookingRepository
                .GetAll()
                .Where(b => b.BookingDate.Year == year && b.BookingDate.Month == month && b.IsPaid == true)
                .GroupBy(b => new { b.Trip.Id, b.Trip.Name })
                .Select(g => new TripStatistics
                {
                    TripId = g.Key.Id,
                    TripName = g.Key.Name,
                    NumberOfBookings = g.Count(),
                    TotalMoneyEarned = g.Sum(b => b.Trip.Price)
                })
                .ToList();
        }

        public IEnumerable<TripStatistics> GetTripStatisticsForLastMonths(int months)
        {
            var cutOffDate = DateTime.Now.AddMonths(-months);

            return _bookingRepository
                .GetAll()
                .Where(b => b.BookingDate >= cutOffDate && b.IsPaid == true)
                .GroupBy(b => new { b.Trip.Id, b.Trip.Name })
                .Select(g => new TripStatistics
                {
                    TripId = g.Key.Id,
                    TripName = g.Key.Name,
                    NumberOfBookings = g.Count(),
                    TotalMoneyEarned = g.Sum(b => b.Trip.Price)
                })
                .ToList();
        }

        public IEnumerable<TripStatistics> GetTripStatisticsForAllTime()
        {
            var trips = _tripRepository.GetAll();
            var bookings = _bookingRepository.GetAll().Where(b => b.IsPaid == true);
            var tripStats = from trip in trips
                            join booking in bookings on trip.Id equals booking.Trip.Id into tripGroup
                            from subTrip in tripGroup.DefaultIfEmpty()
                            select new TripStatistics
                            {
                                TripId = trip.Id,
                                TripName = trip.Name,
                                NumberOfBookings = tripGroup.Count(),
                                TotalMoneyEarned = tripGroup.Sum(b => b != null ? b.Trip.Price : 0)
                            };
            return tripStats.ToList();
        }

        public IEnumerable<TripStatistics> GetPopularTrips()
        {
            return _bookingRepository
                .GetAll()
                .Where(b => b.IsPaid == true)
                .GroupBy(b => new { b.Trip.Id, b.Trip.Name })
                .Select(g => new TripStatistics
                {
                    TripId = g.Key.Id,
                    TripName = g.Key.Name,
                    NumberOfBookings = g.Count(),
                    TotalMoneyEarned = g.Sum(b => b.Trip.Price)
                })
                .Where(ts => ts.NumberOfBookings >= 5)
                .ToList();
        }

        public IEnumerable<TripStatistics> GetProfitableTrips()
        {
            return _bookingRepository
                .GetAll()
                .Where(b => b.IsPaid == true)
                .GroupBy(b => new { b.Trip.Id, b.Trip.Name })
                .Select(g => new TripStatistics
                {
                    TripId = g.Key.Id,
                    TripName = g.Key.Name,
                    NumberOfBookings = g.Count(),
                    TotalMoneyEarned = g.Sum(b => b.Trip.Price)
                })
                .Where(ts => ts.TotalMoneyEarned >= 2000)
                .ToList();
        }
    }
}
