using MailContainerTest.Types;

namespace MailContainerTest.Abstractions;

public interface IOperationalStatusBehaviour
{
    bool IsOperationalStatus(MailContainer sourceContainer, MailContainer destContainer);
}