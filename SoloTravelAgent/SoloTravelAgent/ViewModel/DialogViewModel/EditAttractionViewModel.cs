using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using SoloTravelAgent.Model.Entities;

namespace SoloTravelAgent.ViewModel.DialogViewModel
{
    internal class EditAttractionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public TouristAttraction AttractionToEdit { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        private Action _updateAttractionAction;
        public event Action RequestClose = delegate { };

        public String originalName;
        public String originalAddress;
        public String originalDescription;
        public decimal originalEntryFee;
        public String originalWebsite;

        public EditAttractionViewModel(TouristAttraction attractionToEdit, Action updateAttractionAction)
        {
            originalName = attractionToEdit.Name;
            originalAddress = attractionToEdit.Address;
            originalDescription = attractionToEdit.Description;
            originalEntryFee = attractionToEdit.EntryFee;
            originalWebsite = attractionToEdit.Website;

            AttractionToEdit = attractionToEdit;

            SaveCommand = new RelayCommand(_ => Save(), _ => CanSave());
            CancelCommand = new RelayCommand(_ => Cancel());
            _updateAttractionAction = updateAttractionAction;
        }

        public bool HasUnsavedChanges()
        {
            bool same = originalName == AttractionToEdit.Name &&
                        originalAddress == AttractionToEdit.Address &&
                        originalDescription == AttractionToEdit.Description &&
                        originalEntryFee == AttractionToEdit.EntryFee &&
                        originalWebsite == AttractionToEdit.Website;

            return !same;
        }

        public string Name
        {
            get => AttractionToEdit.Name;
            set
            {
                if (AttractionToEdit.Name != value)
                {
                    AttractionToEdit.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Address
        {
            get => AttractionToEdit.Address;
            set
            {
                if (AttractionToEdit.Address != value)
                {
                    AttractionToEdit.Address = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Description
        {
            get => AttractionToEdit.Description;
            set
            {
                if (AttractionToEdit.Description != value)
                {
                    AttractionToEdit.Description = value;
                    OnPropertyChanged();
                }
            }
        }

        public decimal EntryFee
        {
            get => AttractionToEdit.EntryFee;
            set
            {
                if (AttractionToEdit.EntryFee != value)
                {
                    AttractionToEdit.EntryFee = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Website
        {
            get => AttractionToEdit.Website;
            set
            {
                if (AttractionToEdit.Website != value)
                {
                    AttractionToEdit.Website = value;
                    OnPropertyChanged();
                }
            }
        }

        private void Save()
        {
            _updateAttractionAction?.Invoke();
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
