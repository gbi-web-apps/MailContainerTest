using MailContainerTest.Types;

namespace MailContainerTest.Abstractions;

public interface IMailTransferStrategyFactory
{
    IMailTransferStrategy CreateMakeMailTransferStrategy(MailType mailType);
}