using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SGDCSharp
{
    class Program
    {
        static int Main(string[] args)
        {
            Launcher launcher = new Launcher();
            string sharedSecretKey = launcher.getSharedSecretFromConf();
            if (sharedSecretKey == "error")
                return 0;
            Console.SetWindowSize(20, 32);
            byte[] mSecret = Convert.FromBase64String(sharedSecretKey);
            if (mSecret == null)
            {
                Console.WriteLine("[ERROR] Given shared_key cannot be decrypted! Make sure it was the right one or send" +
            " your key to eaudrey96@gmail.com");
                return -1;
            }
            TimeCorrector TimeCorrector = new TimeCorrector();

            long lTime=TimeCorrector.currentTime();
            SGCPG SGD = new SGCPG();
            string code;
            bool bDone=true;
            int m_iTimeBefore;
            int previousSecond=0;
            lTime = TimeCorrector.currentTime();
            code = SGD.generateSteamGuardForTime(lTime, mSecret);
            Console.Write("Code is :");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(code);
            Console.ResetColor();
            while (true)
            {
                lTime = TimeCorrector.currentTime();
                m_iTimeBefore = TimeCorrector.timeBeforeNextChange(lTime);
                if (m_iTimeBefore < 30) //If the time is inferior to 30 (coz time is generated for 30 secs) then code could be generated again.
                    bDone = true;
                if ((m_iTimeBefore == 30) && (bDone))//If we already show once, and time changed then generate code and write it.
                {
                    Console.Clear();
                    code = SGD.generateSteamGuardForTime(lTime, mSecret);
                    Console.Write("Code is : ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(code);
                    Console.ResetColor();
                    bDone = false;
                }
                if (m_iTimeBefore != previousSecond) //If timeBeforeNextChange is different than previousSecond then show time left for this code.
                {
                    Console.WriteLine("Time left: " + m_iTimeBefore.ToString());
                    previousSecond = m_iTimeBefore;
                }
            }
        }
    }
}