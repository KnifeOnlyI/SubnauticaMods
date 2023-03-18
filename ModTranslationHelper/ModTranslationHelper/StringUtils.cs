using System.Text.RegularExpressions;

namespace Koi.Subnautica.ModTranslationHelper;

/// <summary>
/// An utilitary class to manage strings.
/// </summary>
internal static class StringUtils
{
    /// <summary>
    /// The regex to detect all whitespaces.
    /// </summary>
    private static readonly Regex WhitespacesRegex = new(@"\s+");

    /// <summary>
    /// Normalize the specified string value (Remove all whitespaces and set lower case).
    /// </summary>
    /// <param name="value">The value to normalize</param>
    /// <returns>The normalized value (Empty string if the specified value is NULL)</returns>
    public static string Normalize(string value)
    {
        return value != null
            ? WhitespacesRegex.Replace(value.ToLower(), string.Empty)
            : string.Empty;
    }
}