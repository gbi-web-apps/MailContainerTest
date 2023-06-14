using MailContainerTest.Types;

namespace MailContainerTest.Abstractions;

public interface ICapacityBehaviour
{
    bool IsWithinCapacity(MailContainer sourceContainer, MakeMailTransferRequest request);
}