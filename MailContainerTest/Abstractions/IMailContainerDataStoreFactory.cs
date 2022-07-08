namespace MailContainerTest.Abstractions
{
    public interface IMailContainerDataStoreFactory
    {
        IMailContainerDataStore CreateMailContainerDataStore();
    }
}