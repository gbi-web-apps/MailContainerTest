using MailContainerTest.Entities;

namespace MailContainerTest.Abstractions
{
    public interface IMailContainerDataStore
    {
        public MailContainer GetMailContainer(string mailContainerNumber);

        public void UpdateMailContainer(MailContainer mailContainer);
    }
}