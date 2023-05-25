using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SoloTravelAgent.Model.Entities;
using System.Runtime.CompilerServices;

namespace SoloTravelAgent.ViewModel.DialogViewModel
{
    internal class EditTripViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Trip TripToEdit { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        private Action _updateTripAction;
        public event Action RequestClose = delegate { };

        public String originalName;
        public String originalDescription;
        public DateTime originalStartDate;
        public DateTime originalEndDate;
        public decimal originalPrice;

        public EditTripViewModel(Trip tripToEdit, Action updateTripAction)
        {
            originalName = tripToEdit.Name;
            originalDescription = tripToEdit.Description;
            originalStartDate = tripToEdit.StartDate;
            originalEndDate = tripToEdit.EndDate;
            originalPrice = tripToEdit.Price;

            TripToEdit = tripToEdit;
            SaveCommand = new RelayCommand(_ => Save(), _ => CanSave());
            CancelCommand = new RelayCommand(_ => Cancel());
            _updateTripAction = updateTripAction;
        }

        public bool HasUnsavedChanges()
        {
            bool same = originalName == TripToEdit.Name &&
                        originalDescription == TripToEdit.Description &&
                        originalStartDate == TripToEdit.StartDate &&
                        originalEndDate == TripToEdit.EndDate &&
                        originalPrice == TripToEdit.Price;

            return !same;
        }


        public string Name
        {
            get => TripToEdit.Name;
            set
            {
                if (TripToEdit.Name != value)
                {
                    TripToEdit.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Description
        {
            get => TripToEdit.Description;
            set
            {
                if (TripToEdit.Description != value)
                {
                    TripToEdit.Description = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime StartDate
        {
            get => TripToEdit.StartDate;
            set
            {
                if (TripToEdit.StartDate != value)
                {
                    TripToEdit.StartDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime EndDate
        {
            get => TripToEdit.EndDate;
            set
            {
                if (TripToEdit.EndDate != value)
                {
                    TripToEdit.EndDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public decimal Price
        {
            get => TripToEdit.Price;
            set
            {
                if (TripToEdit.Price != value)
                {
                    TripToEdit.Price = value;
                    OnPropertyChanged();
                }
            }
        }

        private void Save()
        {
            _updateTripAction?.Invoke();
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
