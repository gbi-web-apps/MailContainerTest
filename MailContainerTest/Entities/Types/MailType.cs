namespace MailContainerTest.Entities.Types
{
    [Flags]
    public enum MailType
    {
        Unspecified = 0,
        StandardLetter = 1,
        LargeLetter = 2,
        SmallParcel = 4
    }
}