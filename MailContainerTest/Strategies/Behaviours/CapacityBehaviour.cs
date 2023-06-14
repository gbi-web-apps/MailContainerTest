using MailContainerTest.Abstractions;
using MailContainerTest.Types;

namespace MailContainerTest.Strategies.Behaviours;

public sealed class CapacityBehaviour : ICapacityBehaviour
{
    public bool IsWithinCapacity(MailContainer sourceContainer, MakeMailTransferRequest request)
    {
        return sourceContainer.Capacity < request.NumberOfMailItems;
    }
}