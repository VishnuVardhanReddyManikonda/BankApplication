using System;
using System.Collections.Generic;
using System.Text;

namespace BankApplication
{
    public class TerminalPrint
    {
            public void PrintSuccess(string message)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(message);
                Console.ResetColor();
            }

            public void PrintError(string message)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(message);
                Console.ResetColor();
            }

            public  void PrintWarning(string message)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(message);
                Console.ResetColor();
            }

            public void PrintHeader(string message)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(message);
                Console.ResetColor();
            }
        }
    }

