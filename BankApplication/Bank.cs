using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BankApplication
{
    internal class Bank
    {

        public List<Account> accounts = new List<Account>();
        public int nextId = 1234;

        public Account CreateAccount(string ownerName, decimal initialBalance, string type, decimal interestRate)
        {
            string accountNumber = "BBI" + nextId++.ToString();
            Account acc;
            if (type == "Savings")
            {

                acc = new SavingsAccount(accountNumber, ownerName, initialBalance, interestRate);
            }
            else
            {
                Console.WriteLine("Invalid account type. Defaulting to Normal Account.");

            }
            acc = new Account(accountNumber, ownerName, initialBalance);
            accounts.Add(acc);
            return acc;
        }

        public void DisplayAllAccounts()
        {
            foreach (var account in accounts)
            {
                account.DisplayAccountInfo();
                Console.WriteLine("-------------------------");
            }
        }

    }
}

