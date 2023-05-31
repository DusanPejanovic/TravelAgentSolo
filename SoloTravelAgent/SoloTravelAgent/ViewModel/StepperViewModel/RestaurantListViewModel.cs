using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Diagnostics;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.View.DialogView.SubDialogs;
using SoloTravelAgent.ViewModel.DialogViewModel.SubDialogViewModel;

namespace SoloTravelAgent.ViewModel.StepperViewModel
{
    public class RestaurantListViewModel:INotifyPropertyChanged
    {
        private ObservableCollection<Restaurant> _restaurants;
        private Restaurant _selectedRestaurant;
        private ICommand _addRestaurantCommand;
        private ICommand _editRestaurantCommand;
        private ICommand _removeRestaurantCommand;
        private ListBox _restaurantListBox;
        private StepViewModel _stepViewModel;   

        public event PropertyChangedEventHandler PropertyChanged;


        public RestaurantListViewModel(ListBox restaurantListBox, StepViewModel stepViewModel) {
            Restaurants = new ObservableCollection<Restaurant>();
            AddRestaurantCommand = new GalaSoft.MvvmLight.Command.RelayCommand(AddRestaurant);
            EditRestaurantCommand = new GalaSoft.MvvmLight.Command.RelayCommand(EditRestaurant, CanEditRestaurant);
            RemoveRestaurantCommand = new GalaSoft.MvvmLight.Command.RelayCommand(RemoveRestaurant, CanRemoveRestaurant);
            _restaurantListBox = restaurantListBox;
            _stepViewModel = stepViewModel; 
        }

        public RestaurantListViewModel()
        {
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

        public Restaurant SelectedRestaurant
        {
            get => _selectedRestaurant;
            set
            {
                _selectedRestaurant = value;
                OnPropertyChanged(nameof(SelectedRestaurant));
                ((GalaSoft.MvvmLight.Command.RelayCommand)EditRestaurantCommand).RaiseCanExecuteChanged();
                ((GalaSoft.MvvmLight.Command.RelayCommand)RemoveRestaurantCommand).RaiseCanExecuteChanged();
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

        private void AddRestaurant()
        {
            AddRestaurantSubViewModel subViewModel = new AddRestaurantSubViewModel(Restaurants);
            AddRestaurantSubDialogView subDialog = new AddRestaurantSubDialogView(subViewModel);
            subViewModel.RequestClose += () => subDialog.Close();
            subDialog.ShowDialog();


            if (subDialog.DialogResult == true)
            {
      
                Restaurant newRestaurant = new Restaurant
                {
                    Name = subViewModel.Name,
                    Address = subViewModel.Address,
                    Cuisine = subViewModel.Cuisine,
                    Website = subViewModel.Website,
                    PhoneNumber = subViewModel.PhoneNumber
                };

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

            RestaurantListBox.ItemsSource = null;
            RestaurantListBox.ItemsSource = Restaurants;
        }


        private void EditRestaurant()
        {
           
            EditRestaurantSubViewModel subViewModel = new EditRestaurantSubViewModel(SelectedRestaurant, Restaurants);
            EditRestaurantSubDialogView subDialog = new EditRestaurantSubDialogView(subViewModel);
            subViewModel.RequestClose += () => subDialog.Close();
            subDialog.ShowDialog();
            OnPropertyChanged(nameof(Restaurants));
            OnPropertyChanged(nameof(SelectedRestaurant));
            RefreshRestaurantListBox();

           
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

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void SaveData()
        {
          
        }
    }
}
