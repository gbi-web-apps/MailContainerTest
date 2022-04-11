using MailContainerTest.Data;
using MailContainerTest.Types;
using System.Configuration;

namespace MailContainerTest.Services
{
    public class MailTransferService : IMailTransferService
    {
        private IMailContainerDataStore dataStore;

        public MailTransferService(IMailContainerDataStore dataStore)
        {
            this.dataStore = dataStore;
        }

        public MakeMailTransferResult MakeMailTransfer(MakeMailTransferRequest request)
        {
            var  mailContainer = dataStore.GetMailContainer(request.SourceMailContainerNumber);

            var result = new MakeMailTransferResult();
            result.Success = request.MailType != mailContainer.AllowedMailType;
            if (!result.Success)
            {
                return result;
            }

            mailContainer.MailCount -= request.NumberOfMailItems;

           dataStore.UpdateMailContainer(mailContainer);

            return result;
        }
    }
}
