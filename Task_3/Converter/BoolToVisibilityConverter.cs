using System;
using System.Globalization;
using System.Windows;

using Task_3.Converter.Base;

namespace Task_3.Converter
{

    public sealed class BoolToVisibilityConverter : ConverterBase
    {

        enum Parameter
        {
            TrueVisible,
            FalseVisible
        }
        
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //parameter as enum
            
            if (!(value is bool @bool))
            {
                throw new ArgumentException(string.Empty, nameof(value));
            }
            if (!(parameter is Parameter param))
            {
                throw new ArgumentException(string.Empty, nameof(param));
            }
            if(param == Parameter.TrueVisible)
                return @bool ? Visibility.Visible : Visibility.Collapsed;
            return @bool ? Visibility.Collapsed : Visibility.Visible;
        }

    }

}