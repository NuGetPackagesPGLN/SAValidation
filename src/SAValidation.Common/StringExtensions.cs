using System.Text.RegularExpressions;

namespace SAValidation.Common;

/// <summary>
/// Extension methods for string manipulation used across all validators
/// </summary>
public static class StringExtensions
{
    // Compiled regex for better performance (cached)
    private static readonly Regex _cleaningRegex = new(
        @"[\s\-\(\)\.]",  // Matches spaces, dashes, parentheses, dots
        RegexOptions.Compiled);
    
    private static readonly Regex _digitsOnlyRegex = new(
        @"[^\d]",  // Matches anything that's NOT a digit
        RegexOptions.Compiled);
    
    /// <summary>
    /// Removes common formatting characters: spaces, dashes, parentheses, dots
    /// </summary>
    /// <example>
    /// "082 555 1234" → "0825551234"
    /// "082-555-1234" → "0825551234"
    /// </example>
    public static string CleanNumber(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;
        
        return _cleaningRegex.Replace(input, "");
    }
    
    /// <summary>
    /// Extracts only digits from a string, removing everything else
    /// </summary>
    /// <example>
    /// "082 555-1234" → "0825551234"
    /// "+27 (82) 555-1234" → "27825551234"
    /// </example>
    public static string ExtractDigits(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;
        
        return _digitsOnlyRegex.Replace(input, "");
    }
    
    /// <summary>
    /// Checks if string contains only digits
    /// </summary>
    public static bool IsAllDigits(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return false;
        
        return input.All(char.IsDigit);
    }
    
    /// <summary>
    /// Checks if string starts with any of the provided prefixes
    /// </summary>
    public static bool StartsWithAny(this string input, params string[] prefixes)
    {
        if (string.IsNullOrEmpty(input))
            return false;
        
        return prefixes.Any(prefix => input.StartsWith(prefix));
    }
    
    /// <summary>
    /// Safely gets substring with bounds checking
    /// </summary>
    public static string SafeSubstring(this string input, int startIndex, int length)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;
        
        if (startIndex >= input.Length)
            return string.Empty;
        
        // Calculate how many characters we can actually take
        int availableLength = input.Length - startIndex;
        int takeLength = Math.Min(length, availableLength);
        
        return input.Substring(startIndex, takeLength);
    }
}