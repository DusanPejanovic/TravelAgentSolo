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
    public class AddAttractionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public TouristAttraction NewAttraction { get; private set; }

        public ICommand SaveCommand { get; set; }

        public ICommand CancelCommand { get; private set; }

        private Action _addAttractionAction;
        public event Action RequestClose = delegate { };

        public AddAttractionViewModel()
        {
            NewAttraction = new TouristAttraction();
            SaveCommand = new RelayCommand(_ => Save(), _ => CanSave());
            CancelCommand = new RelayCommand(_ => Cancel());

        }

        public string Name
        {
            get => NewAttraction.Name;
            set
            {
                if (NewAttraction.Name != value)
                {
                    NewAttraction.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Address
        {
            get => NewAttraction.Address;
            set
            {
                if (NewAttraction.Address != value)
                {
                    NewAttraction.Address = value;
                    OnPropertyChanged();
                }
            }
        }

        public decimal EntryFee
        {
            get => NewAttraction.EntryFee;
            set
            {
                if (NewAttraction.EntryFee != value)
                {
                    NewAttraction.EntryFee = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Website
        {
            get => NewAttraction.Website;
            set
            {
                if (NewAttraction.Website != value)
                {
                    NewAttraction.Website = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Description
        {
            get => NewAttraction.Description;
            set
            {
                if (NewAttraction.Description != value)
                {
                    NewAttraction.Description = value;
                    OnPropertyChanged();
                }
            }
        }

        private void Save()
        {
            _addAttractionAction?.Invoke();
            RequestClose.Invoke();
        }

        public bool CanSave()
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
