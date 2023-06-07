using MailContainerTest.Abstractions;
using MailContainerTest.Types;

namespace MailContainerTest.Data
{
    public sealed class BackupMailContainerDataStore : IMailContainerDataStore
    {
        public static string DataStoreType => "Backup";
        
        public MailContainer GetMailContainer(in MailContainerNumber mailContainerNumber)
        {
            // Access the database and return the retrieved mail container. Implementation not required for this exercise.
            return new MailContainer();
        }

        public void UpdateMailContainer(MailContainer mailContainer)
        {
            // Update mail container in the database. Implementation not required for this exercise.
        }
    }
}
