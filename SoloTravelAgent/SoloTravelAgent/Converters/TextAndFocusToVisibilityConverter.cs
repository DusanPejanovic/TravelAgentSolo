using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace SoloTravelAgent.Converters
{
    public class TextAndFocusToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is string text && values[1] is bool isFocused)
            {
                return string.IsNullOrEmpty(text) && !isFocused ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Visible;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
