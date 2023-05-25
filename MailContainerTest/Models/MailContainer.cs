namespace MailContainerTest.Types
{
    public class MailContainer : BaseModel
    {
        public string MailContainerNumber { get; set; } 
        public int Capacity { get; set; }   
        public MailContainerStatus Status { get; set; }
        public AllowedMailType AllowedMailType { get; set; }
    }
}