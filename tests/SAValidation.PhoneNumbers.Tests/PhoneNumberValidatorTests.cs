using FluentAssertions;
using SAValidation.PhoneNumbers;

namespace SAValidation.PhoneNumbers.Tests;

public class PhoneNumberValidatorTests
{
    private readonly PhoneNumberValidator _validator;
    
    public PhoneNumberValidatorTests()
    {
        _validator = new PhoneNumberValidator();
    }
    
    [Fact]
    public void Validate_NullInput_ReturnsFailure()
    {
        // Act
        var result = _validator.Validate(null!);
        
        // Assert
        result.IsValid.Should().BeFalse();
        result.ErrorMessage.Should().Contain("empty");
    }
    
    [Fact]
    public void Validate_EmptyInput_ReturnsFailure()
    {
        // Act
        var result = _validator.Validate("");
        
        // Assert
        result.IsValid.Should().BeFalse();
        result.ErrorMessage.Should().Contain("empty");
    }
    
    [Theory]
    [InlineData("0825551234", PhoneNumberType.Mobile, NetworkOperator.Vodacom)]
    [InlineData("0835551234", PhoneNumberType.Mobile, NetworkOperator.MTN)]
    [InlineData("0845551234", PhoneNumberType.Mobile, NetworkOperator.CellC)]
    [InlineData("0815551234", PhoneNumberType.Mobile, NetworkOperator.Telkom)]
    [InlineData("0855551234", PhoneNumberType.Mobile, NetworkOperator.Neotel)]
    [InlineData("0725551234", PhoneNumberType.Mobile, NetworkOperator.Vodacom)]
    [InlineData("0765551234", PhoneNumberType.Mobile, NetworkOperator.MTN)]
    [InlineData("0795551234", PhoneNumberType.Mobile, NetworkOperator.CellC)]
    public void MobileNumbers_ShouldDetectCorrectTypeAndOperator(string number, 
        PhoneNumberType expectedType, NetworkOperator expectedOperator)
    {
        // Act
        var result = _validator.Validate(number);
        
        // Assert
        result.IsValid.Should().BeTrue();
        result.NumberType.Should().Be(expectedType);
        result.Operator.Should().Be(expectedOperator);
    }
    
    [Theory]
    [InlineData("0115551234", "11", "Johannesburg")]
    [InlineData("0125551234", "12", "Pretoria")]
    [InlineData("0215551234", "21", "Cape Town")]
    [InlineData("0315551234", "31", "Durban")]
    [InlineData("0415551234", "41", "Port Elizabeth")]
    [InlineData("0515551234", "51", "Bloemfontein")]
    public void LandlineNumbers_ShouldDetectCorrectArea(string number, 
        string expectedCode, string expectedAreaPartial)
    {
        // Act
        var result = _validator.Validate(number);
        
        // Assert
        result.IsValid.Should().BeTrue();
        result.NumberType.Should().Be(PhoneNumberType.Landline);
        result.AreaCode.Should().Be(expectedCode);
        result.AreaDescription.Should().Contain(expectedAreaPartial);
    }
    
    [Theory]
    [InlineData("0860123456", PhoneNumberType.Premium)]
    [InlineData("08615551234", PhoneNumberType.Premium)]
    public void PremiumNumbers_ShouldDetectCorrectType(string number, PhoneNumberType expectedType)
    {
        // Act
        var result = _validator.Validate(number);
        
        // Assert
        result.IsValid.Should().BeTrue();
        result.NumberType.Should().Be(expectedType);
    }
    
    [Theory]
    [InlineData("0800123456", PhoneNumberType.TollFree)]
    [InlineData("08005551234", PhoneNumberType.TollFree)]
    public void TollFreeNumbers_ShouldDetectCorrectType(string number, PhoneNumberType expectedType)
    {
        // Act
        var result = _validator.Validate(number);
        
        // Assert
        result.IsValid.Should().BeTrue();
        result.NumberType.Should().Be(expectedType);
    }
    
    [Theory]
    [InlineData("10111")]
    [InlineData("112")]
    [InlineData("107")]
    public void EmergencyNumbers_ShouldBeValid(string number)
    {
        // Act
        var result = _validator.Validate(number);
        
        // Assert
        result.IsValid.Should().BeTrue();
        result.NumberType.Should().Be(PhoneNumberType.Emergency);
    }
    
    [Theory]
    [InlineData("0825551234", "+27825551234")]
    [InlineData("082 555 1234", "+27825551234")]
    [InlineData("082-555-1234", "+27825551234")]
    [InlineData("+27 82 555 1234", "+27825551234")]
    [InlineData("27825551234", "+27825551234")]
    public void Normalization_ShouldProduceCorrectFormat(string input, string expected)
    {
        // Act
        var result = _validator.Validate(input);
        
        // Assert
        result.IsValid.Should().BeTrue();
        result.NormalizedNumber.Should().Be(expected);
    }
    
    [Theory]
    [InlineData("123")]
    [InlineData("08255")]
    [InlineData("082555123456789")]
    [InlineData("0995551234")]
    public void InvalidNumbers_ShouldFail(string number)
    {
        // Act
        var result = _validator.Validate(number);
        
        // Assert
        result.IsValid.Should().BeFalse();
        result.ErrorMessage.Should().NotBeNullOrEmpty();
    }
    
    [Fact]
    public void ToString_OnValidResult_ReturnsReadableSummary()
    {
        // Arrange
        var result = _validator.Validate("0825551234");
        
        // Act
        var summary = result.ToString();
        
        // Assert
        summary.Should().Contain("Valid Mobile")
               .And.Contain("Vodacom")
               .And.Contain("+27825551234");
    }
    
    [Fact]
    public void IsValid_QuickValidation_ReturnsBoolean()
    {
        // Act & Assert
        _validator.IsValid("0825551234").Should().BeTrue();
        _validator.IsValid("invalid").Should().BeFalse();
    }
    
    [Theory]
    [InlineData("0825551234", "0825551234")]
    [InlineData("082 555 1234", "0825551234")]
    [InlineData("082-555-1234", "0825551234")]
    public void CleanedNumber_RemovesFormatting(string input, string expected)
    {
        // Act
        var result = _validator.Validate(input);
        
        // Assert
        result.CleanedNumber.Should().Be(expected);
    }
}