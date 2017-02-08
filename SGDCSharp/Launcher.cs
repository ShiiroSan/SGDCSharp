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
        public string getSharedSecretFromConf()
        {
            if (File.Exists("./conf.txt"))
            {
                System.IO.StreamReader file = new System.IO.StreamReader("conf.txt");
                string sharedSecretKey = file.ReadLine().ToString();
                return sharedSecretKey; 
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The file conf.txt doesn't exist. You should create one and paste the shared_key value.");
                Console.WriteLine("You can contact the dev here for help : eaudrey96@gmail.com");
                Console.ReadKey();
                Console.ResetColor();
                return "error";
            }
        }
        private bool createLocalAccount()
        {
            bool error = false;
            string m_sUsername="", m_sPassword="";
            Console.Write("Desired username: ");
            m_sUsername=Console.ReadLine();
            if (m_sUsername == "")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You cannot use blank username!");
                Console.ResetColor();
            }
            Console.Write("Desired password: ");
            m_sPassword= Console.ReadLine();
            if (m_sPassword == "")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Are you sure you don't want to use password? This is big security issue.");
                Console.ResetColor();
            }
                return !error;
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
