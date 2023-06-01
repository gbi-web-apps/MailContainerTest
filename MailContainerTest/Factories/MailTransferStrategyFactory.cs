using MailContainerTest.Abstractions;
using MailContainerTest.Strategies;
using MailContainerTest.Types;

namespace MailContainerTest.Factories;

public sealed class MailTransferStrategyFactory : IMailTransferStrategyFactory
{
    public IMailTransferStrategy CreateMakeMailTransferStrategy(MailType mailType)
    {
        return mailType switch {
            MailType.StandardLetter => new StandardLetterStrategy(),
            MailType.LargeLetter => new LargeLetterStrategy(),
            MailType.SmallParcel => new SmallParcelStrategy(),
            _ => throw new ArgumentOutOfRangeException(nameof(mailType), mailType, "Mail type is not in enum range")
        };
    }
}