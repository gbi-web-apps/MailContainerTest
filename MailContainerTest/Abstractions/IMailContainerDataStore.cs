using MailContainerTest.Types;

namespace MailContainerTest.Abstractions;

public interface IMailContainerDataStore
{
    MailContainer GetMailContainer(MailContainerNumber mailContainerNumber);

    void UpdateMailContainer(MailContainer mailContainer);
}