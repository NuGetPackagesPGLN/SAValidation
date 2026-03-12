// src/SAValidation.PhoneNumbers/NetworkOperator.cs
namespace SAValidation.PhoneNumbers;

/// <summary>
/// Mobile network operators in South Africa
/// </summary>
public enum NetworkOperator
{
    /// <summary>Unknown operator</summary>
    Unknown = 0,
    
    /// <summary>Vodacom (prefixes: 082, 072)</summary>
    Vodacom = 1,
    
    /// <summary>MTN (prefixes: 083, 076)</summary>
    MTN = 2,
    
    /// <summary>Cell C (prefixes: 084, 079)</summary>
    CellC = 3,
    
    /// <summary>Telkom Mobile/8ta (prefix: 081)</summary>
    Telkom = 4,
    
    /// <summary>Neotel (prefix: 085)</summary>
    Neotel = 5
}