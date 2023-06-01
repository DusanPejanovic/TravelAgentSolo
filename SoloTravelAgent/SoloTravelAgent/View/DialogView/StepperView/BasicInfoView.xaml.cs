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
    /// Interaction logic for BasicInfoView.xaml
    /// </summary>
    public partial class BasicInfoView : UserControl
    {

        //private readonly BasicInfoViewModel _viewModel;

        private readonly StepViewModel _stepViewModel;
        public BasicInfoView(StepViewModel stepViewModel)
        {
            InitializeComponent();
            //_viewModel = new BasicInfoViewModel(stepViewModel);
            //DataContext = _viewModel;
            //stepViewModel.BasicInfoViewModel = _viewModel;
            DataContext = stepViewModel;
        }
    }
}
