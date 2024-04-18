using System;
using System.Globalization;
using System.Windows.Data;

namespace PCPDFengine.Converters
{
    public class StringToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value?.ToString() == "True")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool) value)
                {
                    return "True";
                }
                else
                {
                    return "False";
                }
            }
            else
            {
                return "False";
            }
        }
    }
}
