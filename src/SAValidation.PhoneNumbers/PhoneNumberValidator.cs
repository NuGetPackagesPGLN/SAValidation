using SAValidation.Common;

namespace SAValidation.PhoneNumbers;

/// <summary>
/// Validator for South African phone numbers
/// </summary>
public class PhoneNumberValidator
{
    // Mobile network prefix mappings
    private static readonly Dictionary<string, NetworkOperator> _mobilePrefixes = new()
    {
        ["082"] = NetworkOperator.Vodacom,
        ["072"] = NetworkOperator.Vodacom,
        ["083"] = NetworkOperator.MTN,
        ["076"] = NetworkOperator.MTN,
        ["084"] = NetworkOperator.CellC,
        ["079"] = NetworkOperator.CellC,
        ["081"] = NetworkOperator.Telkom,
        ["085"] = NetworkOperator.Neotel
    };
    
    // Special number prefixes
    private static readonly HashSet<string> _tollFreePrefixes = new() { "0800" };
    private static readonly HashSet<string> _premiumPrefixes = new() { "0860", "0861", "0862", "0863", "0864", "0865", "0866", "0867", "0868", "0869" };
    private static readonly HashSet<string> _emergencyNumbers = new() { "10111", "112", "107", "10177", "1022" };
    
    // Valid starting patterns
    private static readonly string[] _validStartPatterns = new[] { "0", "27", "+27" };
    
    /// <summary>
    /// Validates a South African phone number and returns detailed result
    /// </summary>
    /// <param name="phoneNumber">The phone number to validate</param>
    /// <returns>Detailed validation result</returns>
    public PhoneNumberValidationResult Validate(string phoneNumber)
    {
        // Check for null or empty
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            return PhoneNumberValidationResult.Failure(phoneNumber ?? string.Empty, 
                "Phone number cannot be empty");
        }
        
        // Clean the number (remove formatting)
        var cleaned = phoneNumber.ExtractDigits();
        
        // Check if we have any digits
        if (string.IsNullOrEmpty(cleaned))
        {
            return PhoneNumberValidationResult.Failure(phoneNumber, 
                "Phone number must contain digits");
        }
        
        // Check for emergency numbers first (they have special handling)
        if (_emergencyNumbers.Contains(cleaned))
        {
            return PhoneNumberValidationResult.Success(
                phoneNumber,
                cleaned,
                cleaned, // Emergency numbers aren't normalized
                PhoneNumberType.Emergency
            );
        }
        
        // Normalize to international format for non-emergency numbers
        var normalized = NormalizeToInternational(cleaned);
        if (normalized == null)
        {
            return PhoneNumberValidationResult.Failure(phoneNumber,
                "Invalid format. SA numbers must start with 0, +27, or 27");
        }
        
        // Get the local format (starting with 0)
        var localFormat = ConvertToLocalFormat(cleaned);
        if (localFormat == null)
        {
            return PhoneNumberValidationResult.Failure(phoneNumber,
                "Invalid South African number format");
        }
        
        // Validate length (SA numbers are typically 9-10 digits after cleaning)
        if (localFormat.Length < 9 || localFormat.Length > 11)
        {
            return PhoneNumberValidationResult.Failure(phoneNumber,
                $"Invalid length: SA numbers should be 9-11 digits (got {localFormat.Length})");
        }
        
        // Determine number type
        var (numberType, networkOperator, areaCode) = DetermineNumberType(localFormat);
        
        if (numberType == PhoneNumberType.Unknown)
        {
            return PhoneNumberValidationResult.Failure(phoneNumber,
                "Unknown number type or invalid prefix");
        }
        
        // Get area description for landlines
        string? areaDescription = null;
        if (numberType == PhoneNumberType.Landline && areaCode != null)
        {
            areaDescription = AreaCodeDatabase.GetDescription(areaCode);
        }
        
        // FIX: For landline area codes, remove the leading '0' to match test expectations
        string? displayAreaCode = areaCode;
        if (numberType == PhoneNumberType.Landline && areaCode != null && areaCode.StartsWith("0"))
        {
            displayAreaCode = areaCode.Substring(1); // Remove leading '0' (e.g., "051" -> "51")
        }
        
