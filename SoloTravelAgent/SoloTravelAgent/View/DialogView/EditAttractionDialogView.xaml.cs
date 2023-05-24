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
using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.ViewModel;
using SoloTravelAgent.ViewModel.DialogViewModel;

namespace SoloTravelAgent.View.DialogView
{
    /// <summary>
    /// Interaction logic for EditAttractionDialogView.xaml
    /// </summary>
    public partial class EditAttractionDialogView : Window
    {
        private TouristAttraction _selectedAttraction;
        private readonly EditAttractionViewModel _viewModel;
        public EditAttractionDialogView(TouristAttraction selectedAttraction, TouristAttractionsViewModel rvm)
        {

            InitializeComponent();
            _selectedAttraction = selectedAttraction;
            var dbContext = new TravelSystemDbContext();
            _viewModel = new EditAttractionViewModel(selectedAttraction, () => rvm.UpdateTouristAttraction());
            _viewModel.RequestClose += () => this.Close();


            DataContext = _viewModel;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.HasUnsavedChanges())
            {
                var msg = $"You have unsaved changes. Are you sure you want to discard them?";
                var result = MessageBox.Show(msg, "Unsaved changes", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }



    }

}
