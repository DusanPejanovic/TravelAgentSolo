using System;
using System.Globalization;
using System.Windows.Data;

namespace SoloTravelAgent.Converters
{
    public class OptionToIsCheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int selectedOption && parameter is string tagString)
            {
                if (int.TryParse(tagString, out int tag))
                {
                    return selectedOption == tag;
                }
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isChecked && isChecked && parameter is string tagString)
            {
                if (int.TryParse(tagString, out int tag))
                {
                    return tag;
                }
            }

            return Binding.DoNothing;
        }
    }

}
