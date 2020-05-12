using HotFix_Project.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotFix_Project.Sound;
using HotFix_Project.UIEffect;
using CSF.Tasks;

namespace HotFix_Project
{
    public class Mgr
    {
        /// <summary>UI管理器</summary>
        public static UIMgr UI;

        /// <summary>网络消息管理器</summary>
        //public static NetMgr Net;

        /// <summary>多语言管理器</summary>
        public static LangMgr Lang;

        /// <summary>配置表管理器</summary>
        public static ConfigMgr Config;
        
        

        /// <summary>声音管理器</summary>
        public static SoundMgr Sound;

        /// <summary>声音管理器</summary>
        public static UIItemEffectMgr UIItemEffect;
        

        /// <summary>缓存的数据管理器,用来统一清空缓存数据用</summary>
        public static List<IDisposable> __dataMgrList = new List<IDisposable>();

        public static async CTask Initialize()
        {
            UI           = new UIMgr();
            Lang         = new LangMgr();
            Config       = new ConfigMgr();
            Sound        = new SoundMgr();
            UIItemEffect = new UIItemEffectMgr();
            float startTime = UnityEngine.Time.realtimeSinceStartup;           
            await Config.Initialize();           
            CLog.Log("Load Config Time:" + (UnityEngine.Time.realtimeSinceStartup - startTime) + " seconds");
            await UIItemEffect.Initialize();
        }

        //static void Test()
        //{
        //    float startTime = UnityEngine.Time.realtimeSinceStartup;
        //    float elapsedTime = UnityEngine.Time.realtimeSinceStartup - startTime;
        //    UnityEngine.Debug.Log("Use time " + elapsedTime + " seconds");
        //}

        /// <summary>
        /// 释放全部缓存数据,断线重连后调用
        /// </summary>
        public static void Dispose()
        {
            UI.CloseAll(); //关闭全部UI
            for (int i = 0; i < __dataMgrList.Count; i++)
            {
                CLog.Log("释放" + __dataMgrList[i]);
                __dataMgrList[i].Dispose();
            }
            //foreach (IDisposable dis in __dataMgrList)
            //{
            //    CLog.Log("释放" + dis);
            //    dis.Dispose();
            //}
            //MsgWaiting.CleanAll();

            if (Sound != null)
                Sound.Dispose();
        }
    }
}