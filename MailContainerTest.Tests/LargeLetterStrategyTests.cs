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
    
    [Fact]
    public void IsSuccess_ShouldReturnFalse_WhenOneOfMailContainerStatusIsNotOperational()
    {
        // Arrange
        var strategy = new LargeLetterStrategy();
        var sourceContainer = new MailContainer
                              {
                                  AllowedMailType = AllowedMailType.LargeLetter,
                                  Status = MailContainerStatus.Operational
                              };
        var destContainer = new MailContainer
                            {
                                AllowedMailType = AllowedMailType.LargeLetter,
                                Status = MailContainerStatus.OutOfService
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
    
    [Fact]
    public void IsSuccess_ShouldReturnFalse_WhenContainersNull()
    {
        // Arrange
        var strategy = new LargeLetterStrategy();
        var request = new MakeMailTransferRequest
                      {
                          MailType = MailType.LargeLetter
                      };

        // Act
        var result = strategy.IsSuccess(null, null, request);

        // Assert
        result.Should().BeFalse();
    }
}