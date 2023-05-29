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
using SoloTravelAgent.ViewModel.DragDrop;
using SoloTravelAgent.ViewModels.DialogViewModels;

namespace SoloTravelAgent.View.DragDrop
{
    public partial class AttractionsDragDrop : Window
    {
        private readonly AttractionsDragDropViewModel _viewModel;
        Point startPoint = new Point();

        public AttractionsDragDrop(AddTripDialogViewModel avm)
        {
            InitializeComponent();
            var dbContext = new TravelSystemDbContext();
            _viewModel = new AttractionsDragDropViewModel(dbContext);

            DataContext = _viewModel;
        }
        private void ListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void ListView_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DataGrid dataGrid = sender as DataGrid;
                DataGridRow dataGridRow = FindAncestor<DataGridRow>((DependencyObject)e.OriginalSource);

                // If dataGridRow is null, just return from the method
                if (dataGridRow == null)
                {
                    return;
                }

                TouristAttraction attraction = (TouristAttraction)dataGrid.ItemContainerGenerator.ItemFromContainer(dataGridRow);

                DataObject dragData = new DataObject("myFormat", attraction);
                System.Windows.DragDrop.DoDragDrop(dataGridRow, dragData, DragDropEffects.Move);
            }
        }

        private void ListView_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                TouristAttraction attraction = e.Data.GetData("myFormat") as TouristAttraction;
                _viewModel.AddAttractionCommand.Execute(attraction);
            }
        }

        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void ListView_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }
    }
}
