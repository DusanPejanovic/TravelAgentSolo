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
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.ViewModel;
using SoloTravelAgent.ViewModel.DialogViewModel;

namespace SoloTravelAgent.View.DialogView
{
    /// <summary>
    /// Interaction logic for AddAttractionDialogView.xaml
    /// </summary>
    public partial class AddAttractionDialogView : Window
    {
        private readonly AddAttractionViewModel _viewModel;

        public AddAttractionDialogView(TouristAttractionsViewModel rvm)
        {
            InitializeComponent();
            _viewModel = new AddAttractionViewModel();
            _viewModel.RequestClose += () => this.Close();
            _viewModel.SaveCommand = new RelayCommand(_ =>
            {
                var newAttraction = new TouristAttraction
                {
                    Name = _viewModel.Name,
                    Address = _viewModel.Address,
                    Description = _viewModel.Description,
                    Website = _viewModel.Website,
                    EntryFee = _viewModel.EntryFee
                };
                rvm.AddTouristAttraction(newAttraction);
                this.Close();
            }, _ => _viewModel.CanSave());
            DataContext = _viewModel;
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
