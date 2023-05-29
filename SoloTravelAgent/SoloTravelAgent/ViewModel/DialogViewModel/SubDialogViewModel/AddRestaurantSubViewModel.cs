using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Prism.Commands;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.View.DialogView.SubDialogs;

namespace SoloTravelAgent.ViewModel.DialogViewModel.SubDialogViewModel
{
    public class AddRestaurantSubViewModel : INotifyPropertyChanged
    {
        private string _name;
        private string _address;
        private string _cuisine;
        private string _website;
        private string _phoneNumber;
        public event Action RequestClose = delegate { };
        public event PropertyChangedEventHandler PropertyChanged;

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


        public AddRestaurantSubViewModel(ObservableCollection<Restaurant> restaurants)
        {
            SaveCommand = new DelegateCommand(AddRestaurant);
            Restaurants = restaurants;
        }

        private void AddRestaurant()
        {
            // Create a new restaurant object with the provided details
            Restaurant newRestaurant = new Restaurant
            {
                Name = Name,
                Address = Address,
                Cuisine = Cuisine,
                Website = Website,
                PhoneNumber = PhoneNumber
            };

            // Add the restaurant to the list or perform other operations as needed
            Restaurants.Add(newRestaurant);

            // Close the dialog
            RequestClose.Invoke();
        }


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
