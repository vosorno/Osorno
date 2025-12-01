using System.Globalization;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
namespace SistemaTaxiMobil.Converters
{
    public class BoolToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isSelected)
                return isSelected ? Color.FromArgb("#FFF8E1") : Colors.White;
            return Colors.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}