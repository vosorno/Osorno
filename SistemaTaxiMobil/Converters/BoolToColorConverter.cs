using System.Globalization;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
namespace SistemaTaxiMobil.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isSelected && parameter is string colorType)
            {
                if (colorType == "Primary")
                    return isSelected ? Colors.Orange : Colors.Gray;
                else if (colorType == "Secondary")
                    return !isSelected ? Colors.Orange : Colors.Gray;
            }
            return Colors.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}