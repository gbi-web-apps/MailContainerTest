using MailContainerTest.Abstractions;
using MailContainerTest.Entities;
using Microsoft.Extensions.Options;

namespace MailContainerTest.Data
{
    public class MailContainterDataStoreFactory : IMailContainerDataStoreFactory
    {
        private readonly DataStoreTypeOptions dataStoreTypeOptions;

        public MailContainterDataStoreFactory(IOptions<DataStoreTypeOptions> dataStoreTypeOptions)
        {
            this.dataStoreTypeOptions = dataStoreTypeOptions.Value;
        }

        public IMailContainerDataStore CreateMailContainerDataStore()
        {
            return
                this.dataStoreTypeOptions.DataStoreType == DataStoreTypeOptions.BackupMailName
                ? new BackupMailContainerDataStore()
                : new MailContainerDataStore();
        }
    }
}