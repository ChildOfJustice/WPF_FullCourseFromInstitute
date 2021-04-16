﻿using System;
using System.Globalization;
using System.Windows;
using Task_3.Converter.Base;

namespace Task_3.Converter
{
    public class NullToVisibilityConverter: ConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is null) ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}