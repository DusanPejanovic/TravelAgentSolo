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
    public class EdtiAccommodationSubViewModel : INotifyPropertyChanged
    {
        private Accommodation _selectedAccommodation;
        private string _name;
        private string _address;
        private int _stars;
        private string _website;
        private string _phoneNumber;

        public event Action RequestClose = delegate { };
        public event PropertyChangedEventHandler PropertyChanged;

        public Accommodation SelectedAccommodation
        {
            get => _selectedAccommodation;
            set
            {
                _selectedAccommodation = value;
                OnPropertyChanged(nameof(SelectedAccommodation));
                LoadAccommodationDetails();
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

        public EdtiAccommodationSubViewModel(Accommodation selectedAccomomdation, ObservableCollection<Accommodation> accommodations)
        {
            SelectedAccommodation = selectedAccomomdation;
            SaveCommand = new DelegateCommand(EditAccommodation);
            Accommodations = accommodations;
        }

        private void LoadAccommodationDetails()
        {
            if (SelectedAccommodation != null)
            {
                Name = SelectedAccommodation.Name;
                Address = SelectedAccommodation.Address;
                Stars = SelectedAccommodation.Stars;
                Website = SelectedAccommodation.Website;
                PhoneNumber = SelectedAccommodation.PhoneNumber;
            }
            else
            {
                // Reset the fields when no restaurant is selected
                Name = string.Empty;
                Address = string.Empty;
                Stars = 0;
                Website = string.Empty;
                PhoneNumber = string.Empty;
            }
        }

        private void EditAccommodation()
        {
            if (SelectedAccommodation != null)
            {
                // Update the selected restaurant with the edited details
                SelectedAccommodation.Name = Name;
                SelectedAccommodation.Address = Address;
                SelectedAccommodation.Stars = Stars;
                SelectedAccommodation.Website = Website;
                SelectedAccommodation.PhoneNumber = PhoneNumber;

                OnPropertyChanged(nameof(Accommodation));
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