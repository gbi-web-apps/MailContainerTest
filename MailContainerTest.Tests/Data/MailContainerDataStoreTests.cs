using FluentAssertions;
using MailContainerTest.Abstractions;
using MailContainerTest.Data;
using MailContainerTest.Entities;
using Moq;
using Xunit;

namespace MailContainerTest.Tests.Data
{
    public class MailContainerDataStoreTests
    {
        [Fact]
        public void GetMailContainer_WhenCalled_ReturnsMailContainer()
        {
            // Arrange
            var mailDataStore = new MailContainerDataStore();
            var mailContainerName = "TEST";

            // Act
            var mailContainer = mailDataStore.GetMailContainer(mailContainerName);

            // Assert
            mailContainer.Should().NotBeNull();
            mailContainer.Should().BeOfType<MailContainer>();
        }

        [Fact]
        public void UpdateMailContainer_WhenCalled_ExecutesSuccessfully()
        {
            // Arrange
            //var mockMailDataStore = new Mock<IMailContainerDataStore>();
            var mock = new Mock<MailContainerDataStore>().As<IMailContainerDataStore>();
            mock.CallBase = true;
            var mailContainer = new MailContainer();

            // Act
            mock.Setup(x => x.UpdateMailContainer(It.IsAny<MailContainer>())).Verifiable();
            mock.Object.UpdateMailContainer(mailContainer);

            // Assert
            mailContainer.Should().NotBeNull();
            mock.Verify(x => x.UpdateMailContainer(mailContainer), Times.Once());
        }
    }
}