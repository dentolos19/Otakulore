﻿using System;
using Microsoft.UI.Xaml.Data;
using Otakulore.Core;

namespace Otakulore.Converters;

public class EnumDescriptionConverter : IValueConverter
{

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is Enum @enum)
            return @enum.GetEnumDescription(true);
        throw new NotImplementedException();
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }

}