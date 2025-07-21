using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace ProjectAssessmentTool.Converters
{
    public class ThousandSeparatorConverter : IValueConverter
    {
        // От decimal к строке с пробелами: "1234567.89" → "1 234 567,89"
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal d)
            {
                return d.ToString("#,0.##", CultureInfo.InvariantCulture)
                        .Replace(",", " ")
                        .Replace(".", ",");
            }

            return value?.ToString();
        }

        // Обратное преобразование: "1 234 567,89" → decimal
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var input = value?.ToString()?.Replace(" ", "").Replace(",", ".");
            if (decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal result))
                return result;

            return DependencyProperty.UnsetValue;
        }
    }
}
