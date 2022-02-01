﻿using System;
using Microsoft.UI.Xaml.Data;

namespace Otakulore.Converters;

public class EnumIntegerConverter : IValueConverter
{

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is Enum @enum)
            return (int)(object)@enum;
        throw new NotImplementedException();
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }

}