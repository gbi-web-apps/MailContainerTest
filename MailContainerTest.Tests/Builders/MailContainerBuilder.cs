using MailContainerTest.Types;

namespace MailContainerTest.Tests.Builders;

public sealed class MailContainerBuilder
{
    private MailContainerNumber _mailContainerNumber;
    private MailContainerCapacity _capacity;
    private MailContainerStatus _status;
    private AllowedMailType _allowedMailType;

    public MailContainerBuilder WithMailContainerNumber(MailContainerNumber mailContainerNumber)
    {
        _mailContainerNumber = mailContainerNumber;
        
        return this;
    }
    
    public MailContainerBuilder WithCapacity(MailContainerCapacity capacity)
    {
        _capacity = capacity;
        
        return this;
    }
    
    public MailContainerBuilder WithStatus(MailContainerStatus status)
    {
        _status = status;
        
        return this;
    }
    
    public MailContainerBuilder WithAllowedMailType(AllowedMailType allowedMailType)
    {
        _allowedMailType = allowedMailType;
        
        return this;
    }
    
    public MailContainer Build()
    {
        var mailContainer = new MailContainer
        {
            MailContainerNumber = _mailContainerNumber,
            Status = _status,
            AllowedMailType = _allowedMailType
        };
        
        mailContainer.IncreaseCapacity(_capacity);
        
        return mailContainer;
    }
}