// src/SAValidation.PhoneNumbers/PhoneNumberValidatorExtensions.cs
namespace SAValidation.PhoneNumbers;

/// <summary>
/// Extension methods for easy phone number validation
/// </summary>
public static class PhoneNumberValidatorExtensions
{
    private static readonly PhoneNumberValidator _validator = new();
    
    /// <summary>
    /// Validates if a string is a valid South African phone number
    /// </summary>
    /// <param name="phoneNumber">The phone number to validate</param>
    /// <returns>True if valid, false otherwise</returns>
    /// <example>
    /// <code>
    /// bool isValid = "0825551234".IsValidSouthAfricanPhoneNumber();
    /// </code>
    /// </example>
    public static bool IsValidSouthAfricanPhoneNumber(this string phoneNumber)
    {
        return _validator.IsValid(phoneNumber);
    }
    
    /// <summary>
    /// Gets detailed validation result for a South African phone number
    /// </summary>
    /// <param name="phoneNumber">The phone number to validate</param>
    /// <returns>Detailed validation result with type, operator, area info</returns>
    /// <example>
    /// <code>
    /// var result = "0825551234".ValidateSouthAfricanPhoneNumber();
    /// if (result.IsValid) 
    /// {
    ///     Console.WriteLine($"Type: {result.NumberType}, Operator: {result.Operator}");
    /// }
    /// </code>
    /// </example>
    public static PhoneNumberValidationResult ValidateSouthAfricanPhoneNumber(this string phoneNumber)
    {
        return _validator.Validate(phoneNumber);
    }
}