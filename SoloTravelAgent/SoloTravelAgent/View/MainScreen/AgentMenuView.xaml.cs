using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace SoloTravelAgent.View.Controls
{
    /// <summary>
    /// Interaction logic for MenuControl.xaml
    /// </summary>
    public partial class AgentMenuView : UserControl, INotifyPropertyChanged
    {

        public static readonly DependencyProperty IsMenuVisibleProperty =
        DependencyProperty.Register("IsMenuVisible", typeof(bool), typeof(AgentMenuView), new PropertyMetadata(true));

        public bool IsMenuVisible
        {
            get { return (bool)GetValue(IsMenuVisibleProperty); }
            set
            {
                if (IsMenuVisible != value)
                {
                    SetValue(IsMenuVisibleProperty, value);
                    OnPropertyChanged(nameof(IsMenuVisible));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AgentMenuView()
        {
            InitializeComponent();
        }
    }
}
