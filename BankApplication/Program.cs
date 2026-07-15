using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BankApplication
{

    public class Program
    {

        TerminalPrint terminalPrint = new TerminalPrint();

        public static void Main(string[] args)
        {
            Program program = new Program();
            Bank bank = new Bank();
            MenuUi menuUi = new MenuUi();
            bool running = true;

            bank.CreateAccount("Vishnu", 1000m, "Savings", 5m);

            while (running)
            {
                MainMenu mainMenu = menuUi.ShowMainMenu();

                switch (mainMenu)
                {
                    case MainMenu.Menu:
                        bool inMain = true;

                        while (inMain)
                        {
                            MenuOption option = menuUi.ShowMenu();
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
                                        program.terminalPrint.Pause();
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        program.terminalPrint.PrintError($"Error: {ex.Message}");
                                        program.terminalPrint.Pause();
                                        break;
                                    }

                                case MenuOption.DisplayAllAccounts:
                                    bank.DisplayAllAccounts();
                                    program.terminalPrint.Pause();
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

                                        program.terminalPrint.Pause();
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        program.terminalPrint.PrintError($"Error: {ex.Message}, Please enter Valid amount.");
                                        program.terminalPrint.Pause();
                                        break;
                                    }

                                case MenuOption.Withdraw:
                                    try
                                    {
                                        Console.Write("Enter Account Number: ");
                                        string? withdrawAcc = Console.ReadLine();

                                        Account? withdrawAccount = bank.accounts.FirstOrDefault(a => a.AccountNumber == withdrawAcc);

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

                                        program.terminalPrint.Pause();
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        program.terminalPrint.PrintError($"Error: {ex.Message}, Please enter Valid amount.");
                                        program.terminalPrint.Pause();
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

                                        program.terminalPrint.Pause();
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        program.terminalPrint.PrintError($"Error: {ex.Message} , Please enter Valid Details.");
                                        program.terminalPrint.Pause();
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

                                        program.terminalPrint.Pause();
                                    }
                                    catch (Exception ex)
                                    {
                                        program.terminalPrint.PrintError($"Error: {ex.Message}");
                                        program.terminalPrint.Pause();
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