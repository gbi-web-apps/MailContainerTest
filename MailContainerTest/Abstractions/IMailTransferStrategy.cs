using MailContainerTest.Types;

namespace MailContainerTest.Abstractions;

public interface IMailTransferStrategy
{
    bool IsSuccess(MailContainer? sourceContainer, MailContainer? destContainer);
}