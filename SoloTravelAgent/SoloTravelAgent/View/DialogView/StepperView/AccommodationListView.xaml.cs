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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SoloTravelAgent.ViewModel.StepperViewModel;

namespace SoloTravelAgent.View.DialogView.StepperView
{
    /// <summary>
    /// Interaction logic for AccommodationListView.xaml
    /// </summary>
    public partial class AccommodationListView : UserControl
    {
        private readonly AccommodationListViewModel _viewModel;
        public AccommodationListView(StepViewModel stepViewModel)
        {
            InitializeComponent();
            _viewModel = new AccommodationListViewModel(accommodationListBox, stepViewModel);
            DataContext = _viewModel;
            stepViewModel.AccommodationListViewModel = _viewModel;
        }
    }
}
