namespace MailContainerTest.Types
{
    public class MakeMailTransferRequest
    {
        public string SourceMailContainerNumber { get; set; }   
        public string DestinationMailContainerNumber { get; set; }
        public int NumberOfMailItems { get; set; }
        public DateTime TransferDate { get; set; }   
        public MailType MailType { get; set; }  
    }
}
