using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.View.DialogView.SubDialogs;
using SoloTravelAgent.ViewModel.DialogViewModel.SubDialogViewModel;
using SoloTravelAgent.ViewModel.DragDrop;

namespace SoloTravelAgent.ViewModels.DialogViewModels
{
    public class AddTripDialogViewModel : INotifyPropertyChanged
    {
        private string _name;
        private string _description;
        private decimal _price;
        private DateTime _startDate;
        private DateTime _endDate;
        private ObservableCollection<Restaurant> _restaurants;
        private ObservableCollection<Accommodation> _accommodations;
        private ObservableCollection<TouristAttraction> _touristAttractions;
        private Restaurant _selectedRestaurant;
        private Accommodation _selectedAccommodation;
        private TouristAttraction _selectedTouristAttraction;
        private ICommand _addRestaurantCommand;
        private ICommand _editRestaurantCommand;
        private ICommand _removeRestaurantCommand;
        private ICommand _addAccommodationCommand;
        private ICommand _editAccommodationCommand;
        private ICommand _removeAccommodationCommand;
        private ICommand _addAttractionCommand;
        private ICommand _editAttractionCommand;
        private ICommand _removeAttractionCommand;
        private ListBox _restaurantListBox;
        private ListBox _accommodationsListBox;
        private ListBox _attractionsListBox;


        public event PropertyChangedEventHandler PropertyChanged;
        public AttractionsDragDropViewModel AttractionsViewModel { get; set; }
        public RelayCommand<object> SaveAttractionsCommand { get; private set; }


