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

        public IEnumerable<Booking> GetBookingsByTripId(int tripId)
        {
            return _bookingRepository.GetAll().Where(b => b.Trip.Id == tripId);
        }

        public IEnumerable<Booking> GetCurrentMonthBookingsForTrip(int tripId)
        {
            var now = DateTime.Now;
            return _bookingRepository.GetAll().Where(b => b.Trip.Id == tripId && b.BookingDate.Month == now.Month && b.BookingDate.Year == now.Year);
        }

        public IEnumerable<Booking> GetBookingsForPreviousMonthsAndTrip(int months, int tripId)
        {
            var cutOffDate = DateTime.Now.AddMonths(-months);
            return _bookingRepository
                .GetAll()
                .Where(b => b.Trip.Id == tripId &&  b.BookingDate >= cutOffDate && b.IsPaid == true);
        }
    }
}
