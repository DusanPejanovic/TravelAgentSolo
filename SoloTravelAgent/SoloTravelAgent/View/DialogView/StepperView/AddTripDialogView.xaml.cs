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
using Microsoft.EntityFrameworkCore;
using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.ViewModel;
using SoloTravelAgent.ViewModel.StepperViewModel;

namespace SoloTravelAgent.View.DialogView.StepperView
{
    /// <summary>
    /// Interaction logic for AddTripDialogView.xaml
    /// </summary>
    public partial class AddTripDialogView : Window
    {
        private readonly StepViewModel _viewModel;
        private readonly TravelSystemDbContext _dbContext;

        public AddTripDialogView(TripViewModel rvm, TravelSystemDbContext dbContext)
        {
            _viewModel = new StepViewModel();
            _dbContext = dbContext;
            InitializeComponent();

            _viewModel.AddTripAction += () =>
            {
                var existingAttractions = GetExistingAttractions(_viewModel.Attractions);

                var newTrip = new Trip
                {
                    Name = _viewModel.Name,
                    Description = _viewModel.Description,
                    Price = _viewModel.Price,
                    StartDate = _viewModel.StartDate,
                    EndDate = _viewModel.EndDate,
                    Restaurants = _viewModel.Restaurants.ToList(),
                    Accommodations = _viewModel.Accommodations.ToList(),
                };

                foreach (var attraction in existingAttractions)
                {
                    newTrip.TripTouristAttractions.Add(new TripTouristAttraction
                    {
                        Trip = newTrip,
                        TouristAttraction = attraction
                    });
                }

                rvm.AddTrip(newTrip);
                _dbContext.SaveChanges();
            };


            _viewModel.RequestClose += () => this.Close();
            DataContext = _viewModel;
        }

        private IEnumerable<TouristAttraction> GetExistingAttractions(IEnumerable<TouristAttraction> attractions)
        {
            var existingAttractions = new List<TouristAttraction>();

            foreach (var attraction in attractions)
            {
                var existingAttraction = _dbContext.TouristAttractions.FirstOrDefault(a => a.Id == attraction.Id);
                if (existingAttraction != null)
                {
                    // Attach the existing attraction to the context if not already tracked
                    if (_dbContext.Entry(existingAttraction).State == EntityState.Detached)
                        _dbContext.Attach(existingAttraction);

                    existingAttractions.Add(existingAttraction);
                }
            }

            return existingAttractions;
        }

        public void ViewModel_GoToNextStep()
        {
            _viewModel.NextStep();
        }

        public void ViewModel_GoToPreviousStep()
        {
            _viewModel.PreviousStep();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}