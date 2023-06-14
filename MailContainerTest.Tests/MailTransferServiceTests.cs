using FluentAssertions;
using FluentAssertions.Execution;
using MailContainerTest.Abstractions;
using MailContainerTest.Data;
using MailContainerTest.Services;
using MailContainerTest.Tests.Builders;
using MailContainerTest.Types;
using NSubstitute;
using Xunit;

namespace MailContainerTest.Tests;

public sealed class MailTransferServiceTests
{
    private readonly IMailContainerDataStoreFactory _mailContainerDataStoreFactory = Substitute.For<IMailContainerDataStoreFactory>();
    private readonly IMailContainerDataStore _mailContainerDataStore = Substitute.For<IMailContainerDataStore>();
    private readonly IMailTransferStrategyFactory _mailTransferStrategyFactory = Substitute.For<IMailTransferStrategyFactory>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly ILoggerAdapter<IMailTransferService> _loggerAdapter = Substitute.For<ILoggerAdapter<IMailTransferService>>();

    private readonly MailTransferService _sut;

    public MailTransferServiceTests()
    {
        _sut = new MailTransferService(_mailContainerDataStoreFactory, _mailTransferStrategyFactory, _unitOfWork, _loggerAdapter);
    }

    [Fact]
    public void MakeMailTransfer_ShouldReturnFalse_WhenStrategyNotSuccessful()
    {
        // Arrange
        var request = new MakeMailTransferRequestBuilder()
                      .WithSourceMailContainerNumber("1")
                      .WithDestinationMailContainerNumber("2")
                      .WithMailType(MailType.LargeLetter)
                      .WithNumberOfMailItems(1)
                      .Build();
        
        _mailContainerDataStoreFactory.CreateMailContainerDataStore()
                                      .Returns(new MailContainerDataStore());
        
        _mailTransferStrategyFactory.CreateMakeMailTransferStrategy(Arg.Any<MailType>())
                                    .IsSuccess(Arg.Any<MailContainer>(), Arg.Any<MailContainer>(), Arg.Any<MakeMailTransferRequest>())
                                    .Returns(false);

        // Act
        var result = _sut.MakeMailTransfer(request);

        // Assert
        using (new AssertionScope())
        {
            _unitOfWork.DidNotReceive().Commit();
            result.Success.Should().BeFalse();
        }
    }

    [Fact]
    public void MakeMailTransfer_ShouldReturnTrue_WhenStrategySuccessful()
    {
        // Arrange
        var request = new MakeMailTransferRequestBuilder()
                      .WithSourceMailContainerNumber("1")
                      .WithDestinationMailContainerNumber("2")
                      .WithMailType(MailType.LargeLetter)
                      .WithNumberOfMailItems(1)
                      .Build();
        
        var sourceContainer = new MailContainerBuilder()
                              .WithAllowedMailType(AllowedMailType.LargeLetter)
                              .WithMailContainerNumber("1")
                              .WithStatus(MailContainerStatus.Operational)
                              .WithCapacity(100)
                              .Build();
        
        var destContainer = new MailContainerBuilder()
                            .WithAllowedMailType(AllowedMailType.LargeLetter)
                            .WithMailContainerNumber("2")
                            .WithStatus(MailContainerStatus.Operational)
                            .WithCapacity(100)
                            .Build();

        _mailContainerDataStoreFactory.CreateMailContainerDataStore()
                                      .Returns(_mailContainerDataStore);
        
        _mailContainerDataStore.GetMailContainer(Arg.Any<MailContainerNumber>())
                               .Returns(sourceContainer, destContainer);

        _mailTransferStrategyFactory.CreateMakeMailTransferStrategy(Arg.Any<MailType>())
                                    .IsSuccess(Arg.Any<MailContainer>(), Arg.Any<MailContainer>(), Arg.Any<MakeMailTransferRequest>())
                                    .Returns(true);

        // Act
        var result = _sut.MakeMailTransfer(request);

        // Assert
        using (new AssertionScope())
        {
            _unitOfWork.Received(1).Commit();
            result.Success.Should().BeTrue();
        }
    }

    [Fact]
    public void MakeMailTransfer_ShouldReturnFalse_WhenCommitNotSuccessful()
    {
        // Arrange
        var request = new MakeMailTransferRequestBuilder()
                      .WithSourceMailContainerNumber("1")
                      .WithDestinationMailContainerNumber("2")
                      .WithMailType(MailType.LargeLetter)
                      .WithNumberOfMailItems(1)
                      .Build();
        
        var sourceContainer = new MailContainerBuilder()
                              .WithAllowedMailType(AllowedMailType.LargeLetter)
                              .WithMailContainerNumber("1")
                              .WithStatus(MailContainerStatus.Operational)
                              .WithCapacity(100)
                              .Build();
        
        var destContainer = new MailContainerBuilder()
                            .WithAllowedMailType(AllowedMailType.LargeLetter)
                            .WithMailContainerNumber("2")
                            .WithStatus(MailContainerStatus.Operational)
                            .WithCapacity(100)
                            .Build();
        
        _mailContainerDataStoreFactory.CreateMailContainerDataStore()
                                      .Returns(_mailContainerDataStore);
        
        _mailContainerDataStore.GetMailContainer(Arg.Any<MailContainerNumber>())
                               .Returns(sourceContainer, destContainer);

        _mailTransferStrategyFactory.CreateMakeMailTransferStrategy(Arg.Any<MailType>())
                                    .IsSuccess(Arg.Is(sourceContainer), Arg.Is(destContainer), Arg.Any<MakeMailTransferRequest>())
                                    .Returns(true);

        _unitOfWork.When(static x => x.Commit()).Throw<Exception>();

        // Act
        var result = _sut.MakeMailTransfer(request);

        // Assert
        using (new AssertionScope())
        {
            _unitOfWork.Received(1).Commit();
            _unitOfWork.Received(1).Rollback();
            _loggerAdapter.Received(1).LogError(Arg.Any<Exception>(), "Error saving changes to containers");
            result.Success.Should().BeFalse();
        }
    }
}