using MailContainerTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailContainerTest.Data
{
    public abstract class MailContainerDataStoreBase : IMailContainerDataStore
    {
        protected List<MailContainer> Containers { get; }
        public MailContainerDataStoreBase()
        {
            Containers = new List<MailContainer>();
        }

        public MailContainer GetMailContainer(string mailContainerNumber, MailType allowedMailType, MailContainerStatus initialStatus)
        {
            // Access the database and return the retrieved mail container. Implementation not required for this exercise.
            return new MailContainer(mailContainerNumber, initialStatus, allowedMailType);
        }

        public virtual void UpdateMailContainer(MailContainer mailContainer)
        {
            // Update mail container in the database. Implementation not required for this exercise.
        }

        public virtual MailContainer GetMailContainer(string mailContainerNumber)
        {
            throw new NotImplementedException();
        }
    }
}
