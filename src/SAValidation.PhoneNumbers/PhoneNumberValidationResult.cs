// src/SAValidation.PhoneNumbers/PhoneNumberValidationResult.cs
using System.Text;

namespace SAValidation.PhoneNumbers;

/// <summary>
/// Result of phone number validation with detailed information
/// </summary>
public class PhoneNumberValidationResult
{
    /// <summary>
    /// Creates a successful validation result
    /// </summary>
    public static PhoneNumberValidationResult Success(
        string originalNumber, 
        string cleanedNumber,
        string normalizedNumber,
        PhoneNumberType numberType,
        NetworkOperator networkOperator = NetworkOperator.Unknown,
        string? areaCode = null,
        string? areaDescription = null)
    {
        return new PhoneNumberValidationResult
        {
            IsValid = true,
            OriginalNumber = originalNumber,
            CleanedNumber = cleanedNumber,
            NormalizedNumber = normalizedNumber,
            NumberType = numberType,
            Operator = networkOperator,
            AreaCode = areaCode,
            AreaDescription = areaDescription,
            ErrorMessage = null
        };
    }
    
    /// <summary>
    /// Creates a failed validation result with error message
    /// </summary>
    public static PhoneNumberValidationResult Failure(string originalNumber, string errorMessage)
    {
        return new PhoneNumberValidationResult
        {
            IsValid = false,
            OriginalNumber = originalNumber,
            CleanedNumber = null,
            NormalizedNumber = null,
            NumberType = PhoneNumberType.Unknown,
            Operator = NetworkOperator.Unknown,
            AreaCode = null,
            AreaDescription = null,
            ErrorMessage = errorMessage
        };
    }
    
    /// <summary>Whether the phone number is valid</summary>
    public bool IsValid { get; private set; }
    
    /// <summary>The original input string</summary>
    public string OriginalNumber { get; private set; } = string.Empty;
    
    /// <summary>Number with formatting removed (only digits)</summary>
    public string? CleanedNumber { get; private set; }
    
    /// <summary>Number normalized to international format (+27...)</summary>
    public string? NormalizedNumber { get; private set; }
    
    /// <summary>Type of phone number (Mobile, Landline, etc.)</summary>
    public PhoneNumberType NumberType { get; private set; }
    
    /// <summary>Mobile network operator (for mobile numbers)</summary>
    public NetworkOperator Operator { get; private set; }
    
    /// <summary>Area code (for landline numbers)</summary>
    public string? AreaCode { get; private set; }
    
    /// <summary>Area description (for landline numbers)</summary>
    public string? AreaDescription { get; private set; }
    
    /// <summary>Error message if validation failed</summary>
    public string? ErrorMessage { get; private set; }
    
    /// <summary>
    /// Returns a human-readable summary of the validation result
    /// </summary>
    public override string ToString()
    {
        if (!IsValid)
        {
            return $"Invalid: {ErrorMessage}";
        }
        
        var sb = new StringBuilder();
        sb.Append($"Valid {NumberType}");
        
        if (NumberType == PhoneNumberType.Mobile && Operator != NetworkOperator.Unknown)
        {
            sb.Append($" ({Operator})");
        }
        else if (NumberType == PhoneNumberType.Landline && AreaDescription != null)
        {
            sb.Append($" ({AreaDescription})");
        }
        
        sb.Append($" - {NormalizedNumber}");
        
        return sb.ToString();
    }
}