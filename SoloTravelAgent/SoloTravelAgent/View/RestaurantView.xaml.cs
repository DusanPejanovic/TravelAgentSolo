using SoloTravelAgent.Model.Data;
using SoloTravelAgent.ViewModel;
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
    /// Interaction logic for RestaurantView.xaml
    /// </summary>
    public partial class RestaurantView : Window
    {

        private readonly RestaurantViewModel _viewModel;
        public RestaurantView()
        {
            InitializeComponent();

            // Create an instance of the TravelSystemDbContext and pass it to the ViewModel
            var dbContext = new TravelSystemDbContext();
            _viewModel = new RestaurantViewModel(dbContext);

            // Set the DataContext to the ViewModel instance
            DataContext = _viewModel;
        }
    }
}
