namespace MailContainerTest.Types
{
    public class MakeMailTransferRequest
    {
        public MailContainerNumber SourceMailContainerNumber { get; set; }   
        public MailContainerNumber DestinationMailContainerNumber { get; set; }
        public int NumberOfMailItems { get; set; }
        public DateTime TransferDate { get; set; }   
        public MailType MailType { get; set; }  
    }
}
