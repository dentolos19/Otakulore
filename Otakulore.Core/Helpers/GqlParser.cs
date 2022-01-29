using System.Collections;
using System.Dynamic;

namespace Otakulore.Core.Helpers;

public static class GqlParser
{

    public static string Parse(GqlType type, string name, GqlSelection[] selections, object? @object = null, string? objectTypeName = null)
    {
        var query = type.GetEnumValue() + "{" + name;
        if (@object != null)
        {
            query += "(";
            var queryData = string.Empty;
            if (@object is not string && @object is IEnumerable enumerableObject)
            {
                queryData += objectTypeName + ":[";
                var isFirst = true;
                foreach (var item in enumerableObject)
                {
                    queryData += (isFirst ? "" : ",") + "{" + BuildQueryData(item) + "}";
                    isFirst = false;
                }
                queryData += "]";
            }
            else if (@object is string)
            {
                queryData += objectTypeName + ":\"" + @object + "\"";
            }
            else
            {
                if (objectTypeName != null)
                    queryData += objectTypeName + ":" + "{";
                queryData += BuildQueryData(@object);
                if (objectTypeName != null)
                    queryData += "}";
            }
            query += queryData + ")";
        }
        if (selections.Length > 0)
            query += "{" + BuildSubData(selections) + "}";
        query += "}";
        return query;
    }

    private static string BuildSubData(IEnumerable<GqlSelection> selections)
    {
        var data = string.Empty;
        var isFirst = true;
        foreach (var selection in selections)
        {
            data += (isFirst ? string.Empty : ",") + selection.Name;
            if (selection.Parameters is { Count: > 0 })
            {
                var parameters = "(";
                foreach (var (name, value) in selection.Parameters)
                {
                    parameters += (parameters.Length > 1 ? "," : string.Empty) + name + ":";
                    if (value is string @string)
                    {
                        if (@string.StartsWith("$"))
                        {
                            parameters += @string[1..];
                        }
                        else
                        {
                            parameters += "\"" + @string + "\"";
                        }
                    }
                    else
                    {
                        parameters += value switch
                        {
                            bool @bool => @bool ? "true" : "false",
                            Enum @enum => @enum.GetEnumValue(),
                            _ => value.ToString()
                        };
                    }
                }
                data += parameters + ")";
            }
            if (selection.Selections is { Length: > 0 })
                data += "{" + BuildSubData(selection.Selections) + "}";
            isFirst = false;
        }
        return data;
    }

    private static string BuildQueryData(object @object)
    {
        var queryData = string.Empty;
        foreach (var propertyInfo in @object.GetType().GetProperties())
        {
            var value = propertyInfo.GetValue(@object);
            if (value == null)
                continue;
            var type = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
            if (type.IsArray)
            {
                var values = value as ICollection;
                if (values.Count <= 0)
                    continue;
                queryData += "," + char.ToLowerInvariant(propertyInfo.Name[0]) +
                    propertyInfo.Name[1..] + ":[";
                var queryPart = string.Empty;
                foreach (var v in values)
                {
                    queryPart += queryPart.Length > 0 ? "," : string.Empty;
                    switch (v)
                    {
                        case string:
                            queryPart += "\"" + v + "\"";
                            break;
                        case int:
                        case Enum:
                            queryPart += v;
                            break;
                        default:
                        {
                            queryPart += "{";
                            var isFirst = true;
                            foreach (var vPropertyInfo in v.GetType().GetProperties())
                            {
                                var vValue = vPropertyInfo.GetValue(v);
                                if (vValue == null)
                                    continue;
                                queryPart +=
                                    (isFirst ? "" : ",") + char.ToLowerInvariant(vPropertyInfo.Name[0]) +
                                    vPropertyInfo.Name[1..] + ":" + (vValue is string ? "\"" + vValue + "\"" : vValue);
                                isFirst = false;
                            }
                            queryPart += "},";
                            break;
                        }
                    }
                }
                queryPart += "]";
                queryData += queryData.Length > 0 ? "," + queryPart : queryPart;
            }
            else if (type == typeof(ExpandoObject))
            {
                var queryPart = char.ToLowerInvariant(propertyInfo.Name[0]) + propertyInfo.Name.Substring(1) + ":";
                var jsonString = "{";
                var data = (IDictionary<string, object>)value;
                foreach (var entry in data)
                {
                    if (jsonString.Length > 1)
                        jsonString += ",";
                    var valueQuotes = entry.Value is string ? "\"" : string.Empty;
                    var propertyValue = entry.Value is bool ? entry.Value.ToString().ToLower() : entry.Value;
                    jsonString += char.ToLowerInvariant(entry.Key[0]) + entry.Key.Substring(1) + ":" + valueQuotes + propertyValue + valueQuotes;
                }
                jsonString += "}";
                queryData += queryData.Length > 0 ? "," + queryPart + jsonString : queryPart + jsonString;
            }
            else
            {
                var valueQuotes = type == typeof(string) ? "\"" : string.Empty;
                var propertyValue = type == typeof(bool) ? value.ToString().ToLower() : value;
                var queryPart = char.ToLowerInvariant(propertyInfo.Name[0]) +
                    propertyInfo.Name[1..] + ":" + valueQuotes + propertyValue + valueQuotes;
                queryData += queryData.Length > 0 ? "," + queryPart : queryPart;
            }
        }
        return queryData;
    }

}