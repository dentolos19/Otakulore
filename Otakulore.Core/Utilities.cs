using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;

namespace Otakulore.Core;

public static class Utilities
{

    public static string? ToEnumString<T>(this T type) where T : Enum
    {
        return typeof(T)
            .GetTypeInfo()
            .DeclaredMembers
            .SingleOrDefault(x => x.Name == type.ToString())?
            .GetCustomAttribute<EnumMemberAttribute>(false)?
            .Value;
    }

    public static Color ParseColorHex(string colorHex)
    {
        if (!colorHex.StartsWith('#'))
            return Color.White;
        colorHex = colorHex.TrimStart('#');
        return colorHex.Length == 6
            ? // parses rgb
            Color.FromArgb(255, // alpha
                int.Parse(colorHex.Substring(0, 2), NumberStyles.HexNumber), // red
                int.Parse(colorHex.Substring(2, 2), NumberStyles.HexNumber), // green
                int.Parse(colorHex.Substring(4, 2), NumberStyles.HexNumber)) // blue
            : // parses argb
            Color.FromArgb(int.Parse(colorHex.Substring(0, 2), NumberStyles.HexNumber), // alpha
                int.Parse(colorHex.Substring(2, 2), NumberStyles.HexNumber), // red
                int.Parse(colorHex.Substring(4, 2), NumberStyles.HexNumber), // green
                int.Parse(colorHex.Substring(6, 2), NumberStyles.HexNumber)); // blue
    }

}