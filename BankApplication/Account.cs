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

        public Account(string accountNumber, string ownerName, decimal initialBalance)
        {
            AccountNumber = accountNumber;
            OwnerName = ownerName;
            Balance = initialBalance;
        }

        public virtual void Deposit (decimal amount)
        {
            if (amount <=0)
            {
                throw new ArgumentException("Deposit amount must be positive.");
                //Console.WriteLine("Deposit amount must be positive.");
            }
            Balance = Balance + amount;
            Console.WriteLine($"Deposited {amount} ti {AccountNumber}, Account Balance : {Balance}");
        }

        public virtual void Withdraw(decimal amount)
        {
            if (amount <=0)
            {
                throw new ArgumentException("U cannot withdraw negative or zero amount.");
                
            }
            else if (amount > Balance)
            {
                throw new InvalidOperationException("Insufficient funds for withdrawal.");
                Console.WriteLine($"Insufficient funds. Current Balance: {Balance}");

            }
            Balance = Balance - amount;
            Console.WriteLine($"Withdrawn {amount} from {AccountNumber}, Account Balance : {Balance}");
        }

        public virtual void DisplayAccountInfo()
        {
            Console.WriteLine($"Account Number: {AccountNumber}");
            Console.WriteLine($"Owner Name: {OwnerName}");
            Console.WriteLine($"Balance: {Balance}");
        }

    }
}
