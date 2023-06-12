using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace SoloTravelAgent.Converters
{
    public class TicketsToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var converter = new BrushConverter();

            if (value is int ticketsSold)
            {
                return ticketsSold < 5 ? (Brush)converter.ConvertFromString("#B0B9C6") : (Brush)converter.ConvertFromString("#185FAD");
            }
            return (Brush)converter.ConvertFromString("#B0B9C6");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
