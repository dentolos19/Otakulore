using System;

namespace Otakulore.Core
{

    public static class ExtensionUtilities
    {

        public static string GetStringBetweenStrings(this string @string, string fromString, string toString)
        {
            var fromIndex = @string.IndexOf(fromString, StringComparison.Ordinal) + fromString.Length;
            var toIndex = @string.IndexOf(toString, StringComparison.Ordinal);
            return @string.Substring(fromIndex, toIndex - fromIndex);
        }

    }

}