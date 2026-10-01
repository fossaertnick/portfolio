using System.Globalization;

namespace Mde.Project.Mobile.Converties
{
    public class LoadingConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return !(bool)value!;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return !(bool)value!;
        }
    }
}
