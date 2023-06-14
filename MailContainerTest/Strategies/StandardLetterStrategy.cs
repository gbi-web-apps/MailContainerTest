using MailContainerTest.Abstractions;
using MailContainerTest.Types;

namespace MailContainerTest.Strategies;

public sealed class StandardLetterStrategy : IMailTransferStrategy
{
    private readonly IAllowedMailTypeBehaviour _allowedMailTypeBehaviour;

    public StandardLetterStrategy(IAllowedMailTypeBehaviour allowedMailTypeBehaviour)
    {
        _allowedMailTypeBehaviour = allowedMailTypeBehaviour;
    }
    
    public bool IsSuccess(MailContainer sourceContainer, MailContainer destContainer, MakeMailTransferRequest request)
    {
        if (_allowedMailTypeBehaviour.IsAllowedMailType(sourceContainer, destContainer) is false)
        {
            return false;
        }
        
        return true;
    }
}