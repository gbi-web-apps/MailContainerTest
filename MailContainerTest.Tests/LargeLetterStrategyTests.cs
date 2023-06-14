using FluentAssertions;
using MailContainerTest.Abstractions;
using MailContainerTest.Strategies;
using MailContainerTest.Tests.Builders;
using MailContainerTest.Types;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using Xunit;

namespace MailContainerTest.Tests;

public sealed class LargeLetterStrategyTests
{
    private readonly IAllowedMailTypeBehaviour _allowedMailTypeBehaviour = Substitute.For<IAllowedMailTypeBehaviour>();
    private readonly IOperationalStatusBehaviour _operationalStatusBehaviour = Substitute.For<IOperationalStatusBehaviour>();
    private readonly ICapacityBehaviour _capacityBehaviour = Substitute.For<ICapacityBehaviour>();

    private readonly LargeLetterStrategy _sut;

    public LargeLetterStrategyTests()
    {
        _sut = new LargeLetterStrategy(_allowedMailTypeBehaviour, _operationalStatusBehaviour, _capacityBehaviour);
    }

    [Fact]
    public void IsSuccess_ShouldReturnTrue_WhenAllowedMailTypeIsEqualAndOperationalAndWithinCapacity()
    {
        // Arrange
        var sourceContainer = new MailContainerBuilder()
                              .WithAllowedMailType(AllowedMailType.LargeLetter)
                              .Build();

        var destContainer = new MailContainerBuilder()
                            .WithAllowedMailType(AllowedMailType.SmallParcel)
                            .Build();

        var request = new MakeMailTransferRequestBuilder()
                      .WithMailType(MailType.LargeLetter)
                      .Build();

        _allowedMailTypeBehaviour.IsAllowedMailType(Arg.Any<MailContainer>(), Arg.Any<MailContainer>()).Returns(true);
        _operationalStatusBehaviour.IsOperationalStatus(Arg.Any<MailContainer>(), Arg.Any<MailContainer>()).Returns(true);
        _capacityBehaviour.IsWithinCapacity(Arg.Any<MailContainer>(), Arg.Any<MakeMailTransferRequest>()).Returns(true);

        // Act
        var result = _sut.IsSuccess(sourceContainer, destContainer, request);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsSuccess_ShouldReturnFalse_WhenAllowedMailTypeNotEqual()
    {
        // Arrange
        var sourceContainer = new MailContainerBuilder()
                              .WithAllowedMailType(AllowedMailType.LargeLetter)
                              .Build();
    
        var destContainer = new MailContainerBuilder()
                            .WithAllowedMailType(AllowedMailType.SmallParcel)
                            .Build();
    
        var request = new MakeMailTransferRequestBuilder()
                      .WithMailType(MailType.LargeLetter)
                      .Build();
    
        // Act
        var result = _sut.IsSuccess(sourceContainer, destContainer, request);
    
        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void IsSuccess_ShouldReturnFalse_WhenOneOfMailContainerStatusIsNotOperational()
    {
        // Arrange
        var sourceContainer = new MailContainerBuilder()
                              .WithAllowedMailType(AllowedMailType.LargeLetter)
                              .WithStatus(MailContainerStatus.Operational)
                              .Build();
    
        var destContainer = new MailContainerBuilder()
                            .WithAllowedMailType(AllowedMailType.LargeLetter)
                            .WithStatus(MailContainerStatus.OutOfService)
                            .Build();
    
        var request = new MakeMailTransferRequestBuilder()
                      .WithMailType(MailType.LargeLetter)
                      .Build();
    
        // Act
        var result = _sut.IsSuccess(sourceContainer, destContainer, request);
    
        // Assert
        result.Should().BeFalse();
    }
    
    // [Theory]
    // [MemberData(nameof(FailingTestData))]
    // public void IsSuccess_ShouldReturnFalse_WhenAllowedMailTypeIsNotEqualAndNotOperationalAndNotWithinCapacity(
    //     MailContainer sourceContainer,
    //     MailContainer destContainer,
    //     MakeMailTransferRequest request)
    // {
    //     // Arrange
    //
    //
    //     // Act
    //     var result = _sut.IsSuccess(sourceContainer, destContainer, request);
    //
    //     // Assert
    //     result.Should().BeFalse();
    // }
    //
    // public static IEnumerable<object[]> FailingTestData =>
    //     new List<object[]>
    //     {
    //         new object[]
    //         {
    //             new MailContainerBuilder()
    //                 .WithAllowedMailType(AllowedMailType.LargeLetter)
    //                 .Build(),
    //             new MailContainerBuilder()
    //                 .WithAllowedMailType(AllowedMailType.SmallParcel)
    //                 .Build(),
    //             new MakeMailTransferRequestBuilder()
    //                 .WithMailType(MailType.LargeLetter)
    //                 .Build()
    //         },
    //         new object[]
    //         {
    //             new MailContainerBuilder()
    //                 .WithAllowedMailType(AllowedMailType.LargeLetter)
    //                 .WithStatus(MailContainerStatus.Operational)
    //                 .Build(),
    //             new MailContainerBuilder()
    //                 .WithAllowedMailType(AllowedMailType.LargeLetter)
    //                 .WithStatus(MailContainerStatus.OutOfService)
    //                 .Build(),
    //             new MakeMailTransferRequestBuilder()
    //                 .WithMailType(MailType.LargeLetter)
    //                 .Build()
    //         },
    //         new object[]
    //         {
    //             new MailContainerBuilder()
    //                 .WithAllowedMailType(AllowedMailType.LargeLetter)
    //                 .WithStatus(MailContainerStatus.Operational)
    //                 .WithCapacity(100)
    //                 .Build(),
    //             new MailContainerBuilder()
    //                 .WithAllowedMailType(AllowedMailType.LargeLetter)
    //                 .WithStatus(MailContainerStatus.Operational)
    //                 .WithCapacity(100)
    //                 .Build(),
    //             new MakeMailTransferRequestBuilder()
    //                 .WithMailType(MailType.LargeLetter)
    //                 .WithNumberOfMailItems(200)
    //                 .Build()
    //         }
    //     };
}