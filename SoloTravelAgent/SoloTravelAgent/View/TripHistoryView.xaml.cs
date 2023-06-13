using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.Navigation;
using SoloTravelAgent.View.DialogView;
using SoloTravelAgent.ViewModel;
using SoloTravelAgent.ViewModel.BookingManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SoloTravelAgent.View
{
    /// <summary>
    /// Interaction logic for TripHistoryView.xaml
    /// </summary>
    public partial class TripHistoryView : UserControl
    {
        public TripHistoryView()
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
                    var viewModel = DataContext as TripBookedViewModel;
                    viewModel.DeleteBooking(booking.Id);
                }
            }
        }
}
