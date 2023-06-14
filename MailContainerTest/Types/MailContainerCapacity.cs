namespace MailContainerTest.Types;

public readonly record struct MailContainerCapacity
{
    private int Value { get; }

    private MailContainerCapacity(int value)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), value, "Capacity can never be negative. (overflow)");
        }

        Value = value;
    }

    public static implicit operator int(MailContainerCapacity mailContainerCapacity) => mailContainerCapacity.Value;

    public static implicit operator MailContainerCapacity(int value) => new(value);
}