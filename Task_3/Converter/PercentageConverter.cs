using System;
using System.Globalization;
using Task_3.Converter.Base;

namespace Task_3.Converter
{
    
    public sealed class PercentageConverter : ConverterBase
    {

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is double @double))
            {
                throw new ArgumentException(nameof(value));
            }
            if (!(parameter is double param))
            {
                throw new ArgumentException(nameof(parameter));
            }
            return @double * param;
        }

    }

}