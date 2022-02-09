namespace Otakulore.Core.Helpers;

public static class GqlParser
{

    public static string Parse(GqlType type, IEnumerable<GqlSelection> selections)
    {
        return type.GetEnumValue() + "{" + BuildSelections(selections) + "}";
    }

    public static string Parse(GqlType type, string name, ICollection<GqlSelection> selections, IDictionary<string, object>? parameters = null)
    {
        var selection = new GqlSelection(name, selections);
        if (parameters is not { Count: > 0 })
            return Parse(type, new[] { selection });
        foreach (var entry in parameters)
            selection.Parameters.Add(entry);
        return Parse(type, new[] { selection });
    }

    private static string BuildSelections(IEnumerable<GqlSelection> selections)
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
        {
            data += (data.Length > 1 ? "," : string.Empty) + name + ":";
            if (value is string @string)
            {
                if (@string.StartsWith("$"))
                    data += @string[1..];
                else
                    data += "\"" + @string + "\"";
            }
            else
            {
                data += value switch
                {
                    null => "null",
                    bool @bool => @bool ? "true" : "false",
                    Enum @enum => @enum.GetEnumValue(),
                    _ => value.ToString()
                };
            }
        }
        return data;
    }

}