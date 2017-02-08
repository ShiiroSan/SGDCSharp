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
        System.IO.StreamReader fileRead = new System.IO.StreamReader(l_sFile);
        System.IO.StreamWriter fileWrite = new System.IO.StreamWriter(l_sFile);
        const byte MAXACCOUNT = 32;
        const byte MAXMEMBER = 4;
        /* EXEMPLE DE FICHIER CONF
         * 
         * shiirosan:password:sharedsecret
         * unnamed:sonpass:sonsharedsecret
         * 
         */
        public string readLine(int line)
        {
            string[] wholeText = File.ReadAllLines(fileRead.ReadToEnd());
            return wholeText[line];
        }
        public void writeLine(string textToWrite, int line)
        {
            try
            {
                string[] arrLine = File.ReadAllLines(fileRead.ReadToEnd());
                arrLine[line - 1] = textToWrite;
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
            string[,] m_arrMember= new string[MAXACCOUNT, MAXMEMBER];
            short m_iLine=0;
            string m_szReadedLine;
            char m_chSeparator = ';';
            do
            {
                m_szReadedLine = this.readLine(m_iLine);
                for(int i=1; i < MAXMEMBER; i++)
                {
                    m_arrMember[m_iLine, i] = m_szReadedLine.Split(m_chSeparator)[i];
                }
                m_iLine++;
            } while ((m_iLine < MAXACCOUNT) || (m_szReadedLine != null));
            return m_arrMember;
        }

        public bool writeMember(string username, string password, string shared_secret) //Tableau écris comme suivant username:password:shared_secret
        {
            try
            {
                fileWrite.WriteLine(username + ":" + password + ":" + shared_secret);
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
            }
            return true;
        }

        public string[] readUsername()
        {
            string[,] m_arMember = this.readAllMember();
            string[] m_arUsername=new string[MAXACCOUNT];
            int m_iArMemberLength = m_arMember.Length;
            do
            {

            } while ();
            for(int lineNumber = 0; lineNumber >= m_iArMemberLength; lineNumber++)
            {
                m_arUsername[lineNumber] = m_arMember[lineNumber, 0];
            }
            return m_arUsername;
        }

        public string readSpecificUsername(int linePos)
        {
            string m_szUsername="NONE";
            return m_szUsername = this.readUsername()[linePos];
        }

        public string[] readPassword()
        {
            string[,] m_arMember = this.readAllMember();
            string[] m_arUsername = new string[MAXACCOUNT];
            int m_iArMemberLength = m_arMember.Length;
            for (int lineNumber = 0; lineNumber >= m_iArMemberLength; lineNumber++)
            {
                m_arUsername[lineNumber] = m_arMember[lineNumber, 0];
            }
            return m_arUsername;
        }

        public string readSpecificPassword(int linePos)
        {
            string m_szPassword = "NONE";
            return m_szPassword = this.readPassword()[linePos];
        }

        public string[] readSharedSecret()
        {
            string[,] m_arMember = this.readAllMember();
            string[] m_arUsername = new string[MAXACCOUNT];
            int m_iArMemberLength = m_arMember.Length;
            for (int lineNumber = 0; lineNumber >= m_iArMemberLength; lineNumber++)
            {
                m_arUsername[lineNumber] = m_arMember[lineNumber, 0];
            }
            return m_arUsername;
        }

        public string readSpecificSharedSecret(int linePos)
        {
            string m_szSharedSecret = "NONE";
            return m_szSharedSecret = this.readSharedSecret()[linePos];
        }

        public string writeUsername()
        {
            return "Sup";
        }
    }
}
