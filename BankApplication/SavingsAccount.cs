using System;
using System.Collections.Generic;
using System.Text;

namespace BankApplication
{
    public class SavingsAccount: Account
    {
        public decimal InterestRate { get; set; }

        public SavingsAccount(string accountNumber, string ownerName, decimal initialBalance, decimal interestRate): base(accountNumber, ownerName, initialBalance)
        {
            InterestRate = interestRate;
        }

        public override void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Deposit amount must be positive.");
            }
            var charges = 0.1m;
            Balance = Balance + amount-charges;
            TransactionHistory.Add(new Transaction("Deposit", amount, Balance));
            Console.WriteLine("0.1$ has been charged for Savings Account");
            Console.WriteLine($"Deposited {amount} to {AccountNumber}, Account Balance : {Balance}");
        }


        public void FixedDeposit(decimal amount, int tenure)
        {
            decimal principal = amount;
            decimal rate = (decimal)InterestRate / 100;
            int time = tenure;

            if (principal <= 0 || rate <= 0 || time <= 0)
            {
                throw new ArgumentException("Principal, rate, and time must be positive values.");
            }

            decimal finalAmount = principal * (decimal)Math.Pow((double)(1 + rate), time);
            decimal compoundedInterest = finalAmount - principal;

            Console.WriteLine($"Fixed Deposit (compound interest) of ${principal} for {tenure} years at {InterestRate}% interest rate:");
            Console.WriteLine($"  Interest Earned: ${compoundedInterest:F2}");
            Console.WriteLine($"  Total Amount: ${finalAmount:F2}");
        }

        public override void Withdraw(decimal amount)
        {

            if (amount <= 0)
            {
                throw new ArgumentException("U cannot withdraw negative or zero amount.");
            }
            else if (amount > Balance)
            {
                throw new InvalidOperationException("Insufficient funds for withdrawal.");
            }
            var charges = 0.1m;
            Balance = Balance - amount - charges;
            TransactionHistory.Add(new Transaction("Withdraw", amount, Balance));
            Console.WriteLine("0.1$ has been charged for Savings Account");
            Console.WriteLine($"Withdrawn {amount} from {AccountNumber}, Account Balance : {Balance}");

        }
        public override void DisplayAccountInfo()
        {
            base.DisplayAccountInfo();
        }
    }
}
