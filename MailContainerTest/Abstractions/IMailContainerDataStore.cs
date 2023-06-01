using MailContainerTest.Types;

namespace MailContainerTest.Abstractions;

public interface IMailContainerDataStore
{
    MailContainer GetMailContainer(string mailContainerNumber);

    void UpdateMailContainer(MailContainer mailContainer);
}