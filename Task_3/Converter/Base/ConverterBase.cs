using System;
using System.Globalization;
using System.Windows.Data;

namespace Task_3.Converter.Base
{

    public abstract class ConverterBase : IValueConverter
    {

        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

}