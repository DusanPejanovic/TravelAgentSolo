using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace SoloTravelAgent.Converters
{
    public class EmailToFirstCharacterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var email = value as string;
            return email?.FirstOrDefault().ToString().ToUpper() ?? string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
