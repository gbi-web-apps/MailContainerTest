namespace MailContainerTest.Types
{
    public sealed class MailContainer
    {
        public MailContainerNumber MailContainerNumber { get; init; }
        public MailContainerCapacity Capacity { get; private set; }
        public MailContainerStatus Status { get; init; }
        public AllowedMailType AllowedMailType { get; init; }

        public void IncreaseCapacity(int numberOfMailItems)
        {
            if (numberOfMailItems < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfMailItems),
                                                      numberOfMailItems,
                                                      $"{nameof(numberOfMailItems)} must be a positive number higher than 0.");
            }

            Capacity += numberOfMailItems;
        }

        public void DecreaseCapacity(int numberOfMailItems)
        {
            if (numberOfMailItems < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfMailItems),
                                                      numberOfMailItems,
                                                      $"{nameof(numberOfMailItems)} must be a positive number higher than 0.");
            }

            Capacity -= numberOfMailItems;
        }
    }
}