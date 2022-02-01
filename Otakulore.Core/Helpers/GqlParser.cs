namespace Otakulore.Core.Helpers;

public static class GqlParser
{

    public static string Parse(GqlType type, string name, ICollection<GqlSelection> selections, IDictionary<string, object>? parameters = null)
    {
        var query = type.GetEnumValue() + "{" + name;
        if (parameters is { Count: > 0 })
            query += "(" + BuildParameters(parameters) + ")";
        if (selections.Count > 0)
            query += "{" + ParseSelections(selections) + "}";
        query += "}";
        return query;
    }

    public static string ParseSelections(IEnumerable<GqlSelection> selections)
    {
        var data = string.Empty;
        var isFirst = true;
        foreach (var selection in selections)
        {
            data += (isFirst ? string.Empty : ",") + selection.Name;
            if (selection.Parameters is { Count: > 0 })
                data += "(" + BuildParameters(selection.Parameters) + ")";
            if (selection.Selections is { Count: > 0 })
                data += "{" + ParseSelections(selection.Selections) + "}";
            isFirst = false;
        }
        return data;
    }

    public static string BuildParameters(IDictionary<string, object> parameters)
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
                    bool @bool => @bool ? "true" : "false",
                    Enum @enum => @enum.GetEnumValue(),
                    _ => value.ToString()
                };
            }
        }
        return data;
    }

}