using SoloTravelAgent.ViewModel.MainScreen;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace SoloTravelAgent.View.Controls
{
    /// <summary>
    /// Interaction logic for MenuContentControl.xaml
    /// </summary>
    public partial class AgentMainScreenView : Window
    {
        public static readonly DependencyProperty MainSectionContentProperty =
        DependencyProperty.Register("MainSectionContent", typeof(object), typeof(AgentMainScreenView), new PropertyMetadata(null));

        public static readonly DependencyProperty IsMenuVisibleProperty =
        DependencyProperty.Register("IsMenuVisible", typeof(bool), typeof(AgentMainScreenView), new PropertyMetadata(false));

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


        public AgentMainScreenView()
        {
            InitializeComponent();
            DataContext = new AgentMainScreenViewModel();
            IsMenuVisible = true;  
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
