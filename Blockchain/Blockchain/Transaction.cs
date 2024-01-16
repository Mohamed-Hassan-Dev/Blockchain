namespace Blockchain
{
    public class Transaction(string from, string to, int amount)
    {
        public Guid TransactionID { get; init; } = Guid.NewGuid();
        public string From { get; init; } = from;
        public string To { get; init; } = to;
        public int Amount { get; init; } = amount;

        public override string? ToString()
        {
            return $"\nID: {TransactionID}\n From:{From}\n To:{To}\n Amount:{Amount}";
        }
    }
}
