using MailContainerTest.Entities.Types;

namespace MailContainerTest.Entities
{
    public class MailContainer
    {
        public string MailContainerNumber { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public MailContainerStatus Status { get; set; }
        public MailType MailType { get; set; }
    }
}