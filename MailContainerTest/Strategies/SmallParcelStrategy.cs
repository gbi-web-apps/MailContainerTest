using MailContainerTest.Abstractions;
using MailContainerTest.Types;

namespace MailContainerTest.Strategies;

public sealed class SmallParcelStrategy : IMailTransferStrategy
{
    public bool IsSuccess(MailContainer? sourceContainer, MailContainer? destContainer, MakeMailTransferRequest request)
    {
        if (sourceContainer is null || destContainer is null)
        {
            return false;
        }
        
        if (!sourceContainer.AllowedMailType.HasFlag(AllowedMailType.SmallParcel) || !destContainer.AllowedMailType.HasFlag(AllowedMailType.SmallParcel))
        {
            return false;
        }
        
        if (sourceContainer.Status != MailContainerStatus.Operational || destContainer.Status != MailContainerStatus.Operational)
        {
            return false;
        }

        return true;
    }
}