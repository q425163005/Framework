using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotFix_Project.Common;
using UnityEngine;

namespace HotFix_Project
{
    public class Utils
    {
        /// <summary>
        /// 表示空
        /// </summary>
        public static readonly Vector3 Vector3Null = Vector3.one * -10000;

        /// <summary>
        /// 表示空
        /// </summary>
        public static readonly Vector2Int Vector2IntNull = Vector2Int.one * -10000;

        private static readonly Color[] fontColor =
        {
            Color.white,                             //白色
            new Color(145f      / 255, 1f, 0, 1),    //91ff00
            new Color(0, 1, 49f / 255, 1),           //00e131
            new Color(1f, 85f   / 255, 0, 1),        //ff5500
            new Color(1f, 158f  / 225, 32f / 225, 1) //ff9e20
        };

        private static readonly string[] fontColorStr =
        {
            "FFFFFF", "91ff00", "00e131", "ff5500", "ff9e20","ff0000"
        };

        /// <summary>
        /// 获取颜色
        /// </summary>
        public static Color GetColor(EColorType type)
        {
            return GetColor((int) type);
        }

        public static Color GetColor(int type)
        {
            return fontColor[type];
        }

        public static string GetColorStr(EColorType type, string str)
        {
            return GetColorStr((int) type, str);
        }


        public static string GetColorStr(int type, string str)
        {
            return $"<color=#{fontColorStr[type]}>{str}</color>";
        }

        public static string GetColorStr(string rgb, string str)
        {
            return $"<color=#{rgb}>{str}</color>";
        }

        public static string GetSizeStr(int size, string str)
        {
            return $"<size={size}>{str}</size>";
        }

        public static string GetSizeOrColorStr(string str, int size, string rgb)
        {
            return GetColorStr(rgb, GetSizeStr(size, str));
        }

        // lvl50
        public static string GetLevelStr(int level, int lvlSize = 30, int lvlValSize = 40)
        {
            //string lvlstr    = GetSizeOrColorStr(Mgr.Lang.GetFormat(level.ToString()), lvlSize, "fa7648");
            //string lvlvalstr = GetSizeOrColorStr(level.ToString(), lvlValSize, "fae6c8");
            //return lvlstr.Replace(level.ToString(), lvlvalstr);
            return $"<color=#fa7648><size={lvlSize}>lvl</size></color><size={lvlValSize}><color=#fae6c8>{level}</color></size>";
        }

        //X50
        public static string GetNumStr(int num,int numSize = 35,int numValSize = 40)
        {
            return GetNumStr(num.ToString(), numSize, numValSize);
        }
        public static string GetNumStr(string num, int numSize = 35, int numValSize = 40)
        {
            return $"<color=#fa7648><size={numSize}>x</size></color><size={numValSize}><color=#fae6c8>{num}</color></size>";
        }

        //获取资源
        public static string GetResStr(long currNum, long maxNum)
        {
            return $"<size=30><color=#fae6c8>{currNum.ToMoney()}</color></size><size=24><color=#fa7648>/{maxNum.ToMoney()}</color></size>";
        }

        /// <summary>
        /// 获取枚举值名称
        /// 如:GetEnumName(EQuality.Good)  返回优秀
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enu"></param>
        /// <param name="addColon">是否后面加冒号，默认不加</param>
        /// <param name="meaning">多重意思编号,默认没有</param>
        /// <returns></returns>
        public static string GetEnumName<T>(T enu, bool addColon = false, int meaning = 0) where T : struct
        {
            Type type = typeof(T);
            if (!type.IsEnum)
                return string.Empty;
            string key = type.Name + "." + enu.ToString();
            if (meaning > 0) //此枚举有多重意思
                key += meaning;
            return Mgr.Lang.Get(key) + (addColon ? ":" : string.Empty);
        }

        

        private static readonly string[] EffColorStr =
        {
            "CB0000", "0080E5", "29E500", "E5BD00", "A000E5"
        };

        /// <summary>
        /// 获取特效颜色
        /// 英雄属性类型+自定义
        /// </summary>
        /// <returns></returns>
        public static string GetEffColor(int index)
        {
            if (index >= EffColorStr.Length)
            {
                CLog.Error("特效背景颜色越界");
                return "ffffff";
            }

            return EffColorStr[index];
        }

        public static string GetColorStrByAttr(int index, string str, bool light)
        {
            if (light)
            {
                return $"<color=#{AttrColorStrLight[index]}>{str}</color>";
            }

            return $"<color=#{AttrColorStrDark[index]}>{str}</color>";
        }

        public static string GetColorByAttr(int index, bool light)
        {
            if (light)
            {
                if (index >= AttrColorStrLight.Length)
                {
                    CLog.Error("高亮颜色越界");
                    return "ffffff";
                }

                return AttrColorStrLight[index];
            }

            if (index >= AttrColorStrDark.Length)
            {
                CLog.Error("暗沉颜色越界");
                return "ffffff";
            }

            return AttrColorStrDark[index];
        }


        private static readonly string[] AttrColorStrLight =
        {
            "FFA2A5", "91ECFF", "93FE91", "FFF889", "FFB8FF"
        };

        private static readonly string[] AttrColorStrDark =
        {
            "FD6C71", "3FB8E9", "00DD2E", "E9B524", "E76DFF"
        };

        public static Color GetColorByRGB(int index, bool light)
        {
            return GetColorByRGB(light == true ? AttrColorStrLight[index] : AttrColorStrDark[index]);
        }

        public static Color GetColorByRGB(string rgb)
        {
            Color color;
            ColorUtility.TryParseHtmlString($"#{rgb}", out color);
            return color;
        }

        /// <summary>
        /// 获取一个经过判断后的颜色
        /// 绿橙
        /// </summary>
        /// <returns></returns>
        public static string GetConditionColorStr(int nowVal, int checkVal)
        {
            //string str = GetColorStr(nowVal >= checkVal ? EColorType.Green : EColorType.Orange, checkVal.ToString());
            string str = GetColorStr(nowVal >= checkVal ? "FAE6C8" : "FF0000", checkVal.ToString());
            return str;
        }
    }
}