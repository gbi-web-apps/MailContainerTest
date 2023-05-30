using MailContainerTest;
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
        // Create an instance of the concrete implementation (BackupMailContainerDataStore)

        ContainerRegistry.Initilize();

        mailContainerDataStore = new MailContainerDataStore();

        // Create an instance of MailTransferService using the concrete implementation
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
            NumberOfMailItems = 5,
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
            MailType = MailType.SmallParcel,
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