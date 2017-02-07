using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Security.Cryptography;

//SGCPG is just a reference to old source code :D

namespace SGDCSharp
{
    class SGCPG
    {
        const int SGCharsLength = 27;
        //[] s_cgchSteamguardCodeChars = { "23456789BCDFGHJKMNPQRTVWXY" };

        private static byte[] s_cgchSteamguardCodeChars = new byte[] { 50, 51, 52, 53, 54, 55, 56, 57, 66, 67, 68, 70, 71, 72, 74, 75, 77, 78, 80, 81, 82, 84, 86, 87, 88, 89 };

        public string generateSteamGuardForTime(long lTime, byte[] mSecret)
        {
            string steamCode;
            if (mSecret == null)
            {
                steamCode = "";
            }
            else
            {
                lTime /= 30L;
                byte[] mainComponent = new byte[8];
                int n2=8;
                while (true)
                {
                    int n3 = n2 - 1;
                    if (n2 <= 0)
                        break;

                    mainComponent[n3] = (byte)lTime;
                   // Console.WriteLine("mainComponent[n3] with pos: " + n3.ToString() + " worth " + (int)mainComponent[n3]);
                   // Console.WriteLine("lTime before bitwise worth " + lTime.ToString());
                    lTime >>= 8;
                   // Console.WriteLine("lTime after bitwise worth " + lTime.ToString());
                    n2 = n3;
                }
                HMACSHA1 hmacgenerator = new HMACSHA1();
                hmacgenerator.Key = mSecret;
                byte[] hashedData = hmacgenerator.ComputeHash(mainComponent);
                byte[] byteArray = new byte[5];

                byte b = (byte)(hashedData[19] & 0xF);
                int codePoint = (hashedData[b] & 0x7F) << 24 | (hashedData[b + 1] & 0xFF) << 16 | (hashedData[b + 2] & 0xFF) << 8 | (hashedData[b + 3] & 0xFF);

                for (int i = 0; i<5; ++i)
                {
                    byteArray[i] = s_cgchSteamguardCodeChars[codePoint % s_cgchSteamguardCodeChars.Length];
                    //Console.WriteLine("Byte array at pos: " + i.ToString() + " have for value : " + byteArray[i].ToString());
                    codePoint /= s_cgchSteamguardCodeChars.Length;
                }
                steamCode = Encoding.UTF8.GetString(byteArray);
                return steamCode;
            }
            steamCode = "";
            return steamCode;
        }
        public string generateSteamGuardCode(byte[] mSecret)
        {
            TimeCorrector TC = new TimeCorrector();
            return this.generateSteamGuardForTime(TC.currentTime(), mSecret);
        }

    }
}
