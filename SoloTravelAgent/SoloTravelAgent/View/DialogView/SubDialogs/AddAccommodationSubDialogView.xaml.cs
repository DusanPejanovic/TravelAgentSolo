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
using SoloTravelAgent.ViewModel.DialogViewModel.SubDialogViewModel;

namespace SoloTravelAgent.View.DialogView.SubDialogs
{
    /// <summary>
    /// Interaction logic for AddAccommodationSubDialogView.xaml
    /// </summary>
    public partial class AddAccommodationSubDialogView : Window
    {
        public AddAccommodationSubDialogView(AddAccommodationSubViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    
    }
}
