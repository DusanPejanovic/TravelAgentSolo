using System;
using System.Collections.Generic;
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
    /// Interaction logic for MenuContentControl.xaml
    /// </summary>
    public partial class MenuContentControl : UserControl
    {
        public static readonly DependencyProperty MainSectionContentProperty =
            DependencyProperty.Register("MainSectionContent", typeof(object), typeof(MenuContentControl), new PropertyMetadata(null));

        public static readonly DependencyProperty IsMenuVisibleProperty =
        DependencyProperty.Register("IsMenuVisible", typeof(bool), typeof(MenuContentControl), new PropertyMetadata(false));



        public bool IsMenuVisible
        {
            get { return (bool)GetValue(IsMenuVisibleProperty); }
            set
            {
                SetValue(IsMenuVisibleProperty, value);
                MenuColumn.Width = value ? new GridLength(200, GridUnitType.Pixel) : new GridLength(0);
            }
        }


        public object MainSectionContent
        {
            get { return GetValue(MainSectionContentProperty); }
            set { SetValue(MainSectionContentProperty, value); }
        }


        public MenuContentControl()
        {
            InitializeComponent();
            IsMenuVisible = true;  // Set initial visibility state
           
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            IsMenuVisible = !IsMenuVisible;

            if (IsMenuVisible)
            {
                ShowMenu();
            }
            else
            {
                HideMenu();
            }
        }

        private void ShowMenu()
        {
            var animation = new DoubleAnimation
            {
                From = -200,
                To = 0,
                Duration = new Duration(TimeSpan.FromSeconds(0.6))
            };
            var translateTransform = new TranslateTransform();
            MenuControl.RenderTransform = translateTransform;
            translateTransform.BeginAnimation(TranslateTransform.XProperty, animation);

            var opacityAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = new Duration(TimeSpan.FromSeconds(0.6))
            };
            MenuControl.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
        }

        private void HideMenu()
        {
            var animation = new DoubleAnimation
            {
                From = 0,
                To = -200,
                Duration = new Duration(TimeSpan.FromSeconds(0.6))
            };
            var translateTransform = new TranslateTransform();
            MenuControl.RenderTransform = translateTransform;
            translateTransform.BeginAnimation(TranslateTransform.XProperty, animation);

            var opacityAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = new Duration(TimeSpan.FromSeconds(0.6))
            };
            MenuControl.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
        }


    }
}
