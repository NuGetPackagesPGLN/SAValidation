
# SAValidation

South African validation library for phone numbers, ID numbers, VAT, and more.

[![NuGet](https://img.shields.io/nuget/v/SAValidation.PhoneNumbers)](https://www.nuget.org/packages/SAValidation.PhoneNumbers/)
[![Downloads](https://img.shields.io/nuget/dt/SAValidation.PhoneNumbers)](https://www.nuget.org/packages/SAValidation.PhoneNumbers/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![CI](https://github.com/NuGetPackagesPGLN/SAValidation/actions/workflows/ci.yml/badge.svg)](https://github.com/NuGetPackagesPGLN/SAValidation/actions/workflows/ci.yml)

---

## 📦 Packages

| Package | Version | Downloads |
|---------|---------|-----------|
| **SAValidation.PhoneNumbers** | [![NuGet](https://img.shields.io/nuget/v/SAValidation.PhoneNumbers)](https://www.nuget.org/packages/SAValidation.PhoneNumbers/) | [![Downloads](https://img.shields.io/nuget/dt/SAValidation.PhoneNumbers)](https://www.nuget.org/packages/SAValidation.PhoneNumbers/) |
| **SAValidation.Common** | [![NuGet](https://img.shields.io/nuget/v/SAValidation.Common)](https://www.nuget.org/packages/SAValidation.Common/) | [![Downloads](https://img.shields.io/nuget/dt/SAValidation.Common)](https://www.nuget.org/packages/SAValidation.Common/) |

---

## 🚀 Quick Start

### Install

```bash
dotnet add package SAValidation.PhoneNumbers
```

### Use

```csharp
using SAValidation.PhoneNumbers;

// Simple validation
bool isValid = "0825551234".IsValidSouthAfricanPhoneNumber();

// Detailed validation
var result = "0825551234".ValidateSouthAfricanPhoneNumber();
Console.WriteLine($"Type: {result.NumberType}");     // Mobile
Console.WriteLine($"Operator: {result.Operator}");   // Vodacom
Console.WriteLine($"Normalized: {result.NormalizedNumber}"); // +27825551234
```

---

## 📱 Phone Number Features

| Type | Prefixes | Detection |
|------|----------|-----------|
| **Mobile** | 082,072 (Vodacom), 083,076 (MTN), 084,079 (Cell C), 081 (Telkom), 085 (Neotel) | Operator + type |
| **Landline** | 011 (JHB), 021 (CPT), 031 (DBN), 012 (PTA), 041 (PE), 051 (BFN) | Area + description |
| **Toll-Free** | 0800 | Type only |
| **Premium** | 0860, 0861 | Type only |
| **Emergency** | 10111, 112, 107 | Type only |

### Format Support

```csharp
// All these work the same
"0825551234".IsValidSouthAfricanPhoneNumber();      // true
"082 555 1234".IsValidSouthAfricanPhoneNumber();    // true  
"082-555-1234".IsValidSouthAfricanPhoneNumber();    // true
"+27 82 555 1234".IsValidSouthAfricanPhoneNumber(); // true
"27825551234".IsValidSouthAfricanPhoneNumber();     // true
```

---

## 📋 Examples

### Mobile Operator Detection

```csharp
var numbers = new[] { "0825551234", "0835551234", "0845551234" };
foreach (var num in numbers)
{
    var result = num.ValidateSouthAfricanPhoneNumber();
    Console.WriteLine($"{num} -> {result.Operator}");
}
// 0825551234 -> Vodacom
// 0835551234 -> MTN
// 0845551234 -> CellC
```

### Landline Area Detection

```csharp
var jhb = "0115551234".ValidateSouthAfricanPhoneNumber();
Console.WriteLine(jhb.AreaDescription); // "Johannesburg & Gauteng"
```

### Error Handling

```csharp
var invalid = "123".ValidateSouthAfricanPhoneNumber();
if (!invalid.IsValid)
    Console.WriteLine(invalid.ErrorMessage); // "Invalid length: SA numbers should be 9-11 digits (got 3)"
```

---

## 📊 API Reference

### PhoneNumberValidationResult Properties

| Property | Description |
|----------|-------------|
| `IsValid` | True/false if number is valid |
| `NumberType` | Mobile, Landline, TollFree, Premium, Emergency |
| `Operator` | Vodacom, MTN, CellC, Telkom, Neotel |
| `AreaDescription` | Geographic area (e.g., "Johannesburg & Gauteng") |
| `NormalizedNumber` | International format (+27825551234) |
| `CleanedNumber` | Digits only (0825551234) |
| `ErrorMessage` | Details if invalid |

---

## 🛠️ Development

```bash
# Build
dotnet build

# Test
dotnet test

# Pack
dotnet pack -c Release -o ./packages
```

---

## 🤝 Contributing

1. Fork the repo
2. Create a feature branch
3. Commit changes
4. Push and open a PR

---

## 📄 License

MIT © Nkosinathi Tshabalala (Nathi-devDot)

---

**[GitHub Repository](https://github.com/NuGetPackagesPGLN/SAValidation)** • **[NuGet Profile](https://www.nuget.org/profiles/Nathi-devDot)**
```

## **Add to Git:**

```powershell
cd C:\Users\Nkosinathi\Desktop\SAValidation
git add README.md
git commit -m "docs: Add concise README"
git push origin main
```

