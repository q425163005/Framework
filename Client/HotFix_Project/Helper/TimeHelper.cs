using CSF.Tasks;
using System;

namespace HotFix_Project
{
    public static class TimeHelper
    {
        /// <summary>
        /// 延时执行方法
        /// </summary>
        /// <param name="sec"></param>
        /// <param name="act"></param>
        public static async CTask Delay(float sec, Action act)
        {
            await CTask.WaitForSeconds(sec);
            act();
        }

        private static readonly DateTime DateTime1970 = new DateTime(1970, 1, 1);

        /// <summary>
        /// 时间转时间戳(秒)
        /// </summary>
        /// <returns>秒</returns>
        public static int ToTimestamp(this DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(DateTime1970);
            return (int) (time - startTime).TotalSeconds;
        }

        /// <summary>
        /// 时间戳转时间
        /// </summary>
        /// <param name="timeStamp">时间戳(秒)</param>
        /// <returns>时间</returns>
        public static DateTime ToDateTime(this int timeStamp)
        {
            DateTime time = TimeZone.CurrentTimeZone.ToLocalTime(DateTime1970);
            return time.AddSeconds(timeStamp);
        }

        /// <summary>
        /// 时间戳转时间字符串 yyyy年MM月dd日
        /// </summary>
        /// <param name="timeStamp">时间戳(秒)</param>
        /// <returns>时间</returns>
        public static string ToDateTimeString(this int timeStamp)
        {
            DateTime time = TimeZone.CurrentTimeZone.ToLocalTime(DateTime1970);
            return time.AddSeconds(timeStamp).ToLongDateString();
        }

        /// <summary>
        /// 时间戳转时间字符串 yyyy年MM月dd日 HH:mm:ss
        /// </summary>
        /// <param name="timeStamp">时间戳(秒)</param>
        /// <returns>时间</returns>
        public static string ToLongTimeString(this int timeStamp)
        {
            DateTime time = TimeZone.CurrentTimeZone.ToLocalTime(DateTime1970);
            return time.AddSeconds(timeStamp).ToString("F");
        }


        /// <summary>
        /// 秒转成 0:00:00格式
        /// </summary>
        /// <param name="sec"></param>
        /// <returns></returns>
        public static string ToHHMMSS(this int sec)
        {
            int hour;
            int minute;
            int second;
            hour   = sec                 / 3600;
            minute = (sec - 3600 * hour) / 60;
            second = sec                 % 60;
            return hour + ":" + minute.ToString().PadLeft(2, '0') + ":" + second.ToString().PadLeft(2, '0');
        }


        /// <summary>
        /// 秒转成 0:00:00格式
        /// </summary>
        /// <param name="sec"></param>
        /// <returns></returns>
        public static string ToDDHHMMSS(this int sec)
        {
            int day;
            int hour;
            int minute;
            int second;
            day    = sec                                / 86400;
            hour   = (sec               - 86400 * day)  / 3600;
            minute = (sec - 86400 * day - 3600  * hour) / 60;
            second = sec                                % 60;
            string str = "";
            if (day > 0)
                str += day + "d ";
            if (hour > 0)
                str += hour + "h ";
            if (minute > 0)
                str += minute + "m ";
            if (second > 0)
                str += second + "s ";

            return str;
        }

        /// <summary>
        /// 秒转成字符串(用于比赛时间)
        /// mm:ss:ms
        /// </summary>
        /// <returns></returns>
        public static string MSecondToString(this int totalMs)
        {
            int mm = totalMs           / 60000; //分钟
            int ss = (totalMs % 60000) / 1000;  //秒
            int ms = totalMs           % 1000;
            if (mm > 0)
                return mm + ":" + ss.ToString().PadLeft(2, '0') + "." + ms.ToString().PadLeft(3, '0');
            else
                return ss + "." + ms.ToString().PadLeft(3, '0');
        }

        /// <summary>
        /// 数组时间转成分秒显示
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static string ToMMSS(this int[] time)
        {
            if (time == null || time.Length == 0)
                return "";
            return time[0].ToString().PadLeft(2, '0') + ":" + time[1].ToString().PadLeft(2, '0');
        }

        /// <summary>
        /// 秒转成 00:00格式
        /// </summary>
        /// <param name="sec"></param>
        /// <returns></returns>
        public static string ToMMSS(this int sec)
        {
            if (sec < 0) sec = 0;
            int minute;
            int second;
            minute = sec / 60;
            second = sec % 60;
            return minute.ToString().PadLeft(2, '0') + ":" + second.ToString().PadLeft(2, '0');
        }


        /// <summary>
        /// 跟据当前服务器时间获得剩余时间(秒)
        /// <param name="startTime">开始时间1001</param>
        /// <param name="endTime">结束时间1201</param>
        /// </summary>
        /// <returns></returns>
        public static int GetLeftTime(int startTime, int endTime)
        {
            if (startTime == 0 || endTime == 0)
                return 0;
            int      sce     = 0;
            DateTime now     = DateTime.Now;
            int      nowTime = now.Month * 100 + now.Day;
            if (endTime < startTime) //跨年了
            {
                if (!(nowTime > endTime && nowTime < startTime))
                {
                    DateTime end = new DateTime(now.Year, endTime / 100, endTime % 100).AddDays(1);
                    if (now.Month > endTime / 100)
                        end = end.AddYears(1);
                    sce = (int) ((end - now).TotalSeconds);
                }
            }
            else if (nowTime >= startTime && nowTime <= endTime)
            {
                DateTime end = new DateTime(now.Year, endTime / 100, endTime % 100).AddDays(1);
                sce = (int) ((end - now).TotalSeconds);
            }

            if (sce < 0)
                sce = 0;
            return sce;
        }
    }
}