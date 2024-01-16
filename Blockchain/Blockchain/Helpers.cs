using System.Security.Cryptography;
using System.Text;

namespace Blockchain
{
    public static class Helpers
    {
        private static List<Transaction> transactions = [];
        private static List<Block> blockChain = [];
        private static string GenesisHash = "Thank you for This Technology it's a very wonderfull contribution";
        private static long Indexposition = 0;


        //Public Function to Create Transaction
        public static Transaction CreateTransaction(string from, string to, int amount)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

            //instead of calc the nonce
            Thread.Sleep(1000);

            if (from is null || string.IsNullOrEmpty(from) || string.IsNullOrWhiteSpace(from))
                throw new ArgumentNullException(nameof(from));

            if (to is null || string.IsNullOrEmpty(to) || string.IsNullOrWhiteSpace(to))
                throw new ArgumentNullException(nameof(to));

            if (amount <= 0)
                throw new ArgumentException(nameof(amount));

            var transaction = new Transaction(from, to, amount);
            AddTransactiontoBlock(transaction);
            return transaction;
        }

        //adding Transaction to block if the transactions count is 10 - mean the block will be created if the transaction reach 10
        private static void AddTransactiontoBlock(Transaction transaction)
        {
            if (transactions.Count < 10)
            {
                transactions.Add(transaction);
            }
            else
            {
                PrintAllTransactions(transactions);
                CreateBlock(transactions);
                transactions.Clear();
                transactions.Add(transaction);
            }
        }

        //create the block in the blockchain
        private static void CreateBlock(List<Transaction> transactions)
        {
            var blockHash = new StringBuilder();

            foreach (var transaction in transactions)
                blockHash.Append(CreateHash(transaction.TransactionID + transaction.From + transaction.To + transaction.Amount.ToString()));

            AddBlockToChain(new Block(index: CreateIndex(), timeStamp: GetDateTime(), hash: blockHash.ToString(), previousHash: CreateHash(GenesisHash), nonce: 1, transactions));
        }

        //create index for the block
        private static long CreateIndex() => Indexposition++;

        //add the block to the blockchain 
        private static void AddBlockToChain(Block block)
        {
            //instead of calc the nonce            
            Thread.Sleep((int)block.Nonce * 1000);

            if (block is null || block == null)
                throw new ArgumentNullException(nameof(block));

            if (blockChain is not null && blockChain != null && blockChain.Count >= 1)
                block.PreviousHash = blockChain.LastOrDefault().Hash;

            blockChain.Add(block);
            ValidationForBlocks(blockChain);
            PrintAllBlocks(blockChain);
        }

        //Validate the block
        private static void ValidationForBlock(Block block)
        {
            if (block is null || block == null)
                throw new ArgumentNullException(nameof(block));

            if (block.Index == 0)
            {
                if (block.PreviousHash == CreateHash(GenesisHash))
                {
                    PrintAllBlockValidation(block, true);
                    return;
                }
            }
            else
            {
                var previousBlock = blockChain.Find(x => x.Index == block.Index - 1);

                if (previousBlock != null || previousBlock is not null)
                {
                    if (previousBlock.Hash == block.PreviousHash)
                    {
                        PrintAllBlockValidation(block, true);
                        return;
                    }
                }
            }

            PrintAllBlockValidation(block, false);
        }

        //validate the blockchain 
        private static void ValidationForBlocks(List<Block> blocks)
        {
            if (blocks is null || blocks == null)
                throw new ArgumentNullException(nameof(blocks));

            //Delay for 1 sec
            Thread.Sleep(1000);

            foreach (var item in blocks)
                ValidationForBlock(item);

        }

        //print the Transactions
        private static void PrintAllTransactions(List<Transaction> transactions)
        {
            Console.Write($"\n-----------------------------------------------------\n");
            foreach (var transaction in transactions)
            {
                Console.Write($"\n Transactions");
                Console.Write($"\n{transaction}");

                //Delay for 1 sec
                Thread.Sleep(1000);
            }
        }

        //print the blocks
        private static void PrintAllBlocks(List<Block> blocks)
        {
            Console.Write($"\n-----------------------------------------------------\n");
            Console.Write($"\n BlockChain");
            Console.Write($"\n{blocks.LastOrDefault()}");
        }

        //print the the blocks validation messages
        private static void PrintAllBlockValidation(Block block, bool status)
        {
            Console.Write($"\n-----------------------------------------------------\n");

            if (status == true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"Index = {block.Index}");
                Console.Write($" Block Chain is Valid\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"\n Block Chain is InValid");
                Console.Write(block);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        //get the Date time as timestamp server
        private static DateTime GetDateTime()
        {
            return DateTime.Now;
        }

        //Create the Hash from all the Transactions
        private static string CreateHash(string inputString)
        {
            SHA512 sha512 = SHA512.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hashs = sha512.ComputeHash(bytes);

            string hashString = string.Empty;

            foreach (byte item in hashs)
                hashString += string.Format("{0:x2}", item);

            return hashString;
        }
    }
}
