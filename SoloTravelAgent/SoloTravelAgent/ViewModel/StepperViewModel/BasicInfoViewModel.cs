using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace SoloTravelAgent.ViewModel.StepperViewModel
{
    public class BasicInfoViewModel : INotifyPropertyChanged
    {
        private string _name;
        private string _description;
        private decimal _price;
        private DateTime _startDate;
        private DateTime _endDate;
        public event PropertyChangedEventHandler PropertyChanged;
        private StepViewModel _stepViewModel;
        public BasicInfoViewModel(StepViewModel stepViewModel) { 
        
            StartDate = DateTime.Now;   
            EndDate = DateTime.Now.AddDays(7);
            _stepViewModel = stepViewModel;
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public decimal Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}