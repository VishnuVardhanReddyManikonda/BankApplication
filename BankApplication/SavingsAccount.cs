using System;
using System.Collections.Generic;
using System.Text;

namespace BankApplication
{
    internal class SavingsAccount: Account
    {
        public decimal InterestRate { get; set; }

        public SavingsAccount(string accountNumber, string ownerName, decimal initialBalance, decimal interestRate): base(accountNumber, ownerName, initialBalance)
        {
            InterestRate = interestRate;
        }

        public override void Deposit(decimal amount)
        {
            base.Deposit(amount);
            
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

            // Compound Interest Formula: A = P(1 + r)^t
            decimal finalAmount = principal * (decimal)Math.Pow((double)(1 + rate), time);
            decimal compoundedInterest = finalAmount - principal;

            Console.WriteLine($"Fixed Deposit (compound interest) of ${principal} for {tenure} years at {InterestRate}% interest rate:");
            Console.WriteLine($"  Interest Earned: ${compoundedInterest:F2}");
            Console.WriteLine($"  Total Amount: ${finalAmount:F2}");
        }

        public override void Withdraw(decimal amount)
        {
            base.Withdraw(amount);
        }
        public override void DisplayAccountInfo()
        {
            base.DisplayAccountInfo();
        }
    }
}
