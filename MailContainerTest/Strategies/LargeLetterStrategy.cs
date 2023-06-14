using MailContainerTest.Abstractions;
using MailContainerTest.Types;

namespace MailContainerTest.Strategies;

public sealed class LargeLetterStrategy : IMailTransferStrategy
{
    private readonly IAllowedMailTypeBehaviour _allowedMailTypeBehaviour;
    private readonly IOperationalStatusBehaviour _operationalStatusBehaviour;
    private readonly ICapacityBehaviour _capacityBehaviour;

    public LargeLetterStrategy(IAllowedMailTypeBehaviour allowedMailTypeBehaviour,
                               IOperationalStatusBehaviour operationalStatusBehaviour,
                               ICapacityBehaviour capacityBehaviour)
    {
        _allowedMailTypeBehaviour = allowedMailTypeBehaviour;
        _operationalStatusBehaviour = operationalStatusBehaviour;
        _capacityBehaviour = capacityBehaviour;
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

        if (_capacityBehaviour.IsWithinCapacity(sourceContainer, request) is false)
        {
            return false;
        }
        
        // Unit tests for member data (also commented out in MailContainerTest.Tests\LargeLetterStrategyTests.cs):
        // if (!sourceContainer.AllowedMailType.HasFlag(AllowedMailType.LargeLetter) || !destContainer.AllowedMailType.HasFlag(AllowedMailType.LargeLetter))
        // {
        //     return false;
        // }
        //
        // if (sourceContainer.Status != MailContainerStatus.Operational || destContainer.Status != MailContainerStatus.Operational)
        // {
        //     return false;
        // }
        //
        // if (sourceContainer.Capacity < request.NumberOfMailItems)
        // {
        //     return false;
        // }

        return true;
    }
}