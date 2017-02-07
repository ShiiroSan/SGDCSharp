using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace SGDCSharp
{
    class TimeCorrector
    {
        long _timeCorrector=1L;
        long systemTimeSeconds()
        {
            //long lSeconds = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            //Console.WriteLine("[debug] seconds from systemTimeSeconds: " + lSeconds);
            return (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds - _timeCorrector;
    }
        public long currentTime()
        {
            return systemTimeSeconds()-_timeCorrector;
        }
        public int timeBeforeNextChange(long lPassedTime/*=0*/)
        {
            if (lPassedTime==0) lPassedTime = systemTimeSeconds();
            //Console.WriteLine("[debug] passed time in timeBeforeNextChange: " + lPassedTime.ToString());
            int iRemainingTimeForCode = (int)(30L - lPassedTime % 30L);
            //Console.WriteLine("[debug] time left for code in timeBeforeNextChange: " + iRemainingTimeForCode.ToString());
            return iRemainingTimeForCode;
        }
    }
}
