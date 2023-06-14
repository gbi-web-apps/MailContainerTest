using MailContainerTest.Types;

namespace MailContainerTest.Tests.Builders;

public sealed class MakeMailTransferRequestBuilder
{
    private MakeMailTransferRequest _makeMailTransferRequest = new();
    
    public MakeMailTransferRequestBuilder WithSourceMailContainerNumber(MailContainerNumber sourceMailContainerNumber)
    {
        _makeMailTransferRequest = _makeMailTransferRequest with { SourceMailContainerNumber = sourceMailContainerNumber };

        return this;
    }
    
    public MakeMailTransferRequestBuilder WithDestinationMailContainerNumber(MailContainerNumber destinationMailContainerNumber)
    {
        _makeMailTransferRequest = _makeMailTransferRequest with { DestinationMailContainerNumber = destinationMailContainerNumber };

        return this;
    }
    
    public MakeMailTransferRequestBuilder WithNumberOfMailItems(int numberOfMailItems)
    {
        _makeMailTransferRequest = _makeMailTransferRequest with { NumberOfMailItems = numberOfMailItems };

        return this;
    }
    
    public MakeMailTransferRequestBuilder WithTransferDate(DateTime transferDate)
    {
        _makeMailTransferRequest = _makeMailTransferRequest with { TransferDate = transferDate };

        return this;
    }
    
    public MakeMailTransferRequestBuilder WithMailType(MailType mailType)
    {
        _makeMailTransferRequest = _makeMailTransferRequest with { MailType = mailType };

        return this;
    }
     
    public MakeMailTransferRequest Build()
    {
        return _makeMailTransferRequest;
    }
}