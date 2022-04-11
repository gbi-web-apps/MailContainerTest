namespace MailContainerTest.Types
{
    public class MailContainer 
    {
        public MailContainer(string mailContainerNumber, MailContainerStatus status, MailType allowedMailType)
        {
            MailContainerNumber = mailContainerNumber;
            Status = status;
            AllowedMailType = allowedMailType;
        }

        public string MailContainerNumber { get; }
        public MailContainerStatus Status { get; set; }
        public MailType AllowedMailType { get; set; }
        public int MailCount { get; set; }

    }
}
