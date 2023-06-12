using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using SoloTravelAgent.View.DialogView.StepperView;
using Syncfusion.UI.Xaml.ProgressBar;

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using SoloTravelAgent.Model.Service;
using SoloTravelAgent.Model.Entities;
using System.Diagnostics;
using SoloTravelAgent.ViewModel.DragDrop;
using SoloTravelAgent.Model.Data;
using System.Windows;

namespace SoloTravelAgent.ViewModel.StepperViewModel
{
    public class StepViewModel : ViewModelBase, INotifyPropertyChanged, IDataErrorInfo
    {
        private ObservableCollection<string> stepViewItems;
        private int selectedIndex;
        private UserControl currentView;
        private Dictionary<int, UserControl> viewCache;

        private bool showPreviousButton;
        private bool showNextButton;
        private bool showSaveButton;

        public event Action StepCompleted;
        public event Action GoToNextStep;
        public event Action GoToPreviousStep;

        private ObservableCollection<Restaurant> restaurants;
        private ObservableCollection<Accommodation> accommodations;
        private ObservableCollection<TouristAttraction> attractions;
        private string name;
        private string description;
        private decimal price;
        private double longitude;
        private double latitude;
        private DateTime startDate;
        private DateTime endDate;
        public ICommand SaveCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private Action _addTripAction;
        public event Action RequestClose = delegate { };

        private Dictionary<string, bool> _dirtyProperties = new Dictionary<string, bool>();


        public StepViewModel()
        {
            ShowSaveButton = false;
            ShowNextButton = true;
            StepViewItems = new ObservableCollection<string>();
            PopulateData();
           
            SelectedIndex = 0;
            viewCache = new Dictionary<int, UserControl>();
            Restaurants = new ObservableCollection<Restaurant>();
            Accommodations = new ObservableCollection<Accommodation>();
            Attractions = new ObservableCollection<TouristAttraction>();
      
            description = "";
            price = 0M;

            StartDate = DateTime.Now;
            EndDate = DateTime.Now.AddDays(14);
            NextStepCommand = new RelayCommand(NextStep, CanGoToNextStep);
            PreviousStepCommand = new RelayCommand(PreviousStep, CanGoToPreviousStep);
            SaveCommand = new RelayCommand(Save);
            viewCache[0] = new BasicInfoView(this);
            CurrentView = viewCache[0];

        }



        public event Action AddTripAction
        {
            add => _addTripAction += value;
            remove => _addTripAction -= value;
        }


        private void Save()
        {
            if (!decimal.TryParse(Price.ToString(), out decimal parsedPrice))
            {
                MessageBox.Show("Price has to be decimal. ", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(Name) || Price == 0 || StartDate == null || EndDate == null || string.IsNullOrEmpty(Description))
            {
                MessageBox.Show("Please fill in all the required fields in the Basic Info Step.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _addTripAction?.Invoke();
            RequestClose.Invoke();
        }


        public ObservableCollection<string> StepViewItems
        {
            get => stepViewItems;
            set
            {
                stepViewItems = value;
                OnPropertyChanged(nameof(StepViewItems));
            }
        }

        private RestaurantListViewModel restaurantListViewModel;

        public RestaurantListViewModel RestaurantListViewModel
        {
            get => restaurantListViewModel;
            set
            {
                restaurantListViewModel = value;         
                Restaurants = restaurantListViewModel.Restaurants;
            }
        }

        private AccommodationListViewModel accommodationListViewModel;

        public AccommodationListViewModel AccommodationListViewModel
        {
            get => accommodationListViewModel;
            set
            {
                accommodationListViewModel = value;
                Accommodations = accommodationListViewModel.Accommodations;
            }


        }
        

        private AttractionsDragDropViewModel attractionsViewModel;

        public AttractionsDragDropViewModel AttractionsDragDropViewModel
        {
            get => attractionsViewModel;
            set
            {
                attractionsViewModel = value;
                Attractions = attractionsViewModel.AttractionsToAdd; 

            }
        }

        public ObservableCollection<Accommodation> Accommodations
        {
            get => accommodations;
            set
            {
                accommodations = value;
                OnPropertyChanged(nameof(Accommodations));
            }
        }

        public ObservableCollection<TouristAttraction> Attractions
        {
            get => attractions;
            set
            {
                attractions = value;
                OnPropertyChanged(nameof(Attractions));
            }
        }



        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
                MarkAsDirty(nameof(Name));
                RaisePropertyChanged();
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
                MarkAsDirty(nameof(Description));
                RaisePropertyChanged();
            }
        }

        public decimal Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged(nameof(Price));
                MarkAsDirty(nameof(Price));
                RaisePropertyChanged();
            }
        }

