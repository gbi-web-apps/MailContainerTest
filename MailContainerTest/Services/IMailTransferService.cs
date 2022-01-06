using MailContainerTest.Types;

namespace MailContainerTest.Services
{
    public interface IMailTransferService
    {
        MakeMailTransferResult MakeMailTransfer(MakeMailTransferRequest request);
    }
}