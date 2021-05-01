using System;
using System.Globalization;
using System.Windows;
using Task_3.Converter.Base;

namespace Task_3.Converter
{
    public class NullToVisibilityConverter: ConverterBase
    {
        enum Parameter
        {
            NullVisible,
            NullCollapsed
        }
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(parameter is Parameter param))
            {
                throw new ArgumentException(string.Empty, nameof(param));
            }
            if(param == Parameter.NullCollapsed)
                return (value is null) ? Visibility.Collapsed : Visibility.Visible;
            return (value is null) ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}