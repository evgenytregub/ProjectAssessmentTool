using System;
using System.Globalization;
using System.Windows.Data;

namespace ProjectAssessmentTool.Converters
{
    public class NumberWithSpaceSeparatorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            if (value is IFormattable number)
            {
                // Пример: 1234567.89 → "1 234 567,89"
                string formatted = number.ToString("#,0.##", CultureInfo.InvariantCulture);
                return formatted.Replace(",", " ").Replace(".", ",");
            }

            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing; // обратное преобразование не нужно для TextBlock
        }
    }
}
