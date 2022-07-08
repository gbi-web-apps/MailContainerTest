using FluentAssertions;
using MailContainerTest.Data;
using MailContainerTest.Entities;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace MailContainerTest.Tests.Data
{
    public class MailContainterDataStoreFactoryTests
    {
        private readonly Mock<IOptions<DataStoreTypeOptions>> mockOptions;

        public MailContainterDataStoreFactoryTests()
        {
            mockOptions = new Mock<IOptions<DataStoreTypeOptions>>();
        }

        [Fact]
        public void CreateMailContainerDataStore_WhenCalled_AndDataStoreTypeIsBackup_ReturnsBackupMailContainerDataStore()
        {
            // Arrange
            mockOptions.SetupGet(x => x.Value).Returns(new DataStoreTypeOptions { DataStoreType = "Backup" });
            var factory = new MailContainterDataStoreFactory(mockOptions.Object);

            // Act
            var dataStore = factory.CreateMailContainerDataStore();

            // Assert
            dataStore.Should().NotBeNull();
            dataStore.Should().BeOfType<BackupMailContainerDataStore>();
        }

        [Fact]
        public void CreateMailContainerDataStore_WhenCalled_AndDataStoreTypeIsOtherThanBackup_ReturnsMailContainerDataStore()
        {
            // Arrange
            mockOptions.SetupGet(x => x.Value).Returns(new DataStoreTypeOptions { DataStoreType = "Other" });
            var factory = new MailContainterDataStoreFactory(mockOptions.Object);

            // Act
            var dataStore = factory.CreateMailContainerDataStore();

            // Assert
            dataStore.Should().NotBeNull();
            dataStore.Should().BeOfType<MailContainerDataStore>();
        }
    }
}