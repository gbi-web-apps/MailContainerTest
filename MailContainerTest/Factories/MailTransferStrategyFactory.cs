using MailContainerTest.Abstractions;
using MailContainerTest.Strategies;
using MailContainerTest.Types;

namespace MailContainerTest.Factories;

public sealed class MailTransferStrategyFactory : IMailTransferStrategyFactory
{
    private readonly IAllowedMailTypeBehaviour _allowedMailTypeBehaviour;
    private readonly IOperationalStatusBehaviour _operationalStatusBehaviour;
    private readonly ICapacityBehaviour _capacityBehaviour;

    public MailTransferStrategyFactory(IAllowedMailTypeBehaviour allowedMailTypeBehaviour,
                                       IOperationalStatusBehaviour operationalStatusBehaviour,
                                       ICapacityBehaviour capacityBehaviour)
    {
        _allowedMailTypeBehaviour = allowedMailTypeBehaviour;
        _operationalStatusBehaviour = operationalStatusBehaviour;
        _capacityBehaviour = capacityBehaviour;
    }

    public IMailTransferStrategy CreateMakeMailTransferStrategy(MailType mailType)
    {
        return mailType switch
               {
                   MailType.StandardLetter => new StandardLetterStrategy(_allowedMailTypeBehaviour),
                   MailType.LargeLetter => new LargeLetterStrategy(_allowedMailTypeBehaviour, _operationalStatusBehaviour, _capacityBehaviour),
                   MailType.SmallParcel => new SmallParcelStrategy(_allowedMailTypeBehaviour, _operationalStatusBehaviour),
                   _ => throw new ArgumentOutOfRangeException(nameof(mailType), mailType, "Mail type is not in enum range")
               };
    }
}