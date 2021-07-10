using System;
using System.Globalization;
using Task_3.Converter.Base;

namespace Task_3.Converter
{
    public class MultiBoolToBoolConverter : MultiConverterBase
    {
        
        enum Parameter
        {
            OneTrueAllTrue,
            OneFalseAllTrue,
            OneTrueAllFalse,
            OneFalseAllFalse
        }
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(values[0] is bool))
            {
                throw new ArgumentException(string.Empty, nameof(values));
            }
            if (!(parameter is Parameter param))
            {
                throw new ArgumentException(string.Empty, nameof(param));
            }

            switch (param)
            { 
                case Parameter.OneFalseAllFalse:
                    foreach (var @bool  in values)
                    {
                        if (@bool as bool? != true)
                            return false;
                    }
                    break;
                case Parameter.OneFalseAllTrue:
                    foreach (var @bool  in values)
                    {
                        if (@bool as bool? == false)
                            return true;
                    }
                    break;
                case Parameter.OneTrueAllTrue:
                    foreach (var @bool  in values)
                    {
                        if (@bool as bool? == true)
                            return true;
                    }
                    break;
                case Parameter.OneTrueAllFalse:
                    foreach (var @bool  in values)
                    {
                        if (@bool as bool? == true)
                            return false;
                    }
                    break;
            }
            
            return true;
        }
    }
}