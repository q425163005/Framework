using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CSF.Tasks;
using UnityEngine;

namespace HotFix_Project
{
    public static class Extension
    {
        /// <summary>
        /// 获取字符串全角长度，一个汉字算二个
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int GetFullLength(this string str)
        {
            if (str.Length == 0) return 0;
            ASCIIEncoding ascii   = new ASCIIEncoding();
            int           tempLen = 0;
            byte[]        s       = ascii.GetBytes(str);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int) s[i] == 63)
                    tempLen += 2;
                else
                    tempLen += 1;
            }

            return tempLen;
        }

        public static string ToBase64String(this string value)
        {
            if (value == null || value == "")
            {
                return "";
            }

            byte[] bytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(bytes);
        }

        public static string UnBase64String(this string value)
        {
            if (value == null || value == "")
                return "";
            byte[] bytes = Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// 判断是否有非法字符
        /// </summary>
        /// <returns></returns>
        public static bool CheckIllegalChar(this string str)
        {
            Regex reg = new Regex("[?!@#$%\\^&*()]+");
            Match m   = reg.Match(str);
            return m.Success;
        }

        /// <summary>
        /// 保证返回数据为范围内的数值.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val">待测值.</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>T.</returns>
        public static T Limit<T>(this T val, T min, T max) where T : IComparable
        {
            var maxVal = (val.CompareTo(min)    > 0) ? val : min;
            var minVal = (maxVal.CompareTo(max) > 0) ? max : maxVal;
            return minVal;
        }


        public static Vector2 ToVector2(this int[] val)
        {
            if (val == null || val.Length == 0) return Vector2.zero;
            return new Vector2(val[0], val[1]);
        }

        public static Vector3 ToVector3(this int[] val)
        {
            if (val == null || val.Length == 0) return Vector2.zero;
            return new Vector3(val[0], val[1], val[3]);
        }


        /// <summary>
        /// 万分比值 转成  百分比字符串  1=0.01%
        /// </summary>
        /// <param name="myriadVal"></param>
        /// <returns></returns>
        public static string ToPctString(this int val)
        {
            return val / 100f + "%";
        }

        /// <summary>
        /// 货币显示转换
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToMoney(this int val)
        {
            return ToMoney((long) val);
        }

        /// <summary>
        /// 货币显示转换
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToMoney(this long val)
        {
            string money = string.Empty;
            if (val < 1000)
                money = val.ToString();
            else if (val < 1000000)
                money = (val / 1000) + "K";
            else
                money = (val / 1000000) + "M";
            return money;

            /*
              if (val < 100000)
                money = val.ToString();
            else if (val < 10000000)
                money = (val / 1000) + "K";
            else
                money = (val / 1000000) + "M";
             */
        }


        public static string GetUTF8String(this byte[] buffer)
        {
            if (buffer == null)
                return null;
            if (buffer.Length <= 3)
                return Encoding.UTF8.GetString(buffer);
            byte[] bomBuffer = new byte[] {0xef, 0xbb, 0xbf};
            if (buffer[0] == bomBuffer[0] && buffer[1] == bomBuffer[1] && buffer[2] == bomBuffer[2])
                return new UTF8Encoding(false).GetString(buffer, 3, buffer.Length - 3);
            return Encoding.UTF8.GetString(buffer);
        }


        /// <summary>
        /// 建筑升级时间显示转换
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToBuildUpTime(this int val)
        {
            string time = string.Empty;
            if (val < 60)
                time = val + "s";
            else if (val < 3600)
                time = (val / 60) + "m" + val % 60 + "s";
            else if (val < 86400)
                time = (val / 3600) + "h" + ((val % 3600) / 60) + "m";
            else if (val < 2592000)
                time = (val / 86400) + "d" + (val % 86400 / 3600) + "h";
            else
                time = (val / 2592000) + "M" + (val % 2592000 / 86400) + "d";
            return time;
        }

      
        /// <summary>
        /// 竞技场战绩时间显示转换
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToAreaRecordTime(this int val)
        {
            string time = string.Empty;
            if (val < 60)
                time = val + "s";
            else if (val < 3600)
                time = (val / 60) + "m";
            else if (val < 86400)
                time = (val / 3600) + "h";
            else
                time = (val / 86400) + "d";

            return Mgr.Lang.GetFormat("WatchTowerUI.recordTime", time);
        }

        /// <summary>
        /// 拆分一个整数，按照给定个个数 
        /// 0-10 分2份
        /// 10以上 分5份
        ///
        /// </summary>
        /// <param name="Num"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int[] SplitToInts(this int Num, int count)
        {
            if (Num <= count)
            {
                CLog.Log($"被拆分的整数[{Num}]必须大于拆分的个数[{count}]");
                return null;
            }

            int[] tempInts = new int[count + 1];
            tempInts[0]     = 0;
            tempInts[count] = Num;
            //随机生成count-1个 1----Num-1不重复的数
            int[] randoms = RandomHelper.Randoms(count - 1, 1, Num);
            for (int i = 1; i < count; i++)
            {
                tempInts[i] = randoms[i - 1];
            }

            Array.Sort(tempInts);
            //计算结果
            int[] result = new int[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = tempInts[i + 1] - tempInts[i];
            }

            return result;
        }

        public static void IntArrayToStr(this int[] nums)
        {
            string str = "";
            for (int i = 0; i < nums.Length; i++)
            {
                str += nums[i] + "*";
            }

            CLog.Log("数值值为:" + str);
        }


        public static Color ToColor(this string colorStr)
        {
            Color color = Color.white;
            ColorUtility.TryParseHtmlString(colorStr, out color);
            return color;
        }

        public static T GetAddComponent<T>(this GameObject gameObject) where T : Component
        {
            T compent = gameObject.GetComponent<T>();
            if (compent == null)
                compent = gameObject.AddComponent<T>();
            return compent;
        }

        public static CTaskHandle Run(this CTask task)
        {
            return CSF.Mgr.Task.Manager.Run(task);
        }
    }
}