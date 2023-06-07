namespace MailContainerTest.Types
{
    public record MakeMailTransferRequest
    {
        public MailContainerNumber SourceMailContainerNumber { get; init; }   
        public MailContainerNumber DestinationMailContainerNumber { get; init; }
        public int NumberOfMailItems { get; init; }
        public DateTime TransferDate { get; init; }   
        public MailType MailType { get; init; }  
    }
}
