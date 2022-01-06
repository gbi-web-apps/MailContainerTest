using MailContainerTest.Data;
using MailContainerTest.Types;
using System.Configuration;

namespace MailContainerTest.Services
{
    public class MailTransferService : IMailTransferService
    {
        public MakeMailTransferResult MakeMailTransfer(MakeMailTransferRequest request)
        {
            var dataStoreType = ConfigurationManager.AppSettings["DataStoreType"];

            MailContainer mailContainer = null;

            if (dataStoreType == "Backup")
            {
                var mailContainerDataStore = new BackupMailContainerDataStore();
                mailContainer = mailContainerDataStore.GetMailContainer(request.SourceMailContainerNumber);

            } else
            {
                var mailContainerDataStore = new MailContainerDataStore();
                mailContainer = mailContainerDataStore.GetMailContainer(request.SourceMailContainerNumber);
            }

            var result = new MakeMailTransferResult();

            switch (request.MailType)
            {
                case MailType.StandardLetter:
                    if (mailContainer == null)
                    {
                        result.Success = false;
                    }
                    else if (!mailContainer.AllowedMailType.HasFlag(AllowedMailType.StandardLetter))
                    {
                        result.Success = false;
                    }
                    break;

                case MailType.LargeLetter:
                    if (mailContainer == null)
                    {
                        result.Success = false;
                    }
                    else if (!mailContainer.AllowedMailType.HasFlag(AllowedMailType.LargeLetter))
                    {
                        result.Success = false;
                    }
                    else if (mailContainer.Capacity < request.NumberOfMailItems)
                    {
                        result.Success = false;
                    }
                    break;

                case MailType.SmallParcel:
                    if (mailContainer == null)
                    {
                        result.Success = false;
                    }
                    else if (!mailContainer.AllowedMailType.HasFlag(AllowedMailType.SmallParcel))
                    {
                        result.Success = false;

                    }
                    else if (mailContainer.Status != MailContainerStatus.Operational)
                    {
                        result.Success = false;
                    }
                    break;
            }

            if (result.Success)
            {
                mailContainer.Capacity -= request.NumberOfMailItems;

                if (dataStoreType == "Backup")
                {
                    var mailContainerDataStore = new BackupMailContainerDataStore();
                    mailContainerDataStore.UpdateMailContainer(mailContainer);

                }
                else
                {
                    var mailContainerDataStore = new MailContainerDataStore();
                    mailContainerDataStore.UpdateMailContainer(mailContainer);
                }
            }

            return result;
        }
    }
}
