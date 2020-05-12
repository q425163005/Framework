using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.CLR.Method;
using CSF.Tasks;
using System.Reflection;
using System;
using System.Collections.Generic;

namespace CSF
{
    public class ILRMgr : BaseMgr<ILRMgr>
    {
        //调用热更工程的统一类
        private string HotFixClass = AppSetting.HotFixName + ".ILRMainCall";
        private string HotFixMainClass = AppSetting.HotFixName + ".Main";

#if REFLECT
        private Assembly assembly;
        public Dictionary<string, Type> assemblyTypes = new Dictionary<string, Type>();

        public Type GetAssemblyType(string typeName)
        {
            Type mType;
            if (!assemblyTypes.TryGetValue(typeName, out mType))
            {
                 mType = this.assembly.GetType(typeName);
                assemblyTypes.Add(typeName, mType);
            }
            return mType;
        }
#else
        //AppDomain是ILRuntime的入口，最好是在一个单例类中保存，整个游戏全局就一个，这里为了示例方便，每个例子里面都单独做了一个
        //大家在正式项目中请全局只创建一个AppDomain
        public ILRuntime.Runtime.Enviorment.AppDomain appdomain { get; private set; }
#endif

        /// <summary>
        /// 初始化
        /// </summary>
        public async CTask Initialize()
        {
            await loadHotFixAssembly();
        }


        IMethod getLangMethod;
        object[] getLangparam = new object[2];
        /// <summary>
        /// 调用热更工程中获取语言
        /// </summary>
        /// <param name="key"></param>
        public string CallHotFixGetLang(string key, int type = -1)
        {
            getLangparam[0] = key;
            getLangparam[1] = type;
#if REFLECT
            return Invoke(HotFixClass, "GetLang", getLangparam).ToString();
#else
            if (getLangMethod == null)
                getLangMethod = appdomain.LoadedTypes[HotFixClass].GetMethod("GetLang", 2);
            return appdomain.Invoke(getLangMethod, null, getLangparam).ToString();
#endif
        }

        private async CTask loadHotFixAssembly()
        {
            bool isDebug = getIsDebug();

            //加载热更DLL
            byte[] dll = await loadILRFile(false);

            //加载热更PDB文件是调试数据库，如需要在日志中显示报错的行号，则必须提供PDB文件，
            //不过由于会额外耗用内存，正式发布时请将PDB去掉，下面LoadAssembly的时候pdb传null即可
            byte[] pdb = null;
            if (isDebug)
                pdb = await loadILRFile(true);

#if REFLECT
            this.assembly = Assembly.Load(dll, pdb);
#else
            //首先实例化ILRuntime的AppDomain，AppDomain是一个应用程序域，每个AppDomain都是一个独立的沙盒
            appdomain = new ILRuntime.Runtime.Enviorment.AppDomain();
            System.IO.MemoryStream fs = new MemoryStream(dll);
            {
                if (pdb != null)
                {
                    System.IO.MemoryStream pdbStream = new MemoryStream(pdb);
                    appdomain.LoadAssembly(fs, pdbStream, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
                }
                else
                    appdomain.LoadAssembly(fs);
            }
            InitializeILRuntime(isDebug);
            mHotFixMainUpdate = appdomain.LoadedTypes[HotFixMainClass].GetMethod("Update", 1);
#endif

#if DISABLE_ILRUNTIME_DEBUG && (UNITY_EDITOR) &&(!REFLECT) //UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE
            appdomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
#endif
        }

        /// <summary>
        /// 调用热更工程的访求
        /// </summary>
        /// <param name="method"></param>
        /// <param name="args"></param>
        public void CallHotFix(string method, params object[] args)
        {
            //appdomain.Invoke(HotFixClass, method, null, args);
            Invoke(HotFixClass, method, args);
        }
        IMethod mHotFixMainUpdate;
        public void CallHotFixMainUpdate(float deltaTime)
        {
            //param[0] = deltaTime;
            //appdomain.Invoke(HotFixMainClass, "Update", null, param);
#if REFLECT
            Invoke(HotFixMainClass, "Update", new object[] { deltaTime });
#else
            using (var ctx = appdomain.BeginInvoke(mHotFixMainUpdate))
            {
                ctx.PushFloat(deltaTime);
                ctx.Invoke();
                //return ctx.ReadInteger();
            }
#endif
        }

        /// <summary>
        /// 加载ILR 热更文件
        /// </summary>
        private async CTask<byte[]> loadILRFile(bool isPdb)
        {
            if (!Application.isEditor && !AppSetting.ILRNotABTest)
            {
                //IRL DLL ab包加载
                string assetName = AppSetting.HotFixName + (isPdb ? "_pdb" : "");
                string abName = AppSetting.HoxFixBundleDir + assetName + ".bytes";
                TextAsset asset = await Mgr.Assetbundle.LoadAsset<TextAsset>(abName, assetName);
                return asset.bytes;
            }
            else
            {
                //IRL DLL 直接加载
                string pdbName = ".pdb";
#if REFLECT
                pdbName = ".dll.mdb";
#endif
                string path = AppSetting.ILRCodeDir + AppSetting.HotFixName + (isPdb ? pdbName : ".dll");
                WWW www = await new WWW(path);
                if (!string.IsNullOrEmpty(www.error))
                    UnityEngine.Debug.LogError(www.error + " URL:" + path);
                byte[] fileByte = www.bytes;
                www.Dispose();
                return fileByte;
            }
        }

        void InitializeILRuntime(bool isDebug)
        {
#if !REFLECT
            appdomain.AllowUnboundCLRMethod = AppSetting.HotFixUnbound;
            if (isDebug)
            {
                //开启Debug调试
                appdomain.DebugService.StartDebugService(56000);
            }
            //这里做一些ILRuntime的注册
            ILRHelper.InitILRuntime(appdomain);
#endif
            SetIsDebug(isDebug);
        }

        /// <summary>
        /// 启动热更主程序
        /// </summary>
        public void StartMain()
        {
            //appdomain.Invoke($"{AppSetting.HotFixName}.Main", "Start", null, null);
            Invoke($"{AppSetting.HotFixName}.Main", "Start", null);
        }
        public void SetIsDebug(bool isDebug)
        {
            //appdomain.Invoke($"{AppSetting.HotFixName}.CLog", "SetIsShowLog", null, isDebug);
            Invoke($"{AppSetting.HotFixName}.CLog", "SetIsShowLog", new object[] { isDebug });
            CLog.SetIsShowLog(isDebug);
            Debug.Log("是否启用调试:" + (isDebug ? "是" : "否(并且不显示日志)"));
            PlayerPrefs.SetInt("Reporter_msglog", isDebug ? 1 : 0);
            PlayerPrefs.Save();
        }

        protected object Invoke(string typeName, string method, object[] args)
        {
#if REFLECT
            Type mType = GetAssemblyType(typeName);
            return mType.GetMethod(method).Invoke(null,args);
#else
            return appdomain.Invoke(typeName, method, null, args);
#endif
        }

        /// <summary>
        /// 获取是否为debug状态
        /// false 不显示日志信息,不加载调试文件
        /// </summary>
        /// <returns></returns>
        private bool getIsDebug()
        {
            //设置日志开关
            bool isShowLog = false;
#if UNITY_EDITOR
            isShowLog = true;//(PlayerPrefs.GetInt("Reporter_msglog", 1) == 1) ? true : false; //默认显示日志
#else
            isShowLog = (PlayerPrefs.GetInt("Reporter_msglog") == 1) ? true : false;          
#endif
            return isShowLog;
        }
    }
}
