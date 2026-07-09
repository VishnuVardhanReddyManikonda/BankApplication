using System;
using System.Collections.Generic;
using System.Text;

namespace BankApplication
{
    internal class Program
    {

        public static void Main(string[] args)
        {
            Bank bank = new Bank();

            bool running = true;
            bank.CreateAccount("Vishnu", 1000m, "Savings", 5m);

            while (running)
            {

                Console.WriteLine("Welcome to the Bank Application!");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Display All Accounts");
                Console.WriteLine("3. Deposit");
                Console.WriteLine("4. Withdraw");
                Console.WriteLine("5. FixedDeposit");
                Console.WriteLine("6. View Transaction History");
                Console.WriteLine("7. Exit");
                Console.Write("Please select an option: ");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Console.Write("Enter owner name: ");
                        string ownerName = Console.ReadLine();
                        Console.Write("Enter initial balance: ");
                        decimal initialBalance = decimal.Parse(Console.ReadLine());
                        Console.Write("Enter account type (Savings/Normal): ");
                        string type = Console.ReadLine();
                        decimal interestRate = 0m;
                        if (type.ToLower() == "savings")
                        {
                            Console.Write("Enter interest rate: ");
                            interestRate = decimal.Parse(Console.ReadLine());
                        }
                        bank.CreateAccount(ownerName, initialBalance, type, interestRate);
                        break;
                    case "2":
                        bank.DisplayAllAccounts();
                        break;

                    case "3":
                        Console.WriteLine("Enter Account Number: ");
                        string depositacc = Console.ReadLine();

                        Account account = bank.accounts.Find(a => a.AccountNumber == depositacc);

                        if (account != null)
                        {
                            Console.WriteLine("Enter Amount to Deposit: ");
                            decimal depositAmount = decimal.Parse(Console.ReadLine());

                            account.Deposit(depositAmount);
                        }
                        else
                        {
                            Console.WriteLine("Account not found.");
                        }
                        break;
                    case "4":
                        Console.WriteLine("Enter Account Number: ");
                        string withdrawacc = Console.ReadLine();
                        Account withdrawAccount = bank.accounts.Find(a => a.AccountNumber == withdrawacc);
                        if (withdrawAccount != null)
                        {
                            Console.WriteLine("Enter Amount to Withdraw: ");
                            decimal withdrawAmount = decimal.Parse(Console.ReadLine());
                            withdrawAccount.Withdraw(withdrawAmount);
                        }
                        else
                        {
                            Console.WriteLine("Account not found.");
                        }
                        break;
                    case "5":
                        Console.WriteLine("Enter Account Number: ");
                        string fdacc = Console.ReadLine();

                        Account fdAccount = bank.accounts.Find(a => a.AccountNumber == fdacc);

                        if (fdAccount != null)
                        {
                            if (fdAccount is SavingsAccount savingsAccount)
                            {
                                Console.WriteLine("Enter Amount to Deposit in Fixed Deposit: ");
                                decimal fdAmount = decimal.Parse(Console.ReadLine());

                                Console.WriteLine("Enter Tenure (Years): ");
                                int tenure = int.Parse(Console.ReadLine());

                                savingsAccount.FixedDeposit(fdAmount, tenure);
                            }
                            else
                            {
                                Console.WriteLine("Fixed Deposit is only available for Savings Accounts.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Account not found.");
                        }
                        break;


                    case "6":
                        Console.WriteLine("Enter Account Number: ");
                        string historyacc = Console.ReadLine();
                        Account historyAccount = bank.accounts.Find(a => a.AccountNumber == historyacc);
                        if (historyAccount != null)
                        {
                            historyAccount.DisplayTransactionHistory();
                        }
                        else
                        {
                            Console.WriteLine("Account not found.");
                        }
                        break;


                    case "7":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}