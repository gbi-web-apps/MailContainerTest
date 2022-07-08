using FluentAssertions;
using MailContainerTest.Abstractions;
using MailContainerTest.Entities;
using MailContainerTest.Services;
using Moq;
using Xunit;

namespace MailContainerTest.Tests.Services
{
    public class MailTransferServiceTests
    {
        [Fact]
        public void MakeMailTransfer_WhenCalled_ReturnsSuccess()
        {
            // Arrange

            MakeMailTransferRequest request = new MakeMailTransferRequest
            {
                SourceMailContainerNumber = "",
                DestinationMailContainerNumber = "",
                NumberOfMailItems = 1,
                TransferDate = DateTime.Now,
                MailType = Entities.Types.MailType.StandardLetter | Entities.Types.MailType.LargeLetter | Entities.Types.MailType.SmallParcel,
            };

            var mockContainerDataStoreFactory = new Mock<IMailContainerDataStoreFactory>();
            IMailTransferService transferService = new MailTransferService(mockContainerDataStoreFactory.Object);

            var mailContainer = new Mock<IMailContainerDataStore>();
            mailContainer
                .Setup(x => x.GetMailContainer(It.IsAny<string>()))
                .Returns(new MailContainer { MailType = Entities.Types.MailType.StandardLetter });

            mockContainerDataStoreFactory
                .Setup(x => x.CreateMailContainerDataStore())
                .Returns(mailContainer.Object);

            // Act
            var result = transferService.MakeMailTransfer(request);

            // Assert
            result.Success.Should().BeTrue();
            mailContainer.Verify(x => x.UpdateMailContainer(It.IsAny<MailContainer>()), Times.Once());
        }

        [Fact]
        public void MakeMailTransfer_WhenCalled_ReturnsFailed()
        {
            // Arrange

            MakeMailTransferRequest request = new MakeMailTransferRequest
            {
                SourceMailContainerNumber = "",
                DestinationMailContainerNumber = "",
                NumberOfMailItems = 1,
                TransferDate = DateTime.Now,
                MailType = Entities.Types.MailType.SmallParcel,
            };

            var mockContainerDataStoreFactory = new Mock<IMailContainerDataStoreFactory>();
            IMailTransferService transferService = new MailTransferService(mockContainerDataStoreFactory.Object);

            var mailContainer = new Mock<IMailContainerDataStore>();
            mailContainer
                .Setup(x => x.GetMailContainer(It.IsAny<string>()))
                .Returns(new MailContainer { MailType = Entities.Types.MailType.StandardLetter });

            mockContainerDataStoreFactory
                .Setup(x => x.CreateMailContainerDataStore())
                .Returns(mailContainer.Object);

            // Act
            var result = transferService.MakeMailTransfer(request);

            // Assert
            result.Success.Should().BeFalse();
            mailContainer.Verify(x => x.UpdateMailContainer(It.IsAny<MailContainer>()), Times.Never());
        }
    }
}