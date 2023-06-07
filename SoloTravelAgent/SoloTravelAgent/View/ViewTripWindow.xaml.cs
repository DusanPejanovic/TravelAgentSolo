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
using SoloTravelAgent.Model; // Assuming your model classes are under this namespace
using System.Windows;
using SoloTravelAgent.Model.Entities;

namespace SoloTravelAgent.View
{
    public partial class ViewTripWindow : Window
    {
        public Trip Trip { get; set; }

        public ViewTripWindow(Trip trip)
        {
            InitializeComponent();
            Trip = trip;
            DataContext = this;
        }
    }
}
