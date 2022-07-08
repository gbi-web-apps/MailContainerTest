using MailContainerTest.Entities.Types;

namespace MailContainerTest.Entities
{
    public class MakeMailTransferRequest
    {
        public string SourceMailContainerNumber { get; set; } = string.Empty;
        public string DestinationMailContainerNumber { get; set; } = string.Empty;
        public int NumberOfMailItems { get; set; }
        public DateTime TransferDate { get; set; }
        public MailType MailType { get; set; }
    }
}