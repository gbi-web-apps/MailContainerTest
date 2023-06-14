using MailContainerTest.Data;
using MailContainerTest.Types;
using System.Configuration;
using MailContainerTest.Abstractions;
using Microsoft.Extensions.Logging;

namespace MailContainerTest.Services
{
    public sealed class MailTransferService : IMailTransferService
    {
        private readonly IMailContainerDataStoreFactory _mailContainerDataStoreFactory;
        private readonly IMailTransferStrategyFactory _mailTransferStrategyFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerAdapter<IMailTransferService> _loggerAdapter;

        public MailTransferService(IMailContainerDataStoreFactory mailContainerDataStoreFactory,
                                   IMailTransferStrategyFactory mailTransferStrategyFactory,
                                   IUnitOfWork unitOfWork,
                                   ILoggerAdapter<IMailTransferService> loggerAdapter)
        {
            _mailContainerDataStoreFactory = mailContainerDataStoreFactory;
            _mailTransferStrategyFactory = mailTransferStrategyFactory;
            _unitOfWork = unitOfWork;
            _loggerAdapter = loggerAdapter;
        }

        public MakeMailTransferResult MakeMailTransfer(MakeMailTransferRequest request)
        {
            var containerDataStore = _mailContainerDataStoreFactory.CreateMailContainerDataStore();

            var sourceMailContainer = containerDataStore.GetMailContainer(request.SourceMailContainerNumber);
            var destMailContainer = containerDataStore.GetMailContainer(request.DestinationMailContainerNumber);

            var mailTransfer = new MakeMailTransferResult
                               {
                                   Success = _mailTransferStrategyFactory.CreateMakeMailTransferStrategy(request.MailType)
                                                                         .IsSuccess(sourceMailContainer, destMailContainer, request)
                               };

            if (mailTransfer.Success)
            {
                ApplyMailContainerChanges(request, sourceMailContainer, destMailContainer, containerDataStore, mailTransfer);
            }

            return mailTransfer;
        }

        private void ApplyMailContainerChanges(MakeMailTransferRequest request, MailContainer sourceMailContainer, MailContainer destMailContainer, IMailContainerDataStore containerDataStore, MakeMailTransferResult result)
        {
            try
            {
                sourceMailContainer.DecreaseCapacity(request.NumberOfMailItems);
                destMailContainer.IncreaseCapacity(request.NumberOfMailItems);

                containerDataStore.UpdateMailContainer(sourceMailContainer);
                containerDataStore.UpdateMailContainer(destMailContainer);

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                result.Success = false;

                _unitOfWork.Rollback();

                _loggerAdapter.LogError(ex, "Error saving changes to containers");
            }
        }
    }
}