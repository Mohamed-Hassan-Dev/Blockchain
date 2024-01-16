namespace Blockchain
{
    public class Block(long index,
                       DateTime timeStamp,
                       string hash,
                       string previousHash,
                       long nonce,
                       List<Transaction> transactions)
    {

        public long Index { get; set; } = index;
        public DateTime TimeStamp { get; set; } = timeStamp;
        public string Hash { get; set; } = hash;
        public string PreviousHash { get; set; } = previousHash;
        public long Nonce { get; set; } = nonce;
        public List<Transaction> Transactions { get; set; } = transactions;

        public override string? ToString()
        {
            return $"\n\nIndex:{Index}\n\nTimeStamp:{TimeStamp}\n\nHash:\n{Hash}\n\nPreviousHash:\n{PreviousHash}\n\nNonce:{Nonce}\n\nTransactions:{Transactions.Count}";
        }
    }
}
