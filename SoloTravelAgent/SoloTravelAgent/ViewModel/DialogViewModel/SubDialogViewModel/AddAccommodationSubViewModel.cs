using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using SoloTravelAgent.Model.Entities;

namespace SoloTravelAgent.ViewModel.DialogViewModel.SubDialogViewModel
{
    public class AddAccommodationSubViewModel : INotifyPropertyChanged
    {
        private string _name;
        private string _address;
        private int _stars;
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

        public int Stars
        {
            get => _stars;
            set
            {
                _stars = value;
                OnPropertyChanged(nameof(Stars));
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

        public ObservableCollection<Accommodation> Accommodations { get; }

        public ICommand SaveCommand { get; }


        public AddAccommodationSubViewModel(ObservableCollection<Accommodation> accommodations)
        {
            SaveCommand = new DelegateCommand(AddAccommodation);
            Accommodations = accommodations;
        }

        private void AddAccommodation()
        {
            // Create a new restaurant object with the provided details
            Accommodation newAccommodation = new Accommodation
            {
                Name = Name,
                Address = Address,
                Stars = Stars,
                Website = Website,
                PhoneNumber = PhoneNumber
            };

            // Add the restaurant to the list or perform other operations as needed
            Accommodations.Add(newAccommodation);

            // Close the dialog
            RequestClose.Invoke();
        }


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}