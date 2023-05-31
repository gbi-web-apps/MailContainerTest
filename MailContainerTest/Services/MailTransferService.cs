using MailContainerTest.Data;
using MailContainerTest.DependencyInjection;
using MailContainerTest.Types;
using System.Configuration;

namespace MailContainerTest.Services
{
    public class MailTransferService : IMailTransferService
    {
        private readonly IMailContainerDataStore mailContainerDataStore;

        public MailTransferService(IMailContainerDataStore mailContainerDataStore)
        {
            this.mailContainerDataStore = mailContainerDataStore;
        }

        public MakeMailTransferResult MakeMailTransfer(MakeMailTransferRequest request)
        {
            var mailContainer = mailContainerDataStore.GetMailContainer(request.SourceMailContainerNumber);

            MakeMailTransferResult mailContainerValidated = ValidateTransferRequestToContiner(mailContainer, request);

            if (mailContainerValidated.Success)
            {
                mailContainer.Capacity -= request.NumberOfMailItems;
                mailContainerDataStore.UpdateMailContainer(mailContainer);
            }

            return mailContainerValidated;
        }

        private MakeMailTransferResult ValidateTransferRequestToContiner(MailContainer mailContainer, MakeMailTransferRequest request)
        {
            var result = new MakeMailTransferResult();
            result.Success = true;
            switch (request.MailType)
            {
                case MailType.StandardLetter:
                    if (mailContainer == null || !mailContainer.AllowedMailType.HasFlag(AllowedMailType.StandardLetter))
                    {
                        result.Success = false;
                    }
                    break;

                case MailType.LargeLetter:
                    if (mailContainer == null || !mailContainer.AllowedMailType.HasFlag(AllowedMailType.LargeLetter) ||
                        mailContainer.Capacity < request.NumberOfMailItems)
                    {
                        result.Success = false;
                    }
                    break;

                case MailType.SmallParcel:
                    if (mailContainer == null || !mailContainer.AllowedMailType.HasFlag(AllowedMailType.SmallParcel) ||
                        mailContainer.Status != MailContainerStatus.Operational)
                    {
                        result.Success = false;
                    }
                    break;
            }
            return result;
        }
    }
}
