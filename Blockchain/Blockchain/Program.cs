// See https://aka.ms/new-console-template for more information

using Blockchain;

Console.Write("Starting the Transactions and the Blockchain");

for (int i = 1; i < 1000; i++)
{
    Helpers.CreateTransaction($"Fahmy{i}", $"Hassan{i}", i);
}

Console.ReadLine();
