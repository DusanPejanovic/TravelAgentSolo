using GalaSoft.MvvmLight;
using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.Model.Service;
using System.Collections.ObjectModel;

namespace SoloTravelAgent.ViewModel
{
    public class AgentBookingsViewModel: ViewModelBase
    {
        private readonly BookingService _bookingService;

        public AgentBookingsViewModel(TravelSystemDbContext dbContext)
        {
            Bookings = new ObservableCollection<Booking>();
            _bookingService = new BookingService(dbContext);
            LoadBookings();
        }

        public ObservableCollection<Booking> Bookings { get; set; } = new ObservableCollection<Booking>();

        public Booking SelectedBooking { get; set; }

        public int BookingCount
        {
            get
            {
                return Bookings?.Count ?? 0;
            }
        }

        public void LoadBookings()
        {
            var bookings = _bookingService.GetAllBookings();
            Bookings.Clear();
            foreach (var booking in bookings)
            {
                Bookings.Add(booking);
            }

            RaisePropertyChanged(nameof(Bookings));
        }
    }
}
