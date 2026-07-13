using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BankApplication
{
    internal class Account
    {
        public string AccountNumber { get; set; }
        public string OwnerName { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> TransactionHistory { get; set; } = new List<Transaction>();

        public Account(string accountNumber, string ownerName, decimal initialBalance)
        {
            AccountNumber = accountNumber;
            OwnerName = ownerName;
            Balance = initialBalance;
        }

        public virtual void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Deposit amount must be positive.");
            }
            Balance = Balance + amount;
            TransactionHistory.Add(new Transaction("Deposit", amount, Balance));
            Console.WriteLine($"Deposited {amount} to {AccountNumber}, Account Balance : {Balance}");
        }

        public virtual void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("U cannot withdraw negative or zero amount.");
            }
            else if (amount > Balance)
            {
                throw new InvalidOperationException("Insufficient funds for withdrawal.");
            }
            Balance = Balance - amount;
            TransactionHistory.Add(new Transaction("Withdraw", amount, Balance));
            Console.WriteLine($"Withdrawn {amount} from {AccountNumber}, Account Balance : {Balance}");
        }

        public virtual void DisplayAccountInfo()
        {
            Console.WriteLine($"Account Number: {AccountNumber}");
            Console.WriteLine($"Owner Name: {OwnerName}");
            Console.WriteLine($"Balance: {Balance}");
        }

        public void DisplayTransactionHistory()
        {
            Console.WriteLine($"\n========== Transaction History for {AccountNumber} ==========");
            if (TransactionHistory.Count == 0)
            {
                Console.WriteLine("No transactions yet.");
            }
            else
            {
                Console.WriteLine("Date & Time           | Type       | Amount      | Balance");
                Console.WriteLine("-----------------------------------------------------");
                foreach (var transaction in TransactionHistory)
                {
                    transaction.DisplayTransaction();
                }
            }
            Console.WriteLine("======================================================\n");
        }
    }
}