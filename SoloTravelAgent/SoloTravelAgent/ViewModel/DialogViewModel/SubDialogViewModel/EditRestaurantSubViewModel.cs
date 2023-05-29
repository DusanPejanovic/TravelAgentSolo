using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Prism.Commands;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.View.DialogView.SubDialogs;

namespace SoloTravelAgent.ViewModel.DialogViewModel.SubDialogViewModel
{
    public class EditRestaurantSubViewModel : INotifyPropertyChanged
    {
        private Restaurant _selectedRestaurant;
        private string _name;
        private string _address;
        private string _cuisine;
        private string _website;
        private string _phoneNumber;

        public event Action RequestClose = delegate { };
        public event PropertyChangedEventHandler PropertyChanged;

        public Restaurant SelectedRestaurant
        {
            get => _selectedRestaurant;
            set
            {
                _selectedRestaurant = value;
                OnPropertyChanged(nameof(SelectedRestaurant));
                LoadRestaurantDetails();
            }
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

        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        public string Cuisine
        {
            get => _cuisine;
            set
            {
                _cuisine = value;
                OnPropertyChanged(nameof(Cuisine));
            }
        }

        public string Website
        {
            get => _website;
            set
            {
                _website = value;
                OnPropertyChanged(nameof(Website));
            }
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        public ObservableCollection<Restaurant> Restaurants { get; }

        public ICommand SaveCommand { get; }

        public EditRestaurantSubViewModel(Restaurant selectedRestaurant, ObservableCollection<Restaurant> restaurants)
        {
            SelectedRestaurant = selectedRestaurant;
            SaveCommand = new DelegateCommand(EditRestaurant);
            Restaurants = restaurants;
        }

        private void LoadRestaurantDetails()
        {
            if (SelectedRestaurant != null)
            {
                Name = SelectedRestaurant.Name;
                Address = SelectedRestaurant.Address;
                Cuisine = SelectedRestaurant.Cuisine;
                Website = SelectedRestaurant.Website;
                PhoneNumber = SelectedRestaurant.PhoneNumber;
            }
            else
            {
                // Reset the fields when no restaurant is selected
                Name = string.Empty;
                Address = string.Empty;
                Cuisine = string.Empty;
                Website = string.Empty;
                PhoneNumber = string.Empty;
            }
        }

        private void EditRestaurant()
        {
            if (SelectedRestaurant != null)
            {
                // Update the selected restaurant with the edited details
                SelectedRestaurant.Name = Name;
                SelectedRestaurant.Address = Address;
                SelectedRestaurant.Cuisine = Cuisine;
                SelectedRestaurant.Website = Website;
                SelectedRestaurant.PhoneNumber = PhoneNumber;

                OnPropertyChanged(nameof(Restaurants));
                // Close the dialog
                RequestClose.Invoke();
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
