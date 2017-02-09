using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SGDCSharp
{
    class ConfigReader
    {
        const string l_sFile = "conf.txt";
        //System.IO.StreamReader fileRead = new System.IO.StreamReader(l_sFile);
        
        public int g_iFileLine;
        const byte MAXMEMBER = 4;
        /* EXEMPLE DE FICHIER CONF
         * 
         * shiirosan:password:sharedsecret
         * unnamed:sonpass:sonsharedsecret
         * 
         */

        public ConfigReader()
        {
            if (!File.Exists(l_sFile))
            {
                File.Create("conf.txt");
                this.makeNewAccount();
            }
            g_iFileLine = File.ReadAllLines(l_sFile).Length;
        }

        public bool makeNewAccount()
        {

            string m_szAccUsername="", m_szAccPass="", m_szAccSharedSecret="";
            Console.Write("Enter your username: ");
            m_szAccUsername=Console.ReadLine();
            Console.Write("Enter your password: ");
            m_szAccPass = Console.ReadLine();
            Console.Write("Enter your shared_secret key: ");
            m_szAccSharedSecret = Console.ReadLine();
            string m_szNewAccount = m_szAccUsername + ":" + m_szAccPass + ":" + m_szAccSharedSecret;
            //System.IO.StreamWriter tempTW = new System.IO.StreamWriter("conf.txt");
            //tempTW.WriteLine(m_szNewAccount);
            this.writeMember(m_szAccUsername, m_szAccPass, m_szAccSharedSecret);
            //this.writeMember(m_szAccUsername, m_szAccPass, m_szAccSharedSecret);
            return true;
        }

        public string readLine(int line)
        {
            string[] wholeText = File.ReadAllLines(l_sFile);
            Console.WriteLine("[debug] readLine: "+wholeText[line]);
            return wholeText[line];
        }
        public void writeLine(string textToWrite, int line)
        {
            try
            {
                string[] arrLine = File.ReadAllLines(l_sFile);
                arrLine[line-1] = textToWrite;
                File.WriteAllLines(l_sFile, arrLine);
            }
            catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(
                    "{0}: The write operation could not " +
                    "be performed because the specified " +
                    "part of the file is locked.",
                    e.Message);
                Console.ReadKey();
            }
        }

        public string[,] readAllMember() //Tableau contenant: nombre_element, username, password, shared_secret
        {
            string[,] m_arrMember= new string[g_iFileLine, MAXMEMBER];
            short m_iLine=0;
            string m_szReadedLine;
            char m_chSeparator = ':';
            //Console.WriteLine("[debug] {readAllMember} numberOfLine : "+ g_iFileLine);
            if (g_iFileLine > 0)
            {
                do
                {
                    m_szReadedLine = this.readLine(m_iLine);
                    for (int i = 0; i < MAXMEMBER - 1; i++)
                    {
                        //Console.WriteLine("[debug] {readAllMember} i pos: " + i);
                        m_arrMember[m_iLine, i] = m_szReadedLine.Split(m_chSeparator)[i];
                    }
                    m_iLine++;
                } while ((m_iLine < g_iFileLine));
            }
            return m_arrMember;
        }

        public bool writeMember(string username, string password, string shared_secret) //Tableau écris comme suivant username:password:shared_secret
        {
            /*
            try
            {
                string[] arrLine = File.ReadAllLines(l_sFile); //We get all line 
                string[] newArrLine = new string[arrLine.Length + 1]; //doing newArrLine = arrLine is equal to point newArrLine on arrLine which mean that length will be the same for both.
                for (int i = 0; i < arrLine.Length; i++) // Ofc this is way slow but I don't know how to increment size of pre-allocated array.
                {
                    newArrLine[i] = arrLine[i];
                }
                newArrLine[arrLine.Length] = username + ":" + password + ":" + shared_secret; //We add our line to the array which contain all previous line
                File.WriteAllLines(l_sFile, newArrLine); // Then we write everything on our files. Big disk usage...
            }
            catch (IOException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(
                    "{0}: The write operation could not " +
                    "be performed because the specified " +
                    "part of the file is locked.",
                    e.GetType().Name);
                return false;
            }*/
            System.IO.StreamWriter tempTW = new System.IO.StreamWriter("conf.txt");
            tempTW.WriteLine(username + ":" + password + ":" + shared_secret);
            tempTW.Close();
            g_iFileLine++;
            return true;
        }

        public string[] readUsername()
        {
            string[,] m_arMember = this.readAllMember();
            string[] m_arUsername=new string[g_iFileLine];
            int lineNumber = 0;
            bool m_bExitLoop=false;
            while (!m_bExitLoop) //This is the worst way. Will be kept until found this to shitty.
            {
                try
                {
                    int m_iForceException = m_arMember[lineNumber, 0].Length;
                    m_arUsername[lineNumber] = m_arMember[lineNumber, 0];
                }
                catch (Exception)
                {
                    m_bExitLoop = true;
                }
                lineNumber++;
            }
            //byte m_siMaxLineNum = (byte)File.ReadAllLines(fileRead.ReadToEnd()).Length; /* Just another way to get max numerous username. Linked to the for-condition */
            /*for(int lineNumber = 0; lineNumber >= m_siMaxLinNum; lineNumber++) 
             //Read line by line to get all the username and write them on m_arUsername.
             //This way you take also blank username but it would allow line feed.
            {
                m_arUsername[lineNumber] = m_arMember[lineNumber, 0];
            }*/
            return m_arUsername;
        }

        public string readSpecificUsername(int linePos)
        {
            string m_szUsername="NONE";
            return m_szUsername = this.readUsername()[linePos];
        }

        public int findUsername(string m_szSearchedUsername) //For obvious reason, no findPassword. This would mean you'll need to decrypt password, compare then re-encrypt. 
        {
            int m_iLineWhereFound = -1; 

            return m_iLineWhereFound; //if -1, line isn't found ofc
        }


        public string[] readPassword()
        {
            string[,] m_arMember = this.readAllMember();
            string[] m_arPassword = new string[g_iFileLine];
            byte m_siMaxLineNum = (byte)g_iFileLine;
            for (int lineNumber = 0; lineNumber < m_siMaxLineNum; lineNumber++)
            {
                m_arPassword[lineNumber] = m_arMember[lineNumber, 1];
            }
            return m_arPassword;
        }

        public string readSpecificPassword(int linePos)
        {
            string m_szPassword = "NONE";
            return m_szPassword = this.readPassword()[linePos];
        }

        public string[] readSharedSecret()
        {
            string[,] m_arMember = this.readAllMember();
            string[] m_arSharedSecret = new string[g_iFileLine];
            byte m_siMaxLineNum = (byte)g_iFileLine;
            for (int lineNumber = 0; lineNumber < m_siMaxLineNum; lineNumber++)
            {
                m_arSharedSecret[lineNumber] = m_arMember[lineNumber, 3];
            }
            return m_arSharedSecret;
        }

        public string readSpecificSharedSecret(int linePos)
        {
            string m_szSharedSecret = "NONE";
            return m_szSharedSecret = this.readSharedSecret()[linePos];
        }
    }
}
