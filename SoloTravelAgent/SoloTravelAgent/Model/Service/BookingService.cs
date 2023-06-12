using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.Model.Repository.Implements;
using SoloTravelAgent.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoloTravelAgent.Model.DTO;

namespace SoloTravelAgent.Model.Service
{
    public class BookingService
    {
        private readonly IRepository<Booking> _bookingRepository;

        public BookingService(TravelSystemDbContext dbContext)
        {
            _bookingRepository = new Repository<Booking>(dbContext);
        }

        public IEnumerable<Booking> GetAllBookings()
        {
            return _bookingRepository.GetAll();
        }

        public Booking GetBooking(int id)
        {
            return _bookingRepository.Get(id);
        }

        public void AddBooking(Booking booking)
        {
            _bookingRepository.Add(booking);
        }

        public void UpdateBooking(Booking booking)
        {
            _bookingRepository.Update(booking);
        }

        public void RemoveBooking(Booking booking)
        {
            _bookingRepository.Remove(booking);
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
                .ToList();
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
