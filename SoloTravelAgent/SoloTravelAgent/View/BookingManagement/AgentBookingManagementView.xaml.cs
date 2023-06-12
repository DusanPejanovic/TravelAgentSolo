using SoloTravelAgent.Model.DTO;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.ViewModel.BookingManagement;
using SoloTravelAgent.ViewModel.Report;
using System.Windows;
using System.Windows.Controls;

namespace SoloTravelAgent.View.BookingManagement
{
    /// <summary>
    /// Interaction logic for AgentBookingManagementView.xaml
    /// </summary>
    public partial class AgentBookingManagementView : UserControl
    {
        public AgentBookingManagementView()
        {
            InitializeComponent();
        }

        private void ApproveBooking(object sender, RoutedEventArgs e)
        {
            var booking = (sender as Button).DataContext as Booking;
            if (booking == null) return;


            MessageBoxResult result = MessageBox.Show("Are you sure you want to approve this booking?", "Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                var viewModel = DataContext as AgentBookingManagementViewModel;
                viewModel.ApproveBooking(booking.Id);
            }
        }

        private void RejectBooking(object sender, RoutedEventArgs e)
        {
            var booking = (sender as Button).DataContext as Booking;
            if (booking == null) return;


            MessageBoxResult result = MessageBox.Show("Are you sure you want to reject this booking?", "Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                var viewModel = DataContext as AgentBookingManagementViewModel;
                viewModel.DeleteBooking(booking.Id);
            }
        }
    }
}
