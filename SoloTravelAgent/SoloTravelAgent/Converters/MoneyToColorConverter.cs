using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace SoloTravelAgent.Converters
{
    public class MoneyToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var converter = new BrushConverter();

            if (value is decimal moneyEarned)
            {
                return moneyEarned < 2000 ? (Brush)converter.ConvertFromString("#B0B9C6") : (Brush)converter.ConvertFromString("#009900");
            }
            return (Brush)converter.ConvertFromString("#C7EA46"); 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
