using MailContainerTest.Abstractions;
using MailContainerTest.Types;

namespace MailContainerTest.Strategies;

public sealed class LargeLetterStrategy : IMailTransferStrategy
{
    public bool IsSuccess(MailContainer? sourceContainer, MailContainer? destContainer, MakeMailTransferRequest request)
    {
        if (sourceContainer is null || destContainer is null)
        {
            return false;
        }
        
        if (!sourceContainer.AllowedMailType.HasFlag(AllowedMailType.LargeLetter) || !destContainer.AllowedMailType.HasFlag(AllowedMailType.LargeLetter))
        {
            return false;
        }
        
        if (sourceContainer.Status != MailContainerStatus.Operational || destContainer.Status != MailContainerStatus.Operational)
        {
            return false;
        }
        
        if (sourceContainer.Capacity < request.NumberOfMailItems)
        {
            return false;
        }

        return true;
    }
}