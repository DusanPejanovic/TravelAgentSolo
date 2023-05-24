using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using SoloTravelAgent.Model.Entities;
using System.ComponentModel;

namespace SoloTravelAgent.ViewModel.DialogViewModel
{
    internal class EditAccommodationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Accommodation AccommodationToEdit { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        private Action _updateAccommodationAction;
        public event Action RequestClose = delegate { };

        public String originalName;
        public String originalAddress;
        public int originalStars;
        public String originalWebsite;
        public String originalPhoneNumber;

        public EditAccommodationViewModel(Accommodation accommodationToEdit, Action updateAccommodationAction)
        {
            originalName = accommodationToEdit.Name;
            originalAddress = accommodationToEdit.Address;
            originalStars = accommodationToEdit.Stars;
            originalWebsite = accommodationToEdit.Website;
            originalPhoneNumber = accommodationToEdit.PhoneNumber;

            AccommodationToEdit = accommodationToEdit;

            SaveCommand = new RelayCommand(_ => Save(), _ => CanSave());
            CancelCommand = new RelayCommand(_ => Cancel());
            _updateAccommodationAction = updateAccommodationAction;
        }

        public bool HasUnsavedChanges()
        {
            bool same = originalName == AccommodationToEdit.Name &&
                        originalAddress == AccommodationToEdit.Address &&
                        originalStars == AccommodationToEdit.Stars &&
                        originalWebsite == AccommodationToEdit.Website &&
                        originalPhoneNumber == AccommodationToEdit.PhoneNumber;

            return !same;
        }

        public string Name
        {
            get => AccommodationToEdit.Name;
            set
            {
                if (AccommodationToEdit.Name != value)
                {
                    AccommodationToEdit.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Address
        {
            get => AccommodationToEdit.Address;
            set
            {
                if (AccommodationToEdit.Address != value)
                {
                    AccommodationToEdit.Address = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Stars
        {
            get => AccommodationToEdit.Stars;
            set
            {
                if (AccommodationToEdit.Stars != value)
                {
                    AccommodationToEdit.Stars = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Website
        {
            get => AccommodationToEdit.Website;
            set
            {
                if (AccommodationToEdit.Website != value)
                {
                    AccommodationToEdit.Website = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PhoneNumber
        {
            get => AccommodationToEdit.PhoneNumber;
            set
            {
                if (AccommodationToEdit.PhoneNumber != value)
                {
                    AccommodationToEdit.PhoneNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        private void Save()
        {
            _updateAccommodationAction?.Invoke();
            RequestClose.Invoke();
        }

        private bool CanSave()
        {
            // Add your logic to determine if the Save button should be enabled
            return true;
        }

        private void Cancel()
        {
            RequestClose.Invoke();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