        public double Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
                OnPropertyChanged(nameof(Longitude));
                MarkAsDirty(nameof(Longitude));
                RaisePropertyChanged();
            }
        }

        public double Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
                OnPropertyChanged(nameof(Latitude));
                MarkAsDirty(nameof(Latitude));
                RaisePropertyChanged();
            }
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }


        public ObservableCollection<Restaurant> Restaurants
        {
            get => restaurants;
            set
            {
                restaurants = value;
                OnPropertyChanged(nameof(Restaurants));
            }
        }

        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));
                UpdateButtonVisibility();
            }
        }

        public bool ShowPreviousButton
        {
            get => showPreviousButton;
            set
            {
                showPreviousButton = value;
                OnPropertyChanged(nameof(ShowPreviousButton));
            }
        }

        public bool ShowNextButton
        {
            get => showNextButton;
            set
            {
                showNextButton = value;
                OnPropertyChanged(nameof(ShowNextButton));
            }
        }

        public bool ShowSaveButton
        {
            get => showSaveButton;
            set
            {
                showSaveButton = value;
                OnPropertyChanged(nameof(ShowSaveButton));
            }
        }

        public UserControl CurrentView
        {
            get
            {
                if (!viewCache.ContainsKey(selectedIndex))
                    return null;

                return viewCache[selectedIndex];
            }
            set
            {
                viewCache[selectedIndex] = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public ICommand NextStepCommand { get; }
        public ICommand PreviousStepCommand { get; }

        private bool CanGoToNextStep()
        {
            return SelectedIndex < StepViewItems.Count - 1;
        }

        public void NextStep()
        {
            Debug.WriteLine(Accommodations.Count());
            Debug.WriteLine(Attractions.Count());
            Debug.WriteLine(Name);
            Debug.WriteLine(Description);
            Debug.WriteLine(Price);

            if (CanGoToNextStep())
            {
                SelectedIndex++;
                UpdateCurrentView();
            }
            else
            {
                ShowNextButton = false;
                ShowSaveButton = true; 
            }
        }

        private bool CanGoToPreviousStep()
        {
            return SelectedIndex > 0;
        }

        public void PreviousStep()
        {
            Debug.WriteLine(Name);
            if (CanGoToPreviousStep())
            {
                SelectedIndex--;
                UpdateCurrentView();
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void PopulateData()
        {
            StepViewItems.Add("Basic Info");
            StepViewItems.Add("Restaurants");
            StepViewItems.Add("Accommodations");
            StepViewItems.Add("Attractions");
        }

        private void UpdateCurrentView()
        {
            switch (SelectedIndex)
            {
                case 0:
                    if (!viewCache.ContainsKey(0))
                        viewCache[0] = new BasicInfoView(this);
                    CurrentView = viewCache[0];
                    break;
                case 1:
                    if (!viewCache.ContainsKey(1)) 
                        viewCache[1] = new RestaurantListView(this);
                    CurrentView = viewCache[1];
                    break;
                case 2:
                    if (!viewCache.ContainsKey(2))
                        viewCache[2] = new AccommodationListView(this);
                    CurrentView = viewCache[2];
                    break;
                case 3:
                    if (!viewCache.ContainsKey(3))
                        viewCache[3] = new AttractionsDragDrop(this);
                    CurrentView = viewCache[3];
                    break;
            }
        }



        public string Error { get; set; }
        private void MarkAsDirty(string propertyName)
        {
            _dirtyProperties[propertyName] = true;
        }

        private bool IsPropertyDirty(string propertyName)
        {
            return _dirtyProperties.ContainsKey(propertyName) && _dirtyProperties[propertyName];
        }

        public string this[string columnName]
        {
            get
            {
                if (!IsPropertyDirty(columnName))
                {
                    return null;
                }

                switch (columnName)
                {
                    case nameof(Name):
                        return ValidateName();
                    case nameof(Description):
                        return ValidateDescription();
                    case nameof(Price):
                        return ValidatePrice();
                    //case nameof(Latitude):
                    //    return ValidatePrice();
                    //case nameof(Longitude):
                    //    return ValidatePrice();

                    default:
                        return null;
                }
            }
        }

        private string ValidateName()
        {

            if (string.IsNullOrWhiteSpace(Name))
            {
                return "Name cannot be empty";
            }

            return null;
        }
        private string ValidateDescription()
        {

            if (string.IsNullOrWhiteSpace(Description))
            {
                return "Description cannot be empty";
            }

            return null;
        }

        private string ValidatePrice()
        {

            if (Price < 0)
            {
                return "Wrong value";
            }

            return null;
        }

        //private string ValidatePrice()
        //{

        //    if (decimal.Longitude.)
        //    {
        //        return "Wrong value";
        //    }

        //    return null;
        //}
        private void UpdateButtonVisibility()
        {
            ShowPreviousButton = CanGoToPreviousStep();
            ShowNextButton = CanGoToNextStep();
            ShowSaveButton = !ShowNextButton;
        }


    }
}

