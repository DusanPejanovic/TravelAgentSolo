using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.Model.Service;

namespace SoloTravelAgent.ViewModel.DragDrop
{
    public class AttractionsDragDropViewModel : ViewModelBase
    {
        private ObservableCollection<TouristAttraction> _attractions;
        private ObservableCollection<TouristAttraction> _attractionsToAdd;
        private TouristAttractionService _attractionService;
        private TripService _tripService;
        private TouristAttraction _draggedAttraction;
        public RelayCommand SaveCommand { get; private set; }
        private readonly Action<ObservableCollection<TouristAttraction>> _addAttractionsCallback;

 
        public ObservableCollection<TouristAttraction> Attractions
        {
            get { return _attractions; }
            set { Set(ref _attractions, value); }
        }

        public ObservableCollection<TouristAttraction> AttractionsToAdd
        {
            get { return _attractionsToAdd; }
            set { Set(ref _attractionsToAdd, value); }
        }

        public ICommand AddAttractionCommand { get; private set; }

        public AttractionsDragDropViewModel(TravelSystemDbContext dbContext)
        {
            _tripService = new TripService(dbContext);
            _attractionService = new TouristAttractionService(dbContext);

            Attractions = new ObservableCollection<TouristAttraction>();
            AttractionsToAdd = new ObservableCollection<TouristAttraction>();

          
            AddAttractionCommand = new RelayCommand<TouristAttraction>(AddAttraction);

           
            var allAttractions = _attractionService.GetAllAttractions();

         
          

      
            foreach (var attraction in allAttractions)
            {
                Attractions.Add(attraction);
            }


        }

 

        private void AddAttraction(TouristAttraction attraction)
        {
            if (!AttractionsToAdd.Contains(attraction))
            {
                AttractionsToAdd.Add(attraction);
                Attractions.Remove(attraction);
            }
        }


        private TouristAttraction _selectedAttraction;
        public TouristAttraction SelectedAttraction
        {
            get { return _selectedAttraction; }
            set { Set(ref _selectedAttraction, value); }
        }

        private TouristAttraction _selectedAttractionToAdd;
        public TouristAttraction SelectedAttractionToAdd
        {
            get { return _selectedAttractionToAdd; }
            set { Set(ref _selectedAttractionToAdd, value); }
        }

        internal void SaveData()
        {
            throw new NotImplementedException();
        }
    }
}