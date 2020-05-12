using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSF.Tasks;

namespace HotFix_Project
{
    /// <summary>
    /// 提供主工程调用的接口
    /// </summary>
    public class ILRMainCall
    {
        /// <summary>
        /// 获取脚本
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetLang(string key, int type = -1)
        {
            if (type == -1)
                return Mgr.Lang.Get(key);
            return Mgr.Lang.Get(key, (ELangType) type);
        }

        /// <summary>
        /// 关闭Socket连接
        /// </summary>
        public static void CloseSocket()
        {
        }

        /// <summary>
        /// 打印时间
        /// </summary>
        public static void LogTime()
        {
            //CLog.Error($"LocalTime:{DateTime.Now}[{DateTime.Now.ToTimestamp()}] ServerTime:{Mgr.Time.ServerTime}[{Mgr.Time.ServerTimestamp}]");
        }


        public static void OnApplicationQuit()
        {
            CloseSocket();
        }

        /// <summary>
        /// 指引调试
        /// </summary>
        public static void GuideDebug(int id)
        {
            //GuideMgr.I.Debug(id);
        }

        /// <summary>
        /// 完成当前正在执行的调试
        /// </summary>
        public static void GuideComplete()
        {
            //GuideMgr.I.GuideComplete();
        }

        /// <summary>
        /// 全部关闭
        /// </summary>
        public static void GuideCloseAll()
        {
            //GuideMgr.I.DebugGuideCloseAll();
        }

        /// <summary>
        /// 重新加载指引数据
        /// </summary>
        public static async CTask ReloadGuideConfig()
        {
            await Mgr.Config.ReloadConfig(Mgr.Config.dicGuide);
            await Mgr.Config.ReloadConfig(Mgr.Config.dicGuideStep);
            CLog.Log("重新加载指引数据完成");
        }

        /// <summary>
        /// 重新加载功能开放数据
        /// </summary>
        public static async CTask ReloadFunOpenConfig()
        {
            await Mgr.Config.ReloadConfig(Mgr.Config.dicFunOpen);
            //FunOpenMgr.I.ReloadFunOpenConfig();
            CLog.Log("重新加载功能开放数据完成");
        }

        /// <summary>
        /// 功能开放调试
        /// </summary>
        public static void FunOpenDebug(string funId, bool isOpen)
        {
            //FunOpenMgr.I.FunOpenDebug(funId, isOpen);
        }

        /// <summary>
        /// 功能开放调试
        /// </summary>
        public static void FunOpenAllDebug()
        {
            //FunOpenMgr.I.FunOpenAllDebug();
        }

        /// <summary>
        /// 功能开放调试
        /// </summary>
        public static void FunOpenAllFBLevel()
        {
            //FunOpenMgr.I.FunOpenAllFBLevel();
        }


        /// <summary>
        /// 前往指定UI
        /// </summary>
        public static void GuideGoDebug(string strArg, int intArg1, int intArg2)
        {
            //GuideGoUtils.GoUI(strArg, intArg1, intArg2);
        }

        /// <summary>
        /// 多倍攻击伤害调试
        /// </summary>
        public static void WarMultAttackDebug(int mult)
        {
            //WarN.WarHero.DebugMult = mult;
        }

        /// <summary>
        /// 亮屏后同步时间
        /// </summary>
        public static void OnFocus()
        {
            //Mgr.Time?.OnFocusToUpDateTime();
        }
    }
}