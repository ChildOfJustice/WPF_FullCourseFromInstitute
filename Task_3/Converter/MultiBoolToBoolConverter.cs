using System;
using System.Globalization;
using Task_3.Converter.Base;

namespace Task_3.Converter
{
    public class MultiBoolToBoolConverter : MultiConverterBase
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(values[0] is bool))
            {
                throw new ArgumentException(string.Empty, nameof(values));
            }

            foreach (var @bool  in values)
            {
                if (@bool as bool? != true)
                    return false;
            }

            return true;
        }
    }
}