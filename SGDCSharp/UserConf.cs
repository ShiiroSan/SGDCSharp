using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SGDCSharp
{
    class UserConf
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

        public UserConf()
        {
            if (!File.Exists(l_sFile))
            {
                File.Create("conf.txt");
                this.makeNewAccount();
            }
            g_iFileLine = File.ReadAllLines(l_sFile).Length;
        }

        public string Between(string STR, string FirstString, string LastString)
        {
            string FinalString;
            int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
            int Pos2 = STR.IndexOf(LastString);
            FinalString = STR.Substring(Pos1, Pos2 - Pos1);
            return FinalString;
        }

        public bool makeNewAccount()
        {

            string m_szAccUsername="", m_szAccPass="", m_szAccSharedSecret="";
            Console.Write("Enter your username: ");
            m_szAccUsername=Console.ReadLine();
            if (m_szAccUsername=="")
            {
                while (m_szAccUsername=="")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("You should enter an username!");
                    Console.ResetColor();
                    Console.Write("Enter your username: ");
                    m_szAccUsername = Console.ReadLine();
                }
            }
            Console.Write("Enter your password: ");
            m_szAccPass = Console.ReadLine();
            if (m_szAccPass == "")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Using a password is required to limit access to your steamguard code.");
                Console.ResetColor();
                Console.Write("Are you sure you won't use a password? (Y/n)");

                ConsoleKey m_chNoPassVerif=Console.ReadKey().Key;
                if ((m_chNoPassVerif.ToString() == "Y") || (m_chNoPassVerif.ToString() == "Enter"))
                {
                    m_szAccPass = "0";
                }
                else
                {
                    do
                    {
                        Console.Write("Enter your password: ");
                        m_szAccPass = Console.ReadLine();
                    } while (m_szAccPass=="");
                }
             }
            Console.Write("Enter your shared_secret key: ");
            m_szAccSharedSecret = Console.ReadLine();
            if (m_szAccSharedSecret == "")
            {
                while (m_szAccSharedSecret == "")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("You should enter your shared_secret key!");
                    Console.ResetColor();
                    Console.Write("Enter your shared_secret key: ");
                    m_szAccSharedSecret = Console.ReadLine();
                }
            }
            string m_szNewAccount = m_szAccUsername + ":" + m_szAccPass + ":" + m_szAccSharedSecret;
            this.writeMember(m_szNewAccount);
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

        private bool writeMember(string username, string password, string shared_secret) //Tableau écris comme suivant username:password:shared_secret
        {
            using (StreamWriter tempTW = File.AppendText(l_sFile))
                tempTW.WriteLine(username + ":" + password + ":" + shared_secret);
            g_iFileLine++;
            return true;
        }

        private bool writeMember(string m_szNewMember) //Tableau écris comme suivant username:password:shared_secret
        {
                using (StreamWriter tempTW = File.AppendText(l_sFile))
                    tempTW.WriteLine(m_szNewMember);
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

        public string readSpecificUsername(int linePos) //Si la valeur retourné est NONE alors le nom d'utilisateur ne peut-être trouvé.
        {
            string m_szUsername="NONE";
            return m_szUsername = this.readUsername()[linePos];
        }

        public int findUsername(string m_szSearchedUsername) //For obvious reason, no findPassword. This would mean you'll need to decrypt password, compare then re-encrypt. 
        {
            int m_iLineWhereFound = -1, i=0;
            byte m_siMaxUsername=(byte)g_iFileLine;
            string[] m_arrUsernameList = this.readUsername();
            try
            {
                while (m_szSearchedUsername.ToLower() != m_arrUsernameList[i].ToLower())
                {
                    i++;
                }
                m_iLineWhereFound = i;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return m_iLineWhereFound; //if -1, line isn't found ofc   
            }
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
                m_arSharedSecret[lineNumber] = m_arMember[lineNumber, 2];
            }
            return m_arSharedSecret;
        }

        public string readSpecificSharedSecret(int linePos)
        {
            string m_szSharedSecret = "NONE";
            return m_szSharedSecret = this.readSharedSecret()[linePos];
        }

        public bool importSteamConfToNewAccount(string m_szPlaceOfFile)
        {
            bool m_bNoError = true;
            try
            {
                string m_szContentOfSGFile = File.ReadAllText(m_szPlaceOfFile);
                string m_szAccountName = this.Between(m_szContentOfSGFile, "\"account_name\":\"", "\",\"token_gid");
                string m_szSharedSecret = this.Between(m_szContentOfSGFile, "\"shared_secret\":\"", "\",\"serial_number");
                Console.WriteLine(m_szSharedSecret);
                Console.Write("Enter your password: ");
                string m_szPassword = Console.ReadLine();
                if (m_szPassword == "")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Using a password is required to limit access to your steamguard code.");
                    Console.ResetColor();
                    Console.Write("Are you sure you won't use a password? (Y/n)");

                    ConsoleKey m_chNoPassVerif = Console.ReadKey().Key;
                    if ((m_chNoPassVerif.ToString() == "Y") || (m_chNoPassVerif.ToString() == "Enter"))
                    {
                        m_szPassword = "0";
                    }
                    else
                    {
                        do
                        {
                            Console.Write("Enter your password: ");
                            m_szPassword = Console.ReadLine();
                        } while (m_szPassword == "");
                    }
                }
                this.writeMember(m_szAccountName, m_szPassword, m_szSharedSecret);
                Console.Write("User ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(m_szAccountName);
                Console.ResetColor();
                Console.WriteLine(" has been added to the list of account.");
            }
            catch (Exception)
            {
                m_bNoError = false;
                Console.WriteLine("error happened!");
            }
            return m_bNoError;
        }
    }
}
