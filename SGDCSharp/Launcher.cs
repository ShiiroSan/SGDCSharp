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
                return "error";
            }
        }
    }
}
