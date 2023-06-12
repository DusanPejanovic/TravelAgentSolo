using System;
using System.Globalization;
using System.Windows.Data;

namespace SoloTravelAgent.Converters
{
    public class TicketsToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int ticketsSold)
            {
                return ticketsSold < 5 ? "Not Popular" : "Popular";
            }
            return "Not available";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
