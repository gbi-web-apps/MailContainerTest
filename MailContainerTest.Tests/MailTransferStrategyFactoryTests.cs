using FluentAssertions;
using MailContainerTest.Factories;
using MailContainerTest.Strategies;
using MailContainerTest.Types;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace MailContainerTest.Tests;

public sealed class MailTransferStrategyFactoryTests
{
    [Fact]
    public void CreateMakeMailTransferStrategy_ShouldReturnStandardLetter_WhenMailTypeIsStandardLetter()
    {
        // Arrange
        var factory = new MailTransferStrategyFactory();

        // Act
        var result = factory.CreateMakeMailTransferStrategy(MailType.StandardLetter);

        // Assert
        result.Should().BeOfType<StandardLetterStrategy>();
    }
    
    [Fact]
    public void CreateMakeMailTransferStrategy_ShouldReturnLargeLetterStrategy_WhenMailTypeIsLargeLetter()
    {
        // Arrange
        var factory = new MailTransferStrategyFactory();

        // Act
        var result = factory.CreateMakeMailTransferStrategy(MailType.LargeLetter);

        // Assert
        result.Should().BeOfType<LargeLetterStrategy>();
    }
    
    [Fact]
    public void CreateMakeMailTransferStrategy_ShouldReturnSmallParcelStrategy_WhenMailTypeIsSmallParcel()
    {
        // Arrange
        var factory = new MailTransferStrategyFactory();

        // Act
        var result = factory.CreateMakeMailTransferStrategy(MailType.SmallParcel);

        // Assert
        result.Should().BeOfType<SmallParcelStrategy>();
    }

    [Fact]
    public void CreateMakeMailTransferStrategy_ShouldThrowArgumentOutOfRangeException_WhenMailTypeIsNotInEnumRange()
    {
        // Arrange
        var factory = new MailTransferStrategyFactory();

        // Act
        Action result = () => factory.CreateMakeMailTransferStrategy((MailType) (-1));

        // Assert
        result.Should().Throw<ArgumentOutOfRangeException>().WithMessage("Mail type is not in enum range*");
    }
}