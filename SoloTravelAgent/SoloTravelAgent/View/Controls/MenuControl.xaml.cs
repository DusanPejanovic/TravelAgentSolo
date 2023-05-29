using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SoloTravelAgent.View.Controls
{
    /// <summary>
    /// Interaction logic for MenuControl.xaml
    /// </summary>
    public partial class MenuControl : UserControl, INotifyPropertyChanged
    {

        public static readonly DependencyProperty IsMenuVisibleProperty =
        DependencyProperty.Register("IsMenuVisible", typeof(bool), typeof(MenuControl), new PropertyMetadata(true));

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

        public MenuControl()
        {
            InitializeComponent();
        }
    }
}
