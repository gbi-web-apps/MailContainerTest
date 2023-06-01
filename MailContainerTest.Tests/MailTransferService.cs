using FluentAssertions;
using MailContainerTest.Abstractions;
using MailContainerTest.Data;
using MailContainerTest.Services;
using MailContainerTest.Types;
using NSubstitute;
using Xunit;

namespace MailContainerTest.Tests;

public sealed class MailTransferServiceTests
{
    private readonly IMailContainerDataStoreFactory _mailContainerDataStoreFactory = Substitute.For<IMailContainerDataStoreFactory>();
    private readonly IMailTransferStrategyFactory _mailTransferStrategyFactory = Substitute.For<IMailTransferStrategyFactory>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly ILoggerAdapter<IMailTransferService> _loggerAdapter = Substitute.For<ILoggerAdapter<IMailTransferService>>();

    [Fact]
    public void MakeMailTransfer_ShouldReturnFalse_WhenStrategyNotSuccessful()
    {
        // Arrange
        _mailContainerDataStoreFactory.CreateMailContainerDataStore()
                                      .Returns(new MailContainerDataStore());
        _mailTransferStrategyFactory.CreateMakeMailTransferStrategy(Arg.Any<MailType>())
                                    .IsSuccess(Arg.Any<MailContainer?>(), Arg.Any<MailContainer?>(), Arg.Any<MakeMailTransferRequest>())
                                    .Returns(false);

        var mailTransferService = new MailTransferService(_mailContainerDataStoreFactory, _mailTransferStrategyFactory, _unitOfWork, _loggerAdapter);

        // Act
        var result = mailTransferService.MakeMailTransfer(Arg.Any<MakeMailTransferRequest>());

        // Assert
        _unitOfWork.DidNotReceive().Commit();
        result.Success.Should().BeFalse();
    }
    
    [Fact]
    public void MakeMailTransfer_ShouldReturnTrue_WhenStrategyNotSuccessful()
    {
        // Arrange
        _mailContainerDataStoreFactory.CreateMailContainerDataStore()
                                      .Returns(new MailContainerDataStore());
        _mailTransferStrategyFactory.CreateMakeMailTransferStrategy(Arg.Any<MailType>())
                                    .IsSuccess(Arg.Any<MailContainer?>(), Arg.Any<MailContainer?>(), Arg.Any<MakeMailTransferRequest>())
                                    .Returns(true);

        var mailTransferService = new MailTransferService(_mailContainerDataStoreFactory, _mailTransferStrategyFactory, _unitOfWork, _loggerAdapter);

        // Act
        var result = mailTransferService.MakeMailTransfer(Arg.Any<MakeMailTransferRequest>());

        // Assert
        _unitOfWork.Received(1).Commit();
        result.Success.Should().BeTrue();
    }
    
    [Fact]
    public void MakeMailTransfer_ShouldReturnFalse_WhenCommitNotSuccessful()
    {
        // Arrange
        _mailContainerDataStoreFactory.CreateMailContainerDataStore()
                                      .Returns(new MailContainerDataStore());
        _mailTransferStrategyFactory.CreateMakeMailTransferStrategy(Arg.Any<MailType>())
                                    .IsSuccess(Arg.Any<MailContainer?>(), Arg.Any<MailContainer?>(), Arg.Any<MakeMailTransferRequest>())
                                    .Returns(true);
        
        var mailTransferService = new MailTransferService(_mailContainerDataStoreFactory, _mailTransferStrategyFactory, _unitOfWork, _loggerAdapter);

        _unitOfWork.When(static x => x.Commit()).Throw<Exception>();
        
        // Act
        var result = mailTransferService.MakeMailTransfer(Arg.Any<MakeMailTransferRequest>());

        // Assert
        _unitOfWork.Received(1).Commit();
        _unitOfWork.Received(1).Rollback();
        _loggerAdapter.Received(1).LogError(Arg.Any<Exception>(), "Error saving changes to containers");
        result.Success.Should().BeFalse();
    }
}