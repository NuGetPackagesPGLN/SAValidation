// src/SAValidation.PhoneNumbers/PhoneNumberType.cs
namespace SAValidation.PhoneNumbers;

/// <summary>
/// Types of South African phone numbers
/// </summary>
public enum PhoneNumberType
{
    /// <summary>Unknown or invalid number type</summary>
    Unknown = 0,
    
    /// <summary>Mobile/cellular number (e.g., 082, 083, 084, etc.)</summary>
    Mobile = 1,
    
    /// <summary>Landline/geographic number (e.g., 011, 021, 031, etc.)</summary>
    Landline = 2,
    
    /// <summary>Toll-free number (0800)</summary>
    TollFree = 3,
    
    /// <summary>Premium rate number (0860, 0861, etc.)</summary>
    Premium = 4,
    
    /// <summary>Emergency services (10111, 112, 107)</summary>
    Emergency = 5
}