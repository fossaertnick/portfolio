using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Mde.Project.Mobile.Converters
{
    class BoolToOccasionConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if(value == null || parameter == null) { return false; }
            return value.ToString() == parameter.ToString();
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isChecked && isChecked)
            {
                // Alleen wanneer een radio geselecteerd wordt
                return parameter?.ToString();
            }

            // ⚠️ SUPER BELANGRIJK
            return Binding.DoNothing;
        }
    }
}
