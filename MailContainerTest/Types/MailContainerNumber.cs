namespace MailContainerTest.Types;

public readonly record struct MailContainerNumber
{
    private string Value { get; }
    
    private MailContainerNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentNullException();
        }
        
        Value = value;
    }
    
    public static implicit operator string(MailContainerNumber mailContainerNumber) => mailContainerNumber.Value;
    
    public static implicit operator MailContainerNumber(string value) => new(value);
}