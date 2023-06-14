using MailContainerTest.Types;

namespace MailContainerTest.Abstractions;

public interface IMailContainerDataStore
{
    MailContainer GetMailContainer(in MailContainerNumber mailContainerNumber);

    void UpdateMailContainer(MailContainer mailContainer);
}