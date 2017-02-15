using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SGDCSharp
{
    class Launcher
    {
        const bool _CONNECTED = true;
        UserConf userConfig = new UserConf();
        public Launcher()
        {

        }

        public void mainMenu()
        {

        }

        public void localAccountMenu()
        {
            Console.WriteLine("[1] Add a new account");
            Console.WriteLine("[2] Import a new account");
            Console.WriteLine("[3] Connect to an account");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[0] Back");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.Enter:
                    break;
                case ConsoleKey.Escape:
                    break;
                case ConsoleKey.Spacebar:
                    break;
                case ConsoleKey.NumPad0:
                    break;
                case ConsoleKey.NumPad1:
                    break;
                case ConsoleKey.NumPad2:
                    break;
                case ConsoleKey.NumPad3:
                    break;
                case ConsoleKey.NumPad4:
                    break;
                case ConsoleKey.NumPad5:
                    break;
                case ConsoleKey.NumPad6:
                    break;
                case ConsoleKey.NumPad7:
                    break;
                case ConsoleKey.NumPad8:
                    break;
                case ConsoleKey.NumPad9:
                    break;
                case ConsoleKey.Oem1:
                    break;
                case ConsoleKey.OemPlus:
                    break;
                case ConsoleKey.OemComma:
                    break;
                case ConsoleKey.OemMinus:
                    break;
                case ConsoleKey.OemPeriod:
                    break;
                case ConsoleKey.Oem2:
                    break;
                case ConsoleKey.Oem3:
                    break;
                case ConsoleKey.Oem4:
                    break;
                case ConsoleKey.Oem5:
                    break;
                case ConsoleKey.Oem6:
                    break;
                case ConsoleKey.Oem7:
                    break;
                case ConsoleKey.Oem8:
                    break;
                case ConsoleKey.Oem102:
                    break;
                default:
                    break;
            }
        }

        private bool connectToLocalAccount()
        {

            return true;
        }
        
        private void manageLocalAccount()
        {

        }
    }
}
