using System;

namespace BankApplication
{
    public class Transaction
    {
        public string Type { get; set; }           
        public decimal Amount { get; set; }        
        public DateTime Date { get; set; }         
        public decimal BalanceAfter { get; set; }  
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