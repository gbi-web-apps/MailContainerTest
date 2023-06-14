using MailContainerTest.Types;

namespace MailContainerTest.Abstractions;

public interface IAllowedMailTypeBehaviour
{
    public bool IsAllowedMailType(MailContainer sourceContainer, MailContainer destContainer);
}