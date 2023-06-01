using MailContainerTest.Abstractions;
using MailContainerTest.Data;
using MailContainerTest.Options;
using MailContainerTest.Types;
using Microsoft.Extensions.Options;

namespace MailContainerTest.Factories;

public sealed class MailContainerDataStoreFactory : IMailContainerDataStoreFactory
{
    private readonly IOptionsSnapshot<MailContainerDataStoreOptions> _options;

    public MailContainerDataStoreFactory(IOptionsSnapshot<MailContainerDataStoreOptions> options)
    {
        _options = options;
    }

    public IMailContainerDataStore CreateMailContainerDataStore()
    {
        if (_options.Value.Equals(BackupMailContainerDataStore.DataStoreType))
        {
            return new BackupMailContainerDataStore();
        }

        return new MailContainerDataStore();
    }
}