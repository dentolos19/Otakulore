using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Otakulore.Core
{

    public class ObjectDumper
    {

        private readonly int _depth;
        private readonly Dictionary<object, int> _hashListOfFoundElements;
        private readonly char _indentChar;
        private readonly int _indentSize;
        private readonly StringBuilder _stringBuilder;
        private int _currentIndent;
        private int _currentLine;

        private ObjectDumper(int depth, int indentSize, char indentChar)
        {
            _depth = depth;
            _indentSize = indentSize;
            _indentChar = indentChar;
            _stringBuilder = new StringBuilder();
            _hashListOfFoundElements = new Dictionary<object, int>();
        }

        public static string DumpObject(object element, int depth = 4, int indentSize = 2, char indentChar = ' ')
        {
            var instance = new ObjectDumper(depth, indentSize, indentChar);
            return instance.DumpElement(element, true);
        }

        private bool AlreadyDumped(object value)
        {
            if (value == null)
                return false;
            if (_hashListOfFoundElements.TryGetValue(value, out var lineNumber))
            {
                WriteElement("(reference already dumped - line:{0})", lineNumber);
                return true;
            }
            _hashListOfFoundElements.Add(value, _currentLine);
            return false;
        }

        private string DumpElement(object element, bool isTopOfTree = false)
        {
            if (_currentIndent > _depth)
                return null;
            switch (element)
            {
                case null:
                case string _:
                    WriteElement(FormatValue(element));
                    break;
                case ValueType _:
                {
                    var objectType = element.GetType();
                    var isWritten = false;
                    if (objectType.GetTypeInfo().IsGenericType)
                    {
                        var baseType = objectType.GetGenericTypeDefinition();
                        if (baseType == typeof(KeyValuePair<,>))
                        {
                            isWritten = true;
                            WriteElement("Key:");
                            _currentIndent++;
                            DumpElement(objectType.GetProperty("Key").GetValue(element, null));
                            _currentIndent--;
                            WriteElement("Value:");
                            _currentIndent++;
                            DumpElement(objectType.GetProperty("Value").GetValue(element, null));
                            _currentIndent--;
                        }
                    }
                    if (!isWritten)
                        WriteElement(FormatValue(element));
                    break;
                }
                case IEnumerable enumerableElement:
                {
                    foreach (var item in enumerableElement)
                    {
                        if (item is IEnumerable && !(item is string))
                        {
                            _currentIndent++;
                            DumpElement(item);
                            _currentIndent--;
                        }
                        else
                        {
                            DumpElement(item);
                        }
                    }
                    break;
                }
                default:
                {
                    var objectType = element.GetType();
                    WriteElement("{{{0}(HashCode:{1})}}", objectType.FullName, element.GetHashCode());
                    if (AlreadyDumped(element))
                        return isTopOfTree ? _stringBuilder.ToString() : null;
                    _currentIndent++;
                    var members = objectType.GetMembers(BindingFlags.Public | BindingFlags.Instance);
                    foreach (var memberInfo in members)
                    {
                        var fieldInfo = memberInfo as FieldInfo;
                        var propertyInfo = memberInfo as PropertyInfo;
                        if (fieldInfo == null && (propertyInfo == null || !propertyInfo.CanRead || propertyInfo.GetIndexParameters().Length > 0))
                            continue;
                        var type = fieldInfo != null ? fieldInfo.FieldType : propertyInfo.PropertyType;
                        object value;
                        try
                        {
                            value = fieldInfo != null ? fieldInfo.GetValue(element) : propertyInfo.GetValue(element, null);
                        }
                        catch (Exception e)
                        {
                            WriteElement("{0} failed with:{1}", memberInfo.Name, (e.GetBaseException() ?? e).Message);
                            continue;
                        }

                        if (type.GetTypeInfo().IsValueType || type == typeof(string))
                        {
                            WriteElement("{0}: {1}", memberInfo.Name, FormatValue(value));
                        }
                        else
                        {
                            var isEnumerable = typeof(IEnumerable).IsAssignableFrom(type);
                            WriteElement("{0}: {1}", memberInfo.Name, isEnumerable ? "..." : "{ }");
                            _currentIndent++;
                            DumpElement(value);
                            _currentIndent--;
                        }
                    }
                    _currentIndent--;
                    break;
                }
            }

            return isTopOfTree ? _stringBuilder.ToString() : null;
        }

        private string FormatValue(object @object)
        {
            switch (@object)
            {
                case null:
                    return "null";
                case DateTime time:
                    return time.ToShortDateString();
                case string @string:
                    return "\"" + @string + "\"";
                case char _ when @object.Equals('\0'):
                    return "''";
                case char @char:
                    return "'" + @char + "'";
                case ValueType _:
                    return @object.ToString();
                case IEnumerable _:
                    return "...";
                default:
                    return "{ }";
            }
        }

        private void WriteElement(string value, params object[] args)
        {
            var space = new string(_indentChar, _currentIndent * _indentSize);
            if (args != null)
                value = string.Format(value, args);
            _stringBuilder.AppendLine(space + value);
            _currentLine++;
        }

    }

}