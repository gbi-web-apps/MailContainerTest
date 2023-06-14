using MailContainerTest.Abstractions;
using MailContainerTest.Types;

namespace MailContainerTest.Strategies.Behaviours;

public sealed class AllowedMailTypeBehaviour : IAllowedMailTypeBehaviour
{
    public bool IsAllowedMailType(MailContainer sourceContainer, MailContainer destContainer)
    {
        return sourceContainer.AllowedMailType.HasFlag(AllowedMailType.LargeLetter) && destContainer.AllowedMailType.HasFlag(AllowedMailType.LargeLetter);
    }
}