# SAValidation.Common

Shared utilities for South African validation libraries.

NuGet: https://www.nuget.org/packages/SAValidation.Common/
GitHub: https://github.com/NuGetPackagesPGLN/SAValidation

## Installation

dotnet add package SAValidation.Common

## Features

- String Extensions: Clean numbers, extract digits, safe substring operations
- Guard Clauses: Defensive programming helpers
- Shared Utilities: Common functionality used by all SAValidation packages
- Regex Helpers: Pre-compiled regex for optimal performance
- Null Safety: Built with nullable reference types

## Usage Examples

### Clean a phone number (remove spaces, dashes, parentheses)

using SAValidation.Common;

string dirty = "082 555 1234";
string clean = dirty.CleanNumber();
// Result: "0825551234"

string withDashes = "082-555-1234";
string clean2 = withDashes.CleanNumber();
// Result: "0825551234"

string withParentheses = "(082) 555-1234";
string clean3 = withParentheses.CleanNumber();
// Result: "0825551234"

### Extract only digits from any string

string mixed = "+27 (82) 555-1234";
string digits = mixed.ExtractDigits();
// Result: "27825551234"

string letters = "ABC123DEF456";
string digits2 = letters.ExtractDigits();
// Result: "123456"

### Check if string contains only digits

string numbers = "123456";
bool isDigits = numbers.IsAllDigits(); // true

string mixed = "123abc";
bool isDigits2 = mixed.IsAllDigits(); // false

### Check if string starts with any prefix

string phone = "0825551234";
bool startsWithMobile = phone.StartsWithAny("082", "083", "084");
// Result: true (starts with 082)

string landline = "0115551234";
bool startsWithMobile2 = landline.StartsWithAny("082", "083", "084");
// Result: false

### Safe substring with bounds checking

string text = "0825551234";

// Normal usage
string sub1 = text.SafeSubstring(0, 3); // "082"

// Bounds checking - won't throw exception
string sub2 = text.SafeSubstring(8, 10); // "34" (only takes available characters)

string empty = null;
string sub3 = empty.SafeSubstring(0, 5); // "" (handles null safely)

## API Reference

### StringExtensions Methods

| Method | Description | Example |
|--------|-------------|---------|
| CleanNumber() | Removes spaces, dashes, parentheses, dots | "082 555 1234" -> "0825551234" |
| ExtractDigits() | Returns only digits from string | "+27 (82) 555-1234" -> "27825551234" |
| IsAllDigits() | Checks if string contains only digits | "123456" -> true |
| StartsWithAny() | Checks if string starts with any of the provided prefixes | "0825551234" with ["082","083"] -> true |
| SafeSubstring() | Gets substring with bounds checking | "0825551234", 8, 10 -> "34" |

### Guard Class Methods

| Method | Description |
|--------|-------------|
| AgainstNullOrWhiteSpace() | Throws if string is null or whitespace |
| AgainstOutOfRange() | Throws if value is outside specified range |

## Contributing

Contributions are welcome! Please submit pull requests to the GitHub repository.

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Open a Pull Request

## License

MIT