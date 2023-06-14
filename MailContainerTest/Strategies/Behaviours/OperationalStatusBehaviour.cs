using MailContainerTest.Abstractions;
using MailContainerTest.Types;

namespace MailContainerTest.Strategies.Behaviours;

public sealed class OperationalStatusBehaviour : IOperationalStatusBehaviour
{
    public bool IsOperationalStatus(MailContainer sourceContainer, MailContainer destContainer)
    {
        return sourceContainer.Status == MailContainerStatus.Operational && destContainer.Status == MailContainerStatus.Operational;
    }
}