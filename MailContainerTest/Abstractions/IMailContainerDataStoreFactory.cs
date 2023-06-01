using MailContainerTest.Types;

namespace MailContainerTest.Abstractions;

public interface IMailContainerDataStoreFactory
{
    IMailContainerDataStore CreateMailContainerDataStore(MailContainerNumber mailContainerNumber);
}