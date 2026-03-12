// tests/SAValidation.Common.Tests/StringExtensionsTests.cs
using FluentAssertions;
using SAValidation.Common;

namespace SAValidation.Common.Tests;

public class StringExtensionsTests
{
    [Theory]
    [InlineData("082 555 1234", "0825551234")]
    [InlineData("082-555-1234", "0825551234")]
    [InlineData("082.555.1234", "0825551234")]
    [InlineData("082  555   1234", "0825551234")]
    [InlineData("(082) 555-1234", "0825551234")]
    [InlineData("", "")]
    [InlineData(null, "")]
    public void CleanNumber_ShouldRemoveFormatting(string input, string expected)
    {
        // Act
        var result = input.CleanNumber();
        
        // Assert
        result.Should().Be(expected);
    }
    
    [Theory]
    [InlineData("082 555-1234", "0825551234")]
    [InlineData("+27 (82) 555-1234", "27825551234")]
    [InlineData("abc123def456", "123456")]
    [InlineData("", "")]
    [InlineData(null, "")]
    public void ExtractDigits_ShouldReturnOnlyDigits(string input, string expected)
    {
        // Act
        var result = input.ExtractDigits();
        
        // Assert
        result.Should().Be(expected);
    }
    
    [Theory]
    [InlineData("123456", true)]
    [InlineData("0825551234", true)]
    [InlineData("123abc", false)]
    [InlineData("", false)]
    [InlineData(null, false)]
    public void IsAllDigits_ShouldCheckCorrectly(string input, bool expected)
    {
        // Act
        var result = input.IsAllDigits();
        
        // Assert
        result.Should().Be(expected);
    }
    
    [Theory]
    [InlineData("0825551234", new[] { "082", "083" }, true)]
    [InlineData("0115551234", new[] { "082", "083" }, false)]
    [InlineData("", new[] { "082" }, false)]
    [InlineData(null, new[] { "082" }, false)]
    public void StartsWithAny_ShouldCheckCorrectly(string input, string[] prefixes, bool expected)
    {
        // Act
        var result = input.StartsWithAny(prefixes);
        
        // Assert
        result.Should().Be(expected);
    }
    
    [Theory]
    [InlineData("0825551234", 0, 3, "082")]
    [InlineData("0825551234", 3, 3, "555")]
    [InlineData("0825551234", 10, 3, "")]
    [InlineData("0825551234", 8, 10, "234")]
    public void SafeSubstring_ShouldHandleBounds(string input, int start, int length, string expected)
    {
        // Act
        var result = input.SafeSubstring(start, length);
        
        // Assert
        result.Should().Be(expected);
    }
}