        public AddTripDialogViewModel(ListBox restaurantListBox, ListBox accommodationListBox, ListBox attractionsListBox)
        {
            Restaurants = new ObservableCollection<Restaurant>();
            Accommodations = new ObservableCollection<Accommodation>();
            TouristAttractions = new ObservableCollection<TouristAttraction>();

            AddRestaurantCommand = new RelayCommand(AddRestaurant);
            EditRestaurantCommand = new RelayCommand(EditRestaurant, CanEditRestaurant);
            RemoveRestaurantCommand = new RelayCommand(RemoveRestaurant, CanRemoveRestaurant);

            AddAccommodationCommand = new RelayCommand(AddAccommodation);
            EditAccommodationCommand = new RelayCommand(EditAccommodation, CanEditAccommodation);
            RemoveAccommodationCommand = new RelayCommand(RemoveAccommodation, CanRemoveAccommodation);

            AddAttractionCommand = new RelayCommand(AddAttraction);
            EditAttractionCommand = new RelayCommand(EditAttraction, CanEditAttraction);
            RemoveAttractionCommand = new RelayCommand(RemoveAttraction, CanRemoveAttraction);

            _restaurantListBox = restaurantListBox;
            _accommodationsListBox = accommodationListBox;
            _attractionsListBox = attractionsListBox;

          
            SaveAttractionsCommand = new RelayCommand<object>(SaveAttractions);


        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

     

        public ObservableCollection<Restaurant> Restaurants
        {
            get => _restaurants;
            set
            {
                _restaurants = value;
                OnPropertyChanged(nameof(Restaurants));
            }
        }

        public ObservableCollection<Accommodation> Accommodations
        {
            get => _accommodations;
            set
            {
                _accommodations = value;
                OnPropertyChanged(nameof(Accommodations));
            }
        }

        public ObservableCollection<TouristAttraction> TouristAttractions
        {
            get => _touristAttractions;
            set
            {
                _touristAttractions = value;
                OnPropertyChanged(nameof(TouristAttractions));
            }
        }

        public Restaurant SelectedRestaurant
        {
            get => _selectedRestaurant;
            set
            {
                _selectedRestaurant = value;
                OnPropertyChanged(nameof(SelectedRestaurant));
                ((RelayCommand)EditRestaurantCommand).RaiseCanExecuteChanged();
                ((RelayCommand)RemoveRestaurantCommand).RaiseCanExecuteChanged();
            }
        }

        public Accommodation SelectedAccommodation
        {
            get => _selectedAccommodation;
            set
            {
                _selectedAccommodation = value;
                OnPropertyChanged(nameof(SelectedAccommodation));
                ((RelayCommand)EditAccommodationCommand).RaiseCanExecuteChanged();
                ((RelayCommand)RemoveAccommodationCommand).RaiseCanExecuteChanged();
            }
        }

        public TouristAttraction SelectedTouristAttraction
        {
            get => _selectedTouristAttraction;
            set
            {
                _selectedTouristAttraction = value;
                OnPropertyChanged(nameof(SelectedTouristAttraction));
                ((RelayCommand)EditAttractionCommand).RaiseCanExecuteChanged();
                ((RelayCommand)RemoveAttractionCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand AddRestaurantCommand
        {
            get => _addRestaurantCommand;
            set
            {
                _addRestaurantCommand = value;
                OnPropertyChanged(nameof(AddRestaurantCommand));
            }
        }

        public ICommand EditRestaurantCommand
        {
            get => _editRestaurantCommand;
            set
            {
                _editRestaurantCommand = value;
                OnPropertyChanged(nameof(EditRestaurantCommand));
            }
        }

        public ICommand RemoveRestaurantCommand
        {
            get => _removeRestaurantCommand;
            set
            {
                _removeRestaurantCommand = value;
                OnPropertyChanged(nameof(RemoveRestaurantCommand));
            }
        }

        public ICommand AddAccommodationCommand
        {
            get => _addAccommodationCommand;
            set
            {
                _addAccommodationCommand = value;
                OnPropertyChanged(nameof(AddAccommodationCommand));
            }
        }

        public ICommand EditAccommodationCommand
        {
            get => _editAccommodationCommand;
            set
            {
                _editAccommodationCommand = value;
                OnPropertyChanged(nameof(EditAccommodationCommand));
            }
        }

        public ICommand RemoveAccommodationCommand
        {
            get => _removeAccommodationCommand;
            set
            {
                _removeAccommodationCommand = value;
                OnPropertyChanged(nameof(RemoveAccommodationCommand));
            }
        }

        public ICommand AddAttractionCommand
        {
            get => _addAttractionCommand;
            set
            {
                _addAttractionCommand = value;
                OnPropertyChanged(nameof(AddAttractionCommand));
            }
        }

        public ICommand EditAttractionCommand
        {
            get => _editAttractionCommand;
            set
            {
                _editAttractionCommand = value;
                OnPropertyChanged(nameof(EditAttractionCommand));
            }
        }

        public ICommand RemoveAttractionCommand
        {
            get => _removeAttractionCommand;
            set
            {
                _removeAttractionCommand = value;
                OnPropertyChanged(nameof(RemoveAttractionCommand));
            }
        }

        private void AddRestaurant()
        {
            AddRestaurantSubViewModel subViewModel = new AddRestaurantSubViewModel(Restaurants);
            AddRestaurantSubDialogView subDialog = new AddRestaurantSubDialogView(subViewModel);
            subViewModel.RequestClose += () => subDialog.Close();
            subDialog.ShowDialog();

            // Check if the user saved the restaurant in the sub-dialog
            if (subDialog.DialogResult == true)
            {
                // Get the newly created restaurant from the sub-dialog view model
                Restaurant newRestaurant = new Restaurant
                {
                    Name = subViewModel.Name,
                    Address = subViewModel.Address,
                    Cuisine = subViewModel.Cuisine,
                    Website = subViewModel.Website,
                    PhoneNumber = subViewModel.PhoneNumber
                };

                // Add the restaurant to the list in the trip creation dialog
                Restaurants.Add(newRestaurant);
            }
        }
        public ListBox RestaurantListBox
        {
            get => _restaurantListBox;
            set
            {
                _restaurantListBox = value;
                OnPropertyChanged(nameof(RestaurantListBox));
            }
        }

        private void RefreshRestaurantListBox()
        {
            // Set the ItemsSource of restaurantListBox to null and then reassign the Restaurants collection
            RestaurantListBox.ItemsSource = null;
            RestaurantListBox.ItemsSource = Restaurants;
        }

        public ListBox AccommodationListBox
        {
            get => _accommodationsListBox;
            set
            {
                _accommodationsListBox = value;
                OnPropertyChanged(nameof(AccommodationListBox));
            }
        }

        private void RefreshAccommodationListBox()
        {
            // Set the ItemsSource of restaurantListBox to null and then reassign the Restaurants collection
            AccommodationListBox.ItemsSource = null;
            AccommodationListBox.ItemsSource = Accommodations;
        }


        private void EditRestaurant()
        {
            // Create an instance of the edit restaurant sub-dialog view model
            EditRestaurantSubViewModel subViewModel = new EditRestaurantSubViewModel(SelectedRestaurant, Restaurants);
            EditRestaurantSubDialogView subDialog = new EditRestaurantSubDialogView(subViewModel);
            subViewModel.RequestClose += () => subDialog.Close();
            subDialog.ShowDialog();
            OnPropertyChanged(nameof(Restaurants));
            OnPropertyChanged(nameof(SelectedRestaurant));
            RefreshRestaurantListBox();

            // The changes made in the sub-dialog will directly modify the properties of the SelectedRestaurant instance
        }


        private bool CanEditRestaurant()
        {
            return SelectedRestaurant != null;
        }

        private void RemoveRestaurant()
        {
            Restaurants.Remove(SelectedRestaurant);
        }

        private bool CanRemoveRestaurant()
        {
            return SelectedRestaurant != null;
        }

        private void AddAccommodation()
        {
            AddAccommodationSubViewModel subViewModel = new AddAccommodationSubViewModel(Accommodations);
            AddAccommodationSubDialogView subDialog = new AddAccommodationSubDialogView(subViewModel);
            subViewModel.RequestClose += () => subDialog.Close();
            subDialog.ShowDialog();

            // Check if the user saved the restaurant in the sub-dialog
            if (subDialog.DialogResult == true)
            {
                // Get the newly created restaurant from the sub-dialog view model
                Accommodation newAccommodation = new Accommodation
                {
                    Name = subViewModel.Name,
                    Address = subViewModel.Address,
                    Stars = subViewModel.Stars,
                    Website = subViewModel.Website,
                    PhoneNumber = subViewModel.PhoneNumber
                };

                // Add the restaurant to the list in the trip creation dialog
                Accommodations.Add(newAccommodation);
            }

        }

        private void EditAccommodation()
        {
            EdtiAccommodationSubViewModel subViewModel = new EdtiAccommodationSubViewModel(SelectedAccommodation, Accommodations);
            EditAccommodationSubDialogView subDialog = new EditAccommodationSubDialogView(subViewModel);
            subViewModel.RequestClose += () => subDialog.Close();
            subDialog.ShowDialog();
            OnPropertyChanged(nameof(Accommodations));
            OnPropertyChanged(nameof(SelectedAccommodation));
            RefreshAccommodationListBox();
        }

        private bool CanEditAccommodation()
        {
            return SelectedAccommodation != null;
        }

        private void RemoveAccommodation()
        {
            Accommodations.Remove(SelectedAccommodation);
        }

        private bool CanRemoveAccommodation()
        {
            return SelectedAccommodation != null;
        }


        private void SaveAttractions(object paramteter)
        {
            foreach (var attraction in AttractionsViewModel.AttractionsToAdd)
            {
                TouristAttractions.Add(attraction);
            }
        }


        private void AddAttraction()
        {
           // 
        }

        private void EditAttraction()
        {
            // Implementation...
        }

        private bool CanEditAttraction()
        {
            return SelectedTouristAttraction != null;
        }

        private void RemoveAttraction()
        {
            // Implementation...
        }

        private bool CanRemoveAttraction()
        {
            return SelectedTouristAttraction != null;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
