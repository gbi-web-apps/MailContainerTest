using FluentAssertions;
using MailContainerTest.Strategies;
using MailContainerTest.Types;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using Xunit;

namespace MailContainerTest.Tests;

public sealed class LargeLetterStrategyTests
{
    [Fact]
    public void IsSuccess_ShouldReturnTrue_WhenAllowedMailTypeEqual()
    {
        // Arrange
        var strategy = new LargeLetterStrategy();
        var sourceContainer = new MailContainer
                              {
                                  AllowedMailType = AllowedMailType.LargeLetter
                              };
        var destContainer = new MailContainer
                            {
                                AllowedMailType = AllowedMailType.LargeLetter
                            };
        var request = new MakeMailTransferRequest
                      {
                          MailType = MailType.LargeLetter
                      };

        // Act
        var result = strategy.IsSuccess(sourceContainer, destContainer, request);

        // Assert
        result.Should().BeTrue();
    }
    
    [Fact]
    public void IsSuccess_ShouldReturnFalse_WhenFAllowedMailTypeNotEqual()
    {
        // Arrange
        var strategy = new LargeLetterStrategy();
        var sourceContainer = new MailContainer
                              {
                                  AllowedMailType = AllowedMailType.LargeLetter
                              };
        var destContainer = new MailContainer
                            {
                                AllowedMailType = AllowedMailType.SmallParcel
                            };
        var request = new MakeMailTransferRequest
                      {
                          MailType = MailType.LargeLetter
                      };

        // Act
        var result = strategy.IsSuccess(sourceContainer, destContainer, request);

        // Assert
        result.Should().BeFalse();
    }
}