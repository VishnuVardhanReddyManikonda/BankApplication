using System;

namespace BankApplication
{
    internal class Transaction
    {
        public string Type { get; set; }           // "Deposit" or "Withdraw"
        public decimal Amount { get; set; }        // Amount transacted
        public DateTime Date { get; set; }         // When it happened
        public decimal BalanceAfter { get; set; }  // Balance after transaction

        public Transaction(string type, decimal amount, decimal balanceAfter)
        {
            Type = type;
            Amount = amount;
            Date = DateTime.Now;
            BalanceAfter = balanceAfter;
        }

        public void DisplayTransaction()
        {
            Console.WriteLine($"{Date:dd/MM/yyyy HH:mm:ss} | {Type,-10} | Amount: ${Amount:F2} | Balance: ${BalanceAfter:F2}");
        }
    }
}