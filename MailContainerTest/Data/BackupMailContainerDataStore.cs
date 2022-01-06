using MailContainerTest.Types;

namespace MailContainerTest.Data
{
    public class BackupMailContainerDataStore
    {
        public MailContainer GetMailContainer(string mailContainerNumber)
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
