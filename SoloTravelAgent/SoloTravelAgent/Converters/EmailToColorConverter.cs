using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System;

namespace SoloTravelAgent.Converters
{
    public class EmailToColorConverter : IValueConverter
    {
        private static readonly Dictionary<char, Brush> CharacterColors = new Dictionary<char, Brush>()
        {
            { 'J', (Brush)new BrushConverter().ConvertFromString("#1098AD") },
            { 'M', (Brush)new BrushConverter().ConvertFromString("#1098AD") },
            { 'O', (Brush)new BrushConverter().ConvertFromString("#1098AD") },
            { 'R', (Brush)new BrushConverter().ConvertFromString("#1E88E5") },
            { 'C', (Brush)new BrushConverter().ConvertFromString("#1E88E5") },
            { 'D', (Brush)new BrushConverter().ConvertFromString("#FF8F00") },
            { 'K', (Brush)new BrushConverter().ConvertFromString("#FF8F00") },
            { 'G', (Brush)new BrushConverter().ConvertFromString("#FF5252") },
            { 'N', (Brush)new BrushConverter().ConvertFromString("#FF5252") },
            { 'L', (Brush)new BrushConverter().ConvertFromString("#0CA678") },
            { 'B', (Brush)new BrushConverter().ConvertFromString("#6741D9") },
            { 'I', (Brush)new BrushConverter().ConvertFromString("#6741D9") },
            { 'S', (Brush)new BrushConverter().ConvertFromString("#FF6D00") },
            { 'A', (Brush)new BrushConverter().ConvertFromString("#FF5252") },
            { 'P', (Brush)new BrushConverter().ConvertFromString("#FF5252") },
            { 'F', (Brush)new BrushConverter().ConvertFromString("#1E88E5") },
            { 'H', (Brush)new BrushConverter().ConvertFromString("#1E88E5") },
            { 'E', (Brush)new BrushConverter().ConvertFromString("#1E88E5") },
            { 'Q', (Brush)new BrushConverter().ConvertFromString("#1098AD") },
            { 'T', (Brush)new BrushConverter().ConvertFromString("#FF8F00") }, 
            { 'U', (Brush)new BrushConverter().ConvertFromString("#FF5252") }, 
            { 'V', (Brush)new BrushConverter().ConvertFromString("#0CA678") }, 
            { 'W', (Brush)new BrushConverter().ConvertFromString("#6741D9") }, 
            { 'X', (Brush)new BrushConverter().ConvertFromString("#FF6D00") },
            { 'Y', (Brush)new BrushConverter().ConvertFromString("#FF5252") }, 
            { 'Z', (Brush)new BrushConverter().ConvertFromString("#1E88E5") }, 
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var email = value as string;
            if (email == null || email.Length == 0)
            {
                return Brushes.Transparent; // default color if email is empty
            }

            var firstChar = char.ToUpper(email[0]);
            if (CharacterColors.TryGetValue(firstChar, out var color))
            {
                return color;
            }

            return Brushes.Transparent; // default color if character not found in the dictionary
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


}
