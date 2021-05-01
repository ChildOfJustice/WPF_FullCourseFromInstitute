using System;
using System.Globalization;
using System.Windows;

using Task_3.Converter.Base;

namespace Task_3.Converter
{

    public sealed class NullToBoolConverter : ConverterBase
    {
        enum Parameter
        {
            NullTrue,
            NullFalse
        }
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(parameter is Parameter param))
            {
                throw new ArgumentException(string.Empty, nameof(param));
            }
            if(param == Parameter.NullFalse)
                return !(value is null);
            return value is null;
        }
    }

}