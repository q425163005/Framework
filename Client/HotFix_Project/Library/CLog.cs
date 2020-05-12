using System;
using System.Text;
using UnityEngine;

namespace HotFix_Project
{
    public class CLog
    {

        public static bool isShowLog = true;
        public static bool IsDebug => isShowLog;
        public static void SetIsShowLog(bool isShow)
        {
            isShowLog = isShow;
        }
        /// <summary>
        /// 普通打印输出
        /// </summary>
        /// <param name="msg"></param>
        public static void Log(params object[] msg)
        {
            if (isShowLog)
                CSF.CLog.LogStackTrace(ObjectsToString(null, msg));
        }
        /// <summary>
        /// 输出警告信息
        /// </summary>
        public static void Warning(params object[] msg)
        {
            if (isShowLog)
                Debug.LogWarning(ObjectsToString(null, msg));
        }
        /// <summary>
        /// 输出错误信息
        /// </summary>
        public static void Error(params object[] msg)
        {
            CSF.CLog.ErrorStackTrace(ObjectsToString(null, msg));
        }

        public static void LogException(Exception exception)
        {
            Debug.LogException(exception);
        }

        /// <summary>
        /// 消息日志
        /// </summary>
        /// <param name="isSend"></param>
        /// <param name="msg"></param>
        public static void LogMsg(bool isSend, string msg)
        {
            if (!isShowLog) return;
            string sTitle = string.Empty;
            if (isSend)
                sTitle = "<color=#76EE00>[发送消息]</color>";
            else
                sTitle = "<color=#436EEE>[收到消息]</color>";
            Debug.Log(ObjectsToString(sTitle, msg));
        }

        public static string ObjectsToString(string sTitle, params object[] logs)
        {
            StringBuilder sb = new StringBuilder();
            if (sTitle == null)
                sb.Append("<color=#FF00FF>[HotFix:]</color>");
            else
                sb.Append(sTitle);
            for (int i = 0; i < logs.Length; ++i)
            {
                if (logs[i] == null)
                    sb.Append("null");
                else
                    sb.Append(logs[i].ToString());
                if (i < logs.Length - 1)
                    sb.Append(", ");
            }
            return sb.ToString();
        }
    }
}
