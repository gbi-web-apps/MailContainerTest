using MailContainerTest.Abstractions;
using MailContainerTest.Types;

namespace MailContainerTest.Strategies;

public sealed class SmallParcelStrategy : IMailTransferStrategy
{
    private readonly IAllowedMailTypeBehaviour _allowedMailTypeBehaviour;
    private readonly IOperationalStatusBehaviour _operationalStatusBehaviour;

    public SmallParcelStrategy(IAllowedMailTypeBehaviour allowedMailTypeBehaviour,
                               IOperationalStatusBehaviour operationalStatusBehaviour)
    {
        _allowedMailTypeBehaviour = allowedMailTypeBehaviour;
        _operationalStatusBehaviour = operationalStatusBehaviour;
    }
    
    public bool IsSuccess(MailContainer sourceContainer, MailContainer destContainer, MakeMailTransferRequest request)
    {
        if (_allowedMailTypeBehaviour.IsAllowedMailType(sourceContainer, destContainer) is false)
        {
            return false;
        }

        if (_operationalStatusBehaviour.IsOperationalStatus(sourceContainer, destContainer) is false)
        {
            return false;
        }

        return true;
    }
}