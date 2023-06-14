using FluentAssertions;
using MailContainerTest.Abstractions;
using MailContainerTest.Factories;
using MailContainerTest.Strategies;
using MailContainerTest.Strategies.Behaviours;
using MailContainerTest.Types;
using Xunit;

namespace MailContainerTest.Tests;

public sealed class MailTransferStrategyFactoryTests
{
    private readonly MailTransferStrategyFactory _sut;

    public MailTransferStrategyFactoryTests()
    {
        _sut = new MailTransferStrategyFactory(new AllowedMailTypeBehaviour(), new OperationalStatusBehaviour(), new CapacityBehaviour());
    }

    [Fact]
    public void CreateMakeMailTransferStrategy_ShouldReturnStandardLetter_WhenMailTypeIsStandardLetter()
    {
        // Arrange

        // Act
        var result = _sut.CreateMakeMailTransferStrategy(MailType.StandardLetter);

        // Assert
        result.Should().BeOfType<StandardLetterStrategy>();
    }

    [Fact]
    public void CreateMakeMailTransferStrategy_ShouldReturnLargeLetterStrategy_WhenMailTypeIsLargeLetter()
    {
        // Arrange

        // Act
        var result = _sut.CreateMakeMailTransferStrategy(MailType.LargeLetter);

        // Assert
        result.Should().BeOfType<LargeLetterStrategy>();
    }

    [Fact]
    public void CreateMakeMailTransferStrategy_ShouldReturnSmallParcelStrategy_WhenMailTypeIsSmallParcel()
    {
        // Arrange

        // Act
        var result = _sut.CreateMakeMailTransferStrategy(MailType.SmallParcel);

        // Assert
        result.Should().BeOfType<SmallParcelStrategy>();
    }

    [Fact]
    public void CreateMakeMailTransferStrategy_ShouldThrowArgumentOutOfRangeException_WhenMailTypeIsNotInEnumRange()
    {
        // Arrange

        // Act
        Action result = () => _sut.CreateMakeMailTransferStrategy((MailType) (-1));

        // Assert
        result.Should().Throw<ArgumentOutOfRangeException>().WithMessage("Mail type is not in enum range*");
    }
}