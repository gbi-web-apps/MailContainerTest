using MailContainerTest.Data;
using MailContainerTest.Services;
using MailContainerTest.Types;
using Microsoft.VisualBasic;
using Moq;
using NUnit.Framework;

[TestFixture]
public class MailTransferServiceTests
{
    private IMailContainerDataStore mailContainerDataStore;
    private MailTransferService mailTransferService;

    [SetUp]
    public void Setup()
    {
        // Set up the mock for IMailContainerDataStore
        var mailContainerDataStoreMock = new Mock<IMailContainerDataStore>();

        // Configure the mock behavior for GetMailContainer
        mailContainerDataStoreMock.Setup(d => d.GetMailContainer(It.IsAny<string>()))
            .Returns((string containerNumber) =>
            {
                // Implement the desired behavior for the fake/mock implementation
                // Return a mock MailContainer object or null based on the containerNumber

                // Example: Creating a mock MailContainer with desired properties
                var mailContainerMock = new Mock<MailContainer>();
                mailContainerMock.Setup(c => c.AllowedMailType).Returns(AllowedMailType.StandardLetter);
                mailContainerMock.Setup(c => c.Capacity).Returns(10);
                mailContainerMock.Setup(c => c.Status).Returns(MailContainerStatus.Operational);

                // Return the mock MailContainer based on the containerNumber
                if (containerNumber.Equals("1"))
                {
                    return mailContainerMock.Object;
                }
                else
                {
                    return null;
                }
            });

        // Assign the mock to the mailContainerDataStore
        mailContainerDataStore = mailContainerDataStoreMock.Object;

        // Create an instance of MailTransferService using the mock
        mailTransferService = new MailTransferService(mailContainerDataStore);
    }

    [Test]
    public void MakeMailTransfer_StandardLetter_Success()
    {
        // Arrange
        var request = new MakeMailTransferRequest
        {
            SourceMailContainerNumber = "1",
            MailType = MailType.StandardLetter
        };

        // Act
        var result = mailTransferService.MakeMailTransfer(request);

        // Assert
        Assert.IsTrue(result.Success);
    }

    [Test]
    public void MakeMailTransfer_StandardLetter_InvalidContainer()
    {
        // Arrange
        var request = new MakeMailTransferRequest
        {
            SourceMailContainerNumber = "2",
            MailType = MailType.StandardLetter
        };

        // Act
        var result = mailTransferService.MakeMailTransfer(request);

        // Assert
        Assert.IsFalse(result.Success);
    }

    [Test]
    public void MakeMailTransfer_LargeLetter_Success()
    {
        // Arrange
        var request = new MakeMailTransferRequest
        {
            SourceMailContainerNumber = "1",
            MailType = MailType.LargeLetter,
            NumberOfMailItems = 5
        };

        // Act
        var result = mailTransferService.MakeMailTransfer(request);

        // Assert
        Assert.IsTrue(result.Success);
    }

    [Test]
    public void MakeMailTransfer_LargeLetter_InvalidContainer()
    {
        // Arrange
        var request = new MakeMailTransferRequest
        {
            SourceMailContainerNumber = "2",
            MailType = MailType.LargeLetter,
            NumberOfMailItems = 5
        };

        // Act
        var result = mailTransferService.MakeMailTransfer(request);

        // Assert
        Assert.IsFalse(result.Success);
    }

    [Test]
    public void MakeMailTransfer_LargeLetter_InsufficientCapacity()
    {
        // Arrange
        var request = new MakeMailTransferRequest
        {
            SourceMailContainerNumber = "1",
            MailType = MailType.LargeLetter,
            NumberOfMailItems = 10
        };

        // Act
        var result = mailTransferService.MakeMailTransfer(request);

        // Assert
        Assert.IsFalse(result.Success);
    }

    [Test]
    public void MakeMailTransfer_SmallParcel_Success()
    {
        // Arrange
        var request = new MakeMailTransferRequest
        {
            SourceMailContainerNumber = "1",
            MailType = MailType.SmallParcel
        };

        // Act
        var result = mailTransferService.MakeMailTransfer(request);

        // Assert
        Assert.IsTrue(result.Success);
    }

    [Test]
    public void MakeMailTransfer_SmallParcel_InvalidContainer()
    {
        // Arrange
        var request = new MakeMailTransferRequest
        {
            SourceMailContainerNumber = "2",
            MailType = MailType.SmallParcel
        };

        // Act
        var result = mailTransferService.MakeMailTransfer(request);

        // Assert
        Assert.IsFalse(result.Success);
    }

    [Test]
    public void MakeMailTransfer_SmallParcel_NonOperationalContainer()
    {
        // Arrange
        var request = new MakeMailTransferRequest
        {
            SourceMailContainerNumber = "1",
            MailType = MailType.SmallParcel
        };

        // Act
        var result = mailTransferService.MakeMailTransfer(request);

        // Assert
        Assert.IsFalse(result.Success);
    }
}