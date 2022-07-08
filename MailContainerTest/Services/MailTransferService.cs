using MailContainerTest.Abstractions;
using MailContainerTest.Entities;

namespace MailContainerTest.Services
{
    public class MailTransferService : IMailTransferService
    {
        private readonly IMailContainerDataStoreFactory mailContainerDataStoreFactory;

        public MailTransferService(IMailContainerDataStoreFactory mailContainerDataStoreFactory)
        {
            this.mailContainerDataStoreFactory = mailContainerDataStoreFactory;
        }

        public MakeMailTransferResult MakeMailTransfer(MakeMailTransferRequest request)
        {
            var mailContainerDataStore = mailContainerDataStoreFactory.CreateMailContainerDataStore();

            var mailContainer = mailContainerDataStore.GetMailContainer(request.SourceMailContainerNumber);

            var result = new MakeMailTransferResult();
            result.Success = request.MailType.HasFlag(mailContainer.MailType);

            if (result.Success)
            {
                mailContainerDataStore.UpdateMailContainer(mailContainer);
            }

            return result;
        }
    }
}