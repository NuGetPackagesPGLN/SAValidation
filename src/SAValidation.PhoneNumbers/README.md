# SAValidation.PhoneNumbers

South African phone number validation library.

NuGet: https://www.nuget.org/packages/SAValidation.PhoneNumbers/  
GitHub: https://github.com/NuGetPackagesPGLN/SAValidation

## Installation

  ash
dotnet add package SAValidation.PhoneNumbers
  

## Features

### Mobile Numbers
- Vodacom (082, 072)
- MTN (083, 076)
- Cell C (084, 079)
- Telkom Mobile (081)
- Neotel (085)

### Landline Numbers with Area Detection
- Johannesburg (011)
- Pretoria (012)
- Cape Town (021)
- Durban (031)
- Port Elizabeth (041)
- Bloemfontein (051)
- And 30+ other areas

### Special Numbers
- Toll-Free: 0800 numbers
- Premium Rate: 0860, 0861 numbers
- Emergency: 10111, 112, 107, 10177, 1022

### Format Handling
- Local format: 0825551234
- International format: +27825551234
- With spaces: 082 555 1234
- With dashes: 082-555-1234
- With parentheses: (082) 555-1234

## Quick Usage

  csharp
using SAValidation.PhoneNumbers;

// Simple validation
bool isValid = "0825551234".IsValidSouthAfricanPhoneNumber();

// Detailed validation
var result = "0825551234".ValidateSouthAfricanPhoneNumber();

if (result.IsValid)
{
    Console.WriteLine($"Type: {result.NumberType}");        // Mobile
    Console.WriteLine($"Operator: {result.Operator}");      // Vodacom
    Console.WriteLine($"Normalized: {result.NormalizedNumber}"); // +27825551234
}
  

## Detailed Examples

### Mobile Number Detection

  csharp
// Vodacom
var vodacom = "0825551234".ValidateSouthAfricanPhoneNumber();
Console.WriteLine(vodacom.Operator); // Vodacom

// MTN
var mtn = "0835551234".ValidateSouthAfricanPhoneNumber();
Console.WriteLine(mtn.Operator); // MTN

// Cell C
var cellc = "0845551234".ValidateSouthAfricanPhoneNumber();
Console.WriteLine(cellc.Operator); // CellC
  

### Landline Area Detection

  csharp
var jhb = "0115551234".ValidateSouthAfricanPhoneNumber();
Console.WriteLine(jhb.AreaDescription); // Johannesburg & Gauteng

var cpt = "0215551234".ValidateSouthAfricanPhoneNumber();
Console.WriteLine(cpt.AreaDescription); // Cape Town & Winelands

var durban = "0315551234".ValidateSouthAfricanPhoneNumber();
Console.WriteLine(durban.AreaDescription); // Durban & eThekwini
  

### Different Input Formats

  csharp
// All these return the same normalized result: +27825551234
var formats = new[]
{
    "0825551234",
    "082 555 1234",
    "082-555-1234",
    "+27 82 555 1234",
    "27825551234"
};

foreach (var format in formats)
{
    var result = format.ValidateSouthAfricanPhoneNumber();
    Console.WriteLine($"{format} -> {result.NormalizedNumber}");
}
  

### Special Numbers

  csharp
// Toll-Free
var tollFree = "0800123456".ValidateSouthAfricanPhoneNumber();
Console.WriteLine(tollFree.NumberType); // TollFree

// Premium
var premium = "08615551234".ValidateSouthAfricanPhoneNumber();
Console.WriteLine(premium.NumberType); // Premium

// Emergency
var emergency = "10111".ValidateSouthAfricanPhoneNumber();
Console.WriteLine(emergency.NumberType); // Emergency
  

## API Reference

### PhoneNumberValidator Class

| Method | Description |
|--------|-------------|
| Validate(string phoneNumber) | Returns detailed validation result |
| IsValid(string phoneNumber) | Quick true/false validation |

### Extension Methods

| Method | Description |
|--------|-------------|
| IsValidSouthAfricanPhoneNumber(this string) | Quick validation |
| ValidateSouthAfricanPhoneNumber(this string) | Detailed validation |

### PhoneNumberValidationResult Properties

| Property | Description | Example |
|----------|-------------|---------|
| IsValid | Whether the number is valid | true / false |
| NumberType | Type of number | Mobile, Landline, TollFree, Premium, Emergency |
| Operator | Mobile network operator | Vodacom, MTN, CellC, Telkom, Neotel |
| AreaCode | Geographic area code | 011, 021, 031 |
| AreaDescription | Description of the area | "Johannesburg & Gauteng" |
| NormalizedNumber | International format | +27825551234 |
| CleanedNumber | Digits only | 0825551234 |
| OriginalNumber | Original input | As provided |
| ErrorMessage | Error details if invalid | "Invalid length" |

## Error Handling

  csharp
var invalid = "123".ValidateSouthAfricanPhoneNumber();
if (!invalid.IsValid)
{
    Console.WriteLine(invalid.ErrorMessage); // "Invalid length: SA numbers should be 9-11 digits (got 3)"
}
  

## Contributing

Contributions are welcome! Please submit pull requests to the GitHub repository.

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Open a Pull Request

## License

MIT