        // Return successful result
        return PhoneNumberValidationResult.Success(
            phoneNumber,
            cleaned,
            normalized,
            numberType,
            networkOperator,
            displayAreaCode, // Use the display version without leading 0
            areaDescription
        );
    }
    
    /// <summary>
    /// Quick validation that returns only true/false
    /// </summary>
    public bool IsValid(string phoneNumber)
    {
        return Validate(phoneNumber).IsValid;
    }
    
    private string? NormalizeToInternational(string cleaned)
    {
        // If already starts with 27, add + prefix
        if (cleaned.StartsWith("27") && cleaned.Length >= 11)
        {
            return "+" + cleaned;
        }
        
        // If starts with 0, replace with +27
        if (cleaned.StartsWith("0") && cleaned.Length >= 10)
        {
            return "+27" + cleaned.Substring(1);
        }
        
        // Check if it starts with +27 (with the + symbol)
        if (cleaned.StartsWith("+27"))
        {
            return cleaned;
        }
        
        // Check if it starts with valid pattern
        if (!cleaned.StartsWithAny(_validStartPatterns))
        {
            return null;
        }
        
        return null;
    }
    
    private string? ConvertToLocalFormat(string cleaned)
    {
        // If already starts with 0 and correct length, return as is
        if (cleaned.StartsWith("0") && cleaned.Length >= 10)
        {
            return cleaned;
        }
        
        // If starts with 27, convert to 0 format
        if (cleaned.StartsWith("27") && cleaned.Length >= 11)
        {
            return "0" + cleaned.Substring(2);
        }
        
        // If starts with +27, remove + and convert
        if (cleaned.StartsWith("+27"))
        {
            return "0" + cleaned.Substring(3);
        }
        
        return null;
    }
    
    private (PhoneNumberType type, NetworkOperator networkOperator, string? areaCode) DetermineNumberType(string localFormat)
    {
        // Check emergency numbers
        if (_emergencyNumbers.Contains(localFormat))
        {
            return (PhoneNumberType.Emergency, NetworkOperator.Unknown, null);
        }
        
        // Check mobile prefixes (3 digits) - for 10-digit numbers
        if (localFormat.Length >= 10)
        {
            var mobilePrefix = localFormat.Substring(0, 3);
            if (_mobilePrefixes.TryGetValue(mobilePrefix, out var networkOperator))
            {
                return (PhoneNumberType.Mobile, networkOperator, null);
            }
        }
        
        // Check toll-free (4 digits) - for 10-11 digit numbers
        if (localFormat.Length >= 10)
        {
            var tollFreePrefix = localFormat.Substring(0, 4);
            if (_tollFreePrefixes.Contains(tollFreePrefix))
            {
                return (PhoneNumberType.TollFree, NetworkOperator.Unknown, null);
            }
        }
        
        // Check premium (4 digits) - for 10-11 digit numbers
        if (localFormat.Length >= 10)
        {
            var premiumPrefix = localFormat.Substring(0, 4);
            if (_premiumPrefixes.Contains(premiumPrefix))
            {
                return (PhoneNumberType.Premium, NetworkOperator.Unknown, null);
            }
        }
        
        // Check landline (2-digit area codes, 10-digit total)
        if (localFormat.Length == 10 && localFormat.StartsWith("0"))
        {
            // Try 3-digit area code first (including the leading 0)
            var areaCode3 = localFormat.Substring(0, 3);
            if (AreaCodeDatabase.IsValidAreaCode(areaCode3))
            {
                return (PhoneNumberType.Landline, NetworkOperator.Unknown, areaCode3);
            }
            
            // Try 2-digit area code (add leading 0 for lookup)
            var areaCode2 = "0" + localFormat.Substring(1, 2);
            if (AreaCodeDatabase.IsValidAreaCode(areaCode2))
            {
                return (PhoneNumberType.Landline, NetworkOperator.Unknown, areaCode2);
            }
        }
        
        return (PhoneNumberType.Unknown, NetworkOperator.Unknown, null);
    }
}