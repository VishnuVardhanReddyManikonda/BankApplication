using System;
using System.Collections.Generic;
using System.Text;

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
}
