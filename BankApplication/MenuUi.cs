using System;
using System.Collections.Generic;
using System.Text;

namespace BankApplication
{
    public class MenuUi
    {
        public TerminalPrint terminalPrint;

        public MenuUi(TerminalPrint terminalPrint)
        {
            this.terminalPrint = terminalPrint;
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
                terminalPrint.Pause();
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
                terminalPrint.Pause();
                return ShowMenu();
            }

            return (MenuOption)input;
        }
    }
}
