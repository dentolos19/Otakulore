using System.Collections;
using System.Runtime.Serialization;

namespace Otakulore.Core.Helpers;

public static class GqlParser
{

    public static string Parse(GqlType type, IList<GqlSelection> selections)
    {
        return GetEnumValue(type) + "{" + BuildSelections(selections) + "}";
    }

    public static string Parse(GqlType type, string name, IList<GqlSelection>? selections, IDictionary<string, object?>? parameters = null)
    {
        var selection = new GqlSelection(name, selections);
        if (parameters is not { Count: > 0 })
            return Parse(type, new[] { selection });
        foreach (var entry in parameters)
            selection.Parameters.Add(entry);
        return Parse(type, new[] { selection });
    }

    private static string BuildSelections(IList<GqlSelection> selections)
    {
        var data = string.Empty;
        var isFirst = true;
        foreach (var selection in selections)
        {
            data += (isFirst ? string.Empty : ",") + selection.Name;
            if (selection.Parameters is { Count: > 0 })
                data += "(" + BuildParameters(selection.Parameters) + ")";
            if (selection.Selections is { Count: > 0 })
                data += "{" + BuildSelections(selection.Selections) + "}";
            isFirst = false;
        }
        return data;
    }

    private static string BuildParameters(IDictionary<string, object?> parameters)
    {
        var data = string.Empty;
        foreach (var (name, value) in parameters)
            data += (data.Length > 1 ? "," : string.Empty) + name + ":" + ParseObjectString(value);
        return data;
    }

    private static string ParseObjectString(object? value)
    {
        return value switch
        {
            null => "null",
            string @string => @string.StartsWith('$') ? @string.TrimStart('$') : $"\"{@string}\"",
            bool @bool => @bool ? "true" : "false",
            Enum @enum => GetEnumValue(@enum),
            IEnumerable enumerable => ParseEnumerableString(enumerable),
            _ => value.ToString()
        };
    }

    private static string ParseEnumerableString(IEnumerable value)
    {
        var data = "[";
        var isFirst = true;
        foreach (var item in value)
        {
            data += (isFirst ? string.Empty : ",") + ParseObjectString(item);
            isFirst = false;
        }
        return data + "]";
    }

    private static string GetEnumValue(Enum @enum)
    {
        var field = @enum.GetType().GetField(@enum.ToString());
        var attributes = (EnumMemberAttribute[])field.GetCustomAttributes(typeof(EnumMemberAttribute), false);
        return attributes.Length > 0 ? attributes.First().Value : @enum.ToString();
    }

}