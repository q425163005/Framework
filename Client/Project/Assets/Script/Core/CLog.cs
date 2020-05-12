using System;
using System.Collections.Generic;
using System.Text;
using ILRuntime.CLR.Method;
using ILRuntime.CLR.Utils;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using UnityEngine;

namespace CSF
{
    public class CLog
    {
        private static bool isShowLog = true;
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
                Debug.Log(ObjectsToString(msg));
        }

        /// <summary>
        /// 输出带ILRuntime堆栈信息的日志
        /// </summary>
        /// <param name="msg"></param>
        public static void LogStackTrace(string msg)
        {
            if (isShowLog)
                Debug.Log(msg);
        }
        /// <summary>
        /// 输出带ILRuntime堆栈信息的日志
        /// </summary>
        /// <param name="msg"></param>
        public static void ErrorStackTrace(string msg)
        {
            if (isShowLog)
                Debug.LogError(msg);
        }

        /// <summary>
        /// 输出警告信息
        /// </summary>
        public static void Warning(params object[] msg)
        {
            if (isShowLog)
                Debug.LogWarning(ObjectsToString(msg));
        }
        /// <summary>
        /// 输出错误信息
        /// </summary>
        public static void Error(params object[] msg)
        {
            Debug.LogError(ObjectsToString(msg));
        }
        /// <summary>
        /// 系统输出信息
        /// </summary>
        /// <param name="msg"></param>
        public static void Sys(params object[] msg)
        {
            Debug.Log("<color=#FF8000>[SYS:]" + ObjectsToString(msg) + "</color>");
        }

        public static string ObjectsToString(object[] logs, string sTitle = null)
        {
            StringBuilder sb = new System.Text.StringBuilder();
            if (sTitle != null)
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

        public unsafe static void RegisterILRuntimeCLRRedirection(ILRuntime.Runtime.Enviorment.AppDomain appdomain)
        {
            appdomain.RegisterCLRMethodRedirection(typeof(CSF.CLog).GetMethod("LogStackTrace"), DLogStackTrace);
            appdomain.RegisterCLRMethodRedirection(typeof(CSF.CLog).GetMethod("ErrorStackTrace"), DErrorStackTrace);
        }
        public unsafe static StackObject* DLogStackTrace(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);
            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);

            string @message = (string)typeof(string).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);
            Debug.Log(string.Format("{0}\n{1}", @message, __domain.DebugService.GetStackTrace(__intp)));
            return __ret;
        }
        public unsafe static StackObject* DErrorStackTrace(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);
            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);

            string @message = (string)typeof(string).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);
            Debug.LogError(string.Format("{0}\n{1}", @message, __domain.DebugService.GetStackTrace(__intp)));
            return __ret;
        }

    }
}