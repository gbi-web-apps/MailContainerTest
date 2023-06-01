using MailContainerTest.Types;

namespace MailContainerTest.Abstractions
{
    public interface IMailTransferService
    {
        MakeMailTransferResult MakeMailTransfer(MakeMailTransferRequest request);
    }
}