using System.Globalization;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
namespace SistemaTaxiMobil.Converters
{
    public class BoolToBorderColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isSelected)
                return isSelected ? Color.FromArgb("#FFC107") : Color.FromArgb("#E0E0E0");
            return Color.FromArgb("#E0E0E0");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}