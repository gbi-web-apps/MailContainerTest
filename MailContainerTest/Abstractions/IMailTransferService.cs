using MailContainerTest.Entities;

namespace MailContainerTest.Abstractions
{
    public interface IMailTransferService
    {
        MakeMailTransferResult MakeMailTransfer(MakeMailTransferRequest request);
    }
}