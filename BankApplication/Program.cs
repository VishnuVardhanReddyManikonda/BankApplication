using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BankApplication
{
    public enum MenuOption
    {
        CreateAccount = 1,
        DisplayAllAccounts,
        Deposit,
        Withdraw,
        FixedDeposit,
        ViewTransactionHistory,
        Exit
    }

    public enum MainMenu
    {
        Menu = 1,
        exit
    }


    public class Program
    {

        TerminalPrint terminalPrint = new TerminalPrint();
        public void Pause()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nPress any key to continue...");
            Console.ResetColor();
            Console.ReadKey();
        }

        public MainMenu ShowMainMenu()
        {
            Console.Clear();
            terminalPrint.PrintHeader("Welcome to the Bank Application");
            Console.WriteLine("1. Menu");
            Console.WriteLine("2. Exit");
            Console.Write("\nSelect an option: ");
            var input = Console.ReadLine();
            int option;

            if (!int.TryParse(input, out option) || option < 1 || option > 2)
            {
                terminalPrint.PrintError("Invalid option. Please try again.");
                Pause();
                return ShowMainMenu();
            }

            return (MainMenu)option;
        }

        public MenuOption ShowMenu()
        {
            Console.Clear();
            terminalPrint.PrintHeader("\n====== Bank Application ======");
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Display All Accounts");
            Console.WriteLine("3. Deposit");
            Console.WriteLine("4. Withdraw");
            Console.WriteLine("5. Fixed Deposit");
            Console.WriteLine("6. View Transaction History");
            Console.WriteLine("7. Exit to Main Menu");

            Console.Write("\nPlease select an option: ");

            string? choice = Console.ReadLine();

            int input;

            if (!int.TryParse(choice, out input) || input < 1 || input > 7)
            {
                terminalPrint.PrintError("Invalid option. Please try again.");
                Pause();
                return ShowMenu();
            }

            return (MenuOption)input;
        }

        public static void Main(string[] args)
        {
            Program program = new Program();
            Bank bank = new Bank();
            bool running = true;

            bank.CreateAccount("Vishnu", 1000m, "Savings", 5m);

            while (running)
            {
                MainMenu mainMenu = program.ShowMainMenu();

                switch (mainMenu)
                {
                    case MainMenu.Menu:
                        bool inMain = true;

                        while (inMain)
                        {
                            MenuOption option = program.ShowMenu();
                            switch (option)
                            {
                                case MenuOption.CreateAccount:
                                    try
                                    {
                                        Console.Write("Enter owner name: ");
                                        string ownerName = Console.ReadLine() ?? "";
                                        if (!ownerName.All(char.IsLetter))
                                        {
                                            throw new ArgumentException("Account Owner name must contain only letters no spaces. Since Owner name will be a part of Account Number.");
                                        }

                                        Console.Write("Enter initial balance: ");
                                        decimal initialBalance = decimal.Parse(Console.ReadLine() ?? "0");
                                        if (initialBalance < 0)
                                        {
                                            throw new ArgumentException("Initial balance cannot be negative.");
                                        }

                                        Console.Write("Enter account type (Savings/Normal): ");

                                        string type = (Console.ReadLine() ?? "").Trim().ToLower();

                                        decimal interestRate = 0m;

                                        if (type == "savings")
                                        {
                                            Console.Write("Enter interest rate: ");
                                            interestRate = decimal.Parse(Console.ReadLine() ?? "0");
                                            program.terminalPrint.PrintSuccess($"Interest rate set to {interestRate}% for Savings Account.");
                                        }
                                        else if (type == "normal")
                                        {
                                            interestRate = 0m;
                                            program.terminalPrint.PrintSuccess("Normal/Current Account has been created");
                                        }
                                        else
                                        {
                                            throw new ArgumentException("Invalid account type. Please enter 'Savings' or 'Normal'.");
                                        }

                                        Account account = bank.CreateAccount(ownerName, initialBalance, type, interestRate);

                                        program.terminalPrint.PrintSuccess($"Account Details : \n Account Number : {account.AccountNumber}\n Owner Name :{account.OwnerName}\n Balance : {account.Balance}$ ");
                                        program.Pause();
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        program.terminalPrint.PrintError($"Error: {ex.Message}");
                                        program.Pause();
                                        break;
                                    }

                                case MenuOption.DisplayAllAccounts:
                                    bank.DisplayAllAccounts();
                                    program.Pause();
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
                                            program.terminalPrint.PrintSuccess("Deposit successful!");
                                        }
                                        else
                                        {
                                            program.terminalPrint.PrintError("Account not found.");
                                        }

                                        program.Pause();
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        program.terminalPrint.PrintError($"Error: {ex.Message}, Please enter Valid amount.");
                                        program.Pause();
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
                                            decimal withdrawAmount = decimal.Parse(Console.ReadLine() ?? "0");

                                            withdrawAccount.Withdraw(withdrawAmount);
                                            program.terminalPrint.PrintSuccess("Withdrawal successful!");
                                        }
                                        else
                                        {
                                            program.terminalPrint.PrintError(" Account not found.");
                                        }

                                        program.Pause();
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        program.terminalPrint.PrintError($"Error: {ex.Message}, Please enter Valid amount.");
                                        program.Pause();
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
                                                decimal fdAmount = decimal.Parse(Console.ReadLine() ?? "0");

                                                Console.Write("Enter Tenure (Years): ");
                                                int tenure = int.Parse(Console.ReadLine() ?? "0");

                                                savingsAccount.FixedDeposit(fdAmount, tenure);
                                                program.terminalPrint.PrintSuccess("Fixed Deposit processed!");
                                            }
                                            else
                                            {
                                                program.terminalPrint.PrintWarning("Fixed Deposit is available only for Savings Accounts.");
                                            }
                                        }
                                        else
                                        {
                                            program.terminalPrint.PrintError("Account not found.");
                                        }

                                        program.Pause();
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        program.terminalPrint.PrintError($"Error: {ex.Message} , Please enter Valid Details.");
                                        program.Pause();
                                        break;
                                    }

                                case MenuOption.ViewTransactionHistory:
                                    try
                                    {
                                        Console.Write("Enter Account Number: ");
                                        var historyAcc = Console.ReadLine();

                                        var historyAccount = bank.accounts.FirstOrDefault(a => a.AccountNumber == historyAcc);

                                        if (historyAccount != null)
                                        {
                                            historyAccount.DisplayTransactionHistory();
                                        }
                                        else
                                        {
                                            program.terminalPrint.PrintError("Account not found.");
                                        }

                                        program.Pause();
                                    }
                                    catch (Exception ex)
                                    {
                                        program.terminalPrint.PrintError($"Error: {ex.Message}");
                                        program.Pause();
                                    }
                                    break;

                                case MenuOption.Exit:
                                    program.terminalPrint.PrintSuccess("Returning to Main Menu...");
                                    inMain = false;
                                    break;
                            }
                        }
                        break;

                    case MainMenu.exit:
                        program.terminalPrint.PrintSuccess("Thank you for using Bank Application!");
                        running = false;
                        break;
                }
            }
        }
    }
}