
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.View.DialogView.SubDialogs;
using SoloTravelAgent.ViewModel.DialogViewModel.SubDialogViewModel;

namespace SoloTravelAgent.ViewModel.StepperViewModel
{
    public class AccommodationListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Accommodation> _accommodations;
        private Accommodation _selectedAccommodation;
        private ICommand _addAccommodationCommand;
        private ICommand _editAccommodationCommand;
        private ICommand _removeAccommodationCommand;
        private ListBox _accommodationsListBox;
        public event PropertyChangedEventHandler PropertyChanged;
        private StepViewModel _stepViewModel;
        public AccommodationListViewModel(ListBox accommodationListBox, StepViewModel stepViewModel) 
        {
            Accommodations = new ObservableCollection<Accommodation>();
            AddAccommodationCommand = new GalaSoft.MvvmLight.Command.RelayCommand(AddAccommodation);
            EditAccommodationCommand = new GalaSoft.MvvmLight.Command.RelayCommand(EditAccommodation, CanEditAccommodation);
            RemoveAccommodationCommand = new GalaSoft.MvvmLight.Command.RelayCommand(RemoveAccommodation, CanRemoveAccommodation);
            _accommodationsListBox = accommodationListBox;
            _stepViewModel = stepViewModel;
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

        public Accommodation SelectedAccommodation
        {
            get => _selectedAccommodation;
            set
            {
                _selectedAccommodation = value;
                OnPropertyChanged(nameof(SelectedAccommodation));
                ((GalaSoft.MvvmLight.Command.RelayCommand)EditAccommodationCommand).RaiseCanExecuteChanged();
                ((GalaSoft.MvvmLight.Command.RelayCommand)RemoveAccommodationCommand).RaiseCanExecuteChanged();
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

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void SaveData()
        {
            throw new NotImplementedException();
        }
    }
}
