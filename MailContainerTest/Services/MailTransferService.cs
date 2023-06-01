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
            var sourceMailContainerDataStore = _mailContainerDataStoreFactory.CreateMailContainerDataStore();
            var sourceMailContainer = sourceMailContainerDataStore.GetMailContainer(request.SourceMailContainerNumber);

            var destMailContainerDataStore = _mailContainerDataStoreFactory.CreateMailContainerDataStore();
            var destMailContainer = destMailContainerDataStore.GetMailContainer(request.DestinationMailContainerNumber);

            var result = new MakeMailTransferResult
                         {
                             Success = _mailTransferStrategyFactory.CreateMakeMailTransferStrategy(request.MailType)
                                                                   .IsSuccess(sourceMailContainer, destMailContainer, request)
                         };

            if (result.Success)
            {
                try
                {
                    sourceMailContainer.Capacity -= request.NumberOfMailItems;
                    destMailContainer.Capacity += request.NumberOfMailItems;

                    sourceMailContainerDataStore.UpdateMailContainer(sourceMailContainer);
                    destMailContainerDataStore.UpdateMailContainer(destMailContainer);

                    _unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    result.Success = false;

                    _unitOfWork.Rollback();

                    _loggerAdapter.LogError(ex, "Error saving changes to containers");
                }
            }

            return result;
        }
    }
}