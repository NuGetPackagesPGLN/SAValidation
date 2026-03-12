// src/SAValidation.Common/Guard.cs
namespace SAValidation.Common;

/// <summary>
/// Defensive programming helpers to validate parameters
/// </summary>
public static class Guard
{
    /// <summary>
    /// Throws ArgumentNullException if string is null or whitespace
    /// </summary>
    public static string AgainstNullOrWhiteSpace(string value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(parameterName, $"{parameterName} cannot be null or whitespace");
        
        return value;
    }
    
    /// <summary>
    /// Throws ArgumentOutOfRangeException if value is out of range
    /// </summary>
    public static int AgainstOutOfRange(int value, int min, int max, string parameterName)
    {
        if (value < min || value > max)
            throw new ArgumentOutOfRangeException(parameterName, $"{parameterName} must be between {min} and {max}");
        
        return value;
    }
}