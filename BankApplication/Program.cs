using System;
using System.Collections.Generic;
using System.Text;

using System;

namespace BankApplication
{
    enum MenuOption
    {
        CreateAccount = 1,
        DisplayAllAccounts,
        Deposit,
        Withdraw,
        FixedDeposit,
        ViewTransactionHistory,
        Exit
    }




    internal class Program
    {


        static void Pause()
        {
            Console.WriteLine("\nPress any key to continue..."); 
            Console.ReadKey();
        }
        public static void Main(string[] args)
        {
            Bank bank = new Bank();
            bool running = true;

            bank.CreateAccount("Vishnu", 1000m, "Savings", 5m);

            while (running)
            {

                Console.Clear();
                Console.WriteLine("\n====== Bank Application ======");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Display All Accounts");
                Console.WriteLine("3. Deposit");
                Console.WriteLine("4. Withdraw");
                Console.WriteLine("5. Fixed Deposit");
                Console.WriteLine("6. View Transaction History");
                Console.WriteLine("7. Exit");


                Console.Write("Please select an option: ");

                string? choice = Console.ReadLine();

                int input;

                if (!int.TryParse(choice, out input) || input < 1 || input > 7)
                {
                    Console.WriteLine("Invalid option. Please try again.");
                    continue;
                }
                MenuOption option = (MenuOption)input;
                switch (option)

                {

                    case MenuOption.CreateAccount:
                        try
                        {

                            Console.Write("Enter owner name: ");
                            string ownerName = Console.ReadLine() ?? "";
                            if (!ownerName.All(char.IsLetter))
                            {
                                throw new ArgumentException("Account Owner name must contain only letters no spaces.");
                            }

                            Console.Write("Enter initial balance: ");
                            decimal initialBalance = decimal.Parse(Console.ReadLine() ?? "0");
                            if (initialBalance < 0)
                            {
                                throw new ArgumentException("Initial balance cannot be negative.");
                            }

                            Console.Write("Enter account type (Savings/Normal): ");

                            string type = (Console.ReadLine()??"").Trim().ToLower();


                            decimal interestRate = 0m;

                            if (type == "savings")
                            {
                                Console.Write("Enter interest rate: ");
                                interestRate = decimal.Parse(Console.ReadLine()??"0");
                                Console.WriteLine($"Interest rate set to {interestRate}% for Savings Account.");

                            }
                            else if (type == "normal")
                            {
                                interestRate = 0m;
                                Console.WriteLine("Normal/Current has been created");
                            }
                            else
                            {
                                throw new ArgumentException("Invalid account type. Please enter 'Savings' or 'Normal'.");
                            }


                            bank.CreateAccount(ownerName, initialBalance, type, interestRate);
                            break;

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message} , Please Enter a Valid Amount");
                            break;
                        }
                    case MenuOption.DisplayAllAccounts:

                        bank.DisplayAllAccounts();
                        break;

                    case MenuOption.Deposit:
                        try
                        {
                            Console.Write("Enter Account Number: ");
                            string? depositAcc = Console.ReadLine();

                            Account? depositAccount = bank.accounts.Find(a => a.AccountNumber == depositAcc);

                            if (depositAccount != null)
                            {
                                Console.Write("Enter Amount to Deposit: ");
                                decimal depositAmount = decimal.Parse(Console.ReadLine() ?? "0");

                                depositAccount.Deposit(depositAmount);
                            }
                            else
                            {
                                Console.WriteLine("Account not found.");
                            }

                            break;
                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine($"Error: {ex.Message}, Please enter Valid amount.");
                            break;
                        }
                    case MenuOption.Withdraw:
                        try
                        {
                            Console.Write("Enter Account Number: ");
                            string? withdrawAcc = Console.ReadLine();

                            Account? withdrawAccount = bank.accounts.Find(a => a.AccountNumber == withdrawAcc);

                            if (withdrawAccount != null)
                            {
                                Console.Write("Enter Amount to Withdraw: ");
                                decimal withdrawAmount = decimal.Parse(Console.ReadLine()??"0");

                                withdrawAccount.Withdraw(withdrawAmount);
                            }
                            else
                            {
                                Console.WriteLine("Account not found.");
                            }

                            break;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}, Please enter Valid amount.");
                            break;
                        }
                    case MenuOption.FixedDeposit:

                        try
                        {

                            Console.Write("Enter Account Number: ");
                            string? fdAcc = Console.ReadLine();

                            Account? fdAccount = bank.accounts.Find(a => a.AccountNumber == fdAcc);

                            if (fdAccount != null)
                            {
                                if (fdAccount is SavingsAccount savingsAccount)
                                {
                                    Console.Write("Enter Fixed Deposit Amount: ");
                                    decimal fdAmount = decimal.Parse(Console.ReadLine()??"0");

                                    Console.Write("Enter Tenure (Years): ");
                                    int tenure = int.Parse(Console.ReadLine()??"0");

                                    savingsAccount.FixedDeposit(fdAmount, tenure);
                                }
                                else
                                {
                                    Console.WriteLine("Fixed Deposit is available only for Savings Accounts.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Account not found.");
                            }

                            break;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message} , Please enter Valid Details.");
                            break;
                        }

                    case MenuOption.ViewTransactionHistory:

                        Console.Write("Enter Account Number: ");
                        string? historyAcc = Console.ReadLine();

                        Account? historyAccount = bank.accounts.Find(a => a.AccountNumber == historyAcc);

                        if (historyAccount != null)
                        {
                            historyAccount.DisplayTransactionHistory();
                        }
                        else
                        {
                            Console.WriteLine("Account not found.");
                        }

                        break;

                    case MenuOption.Exit:

                        Console.WriteLine("Thank you for using Bank Application!");
                        running = false;
                        break;
                        
                }
            if (running)
                {
                    Pause();
                }
            }
        }
    }
}