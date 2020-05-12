using AssetBundles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UI;
using Spine.Unity;
using CSF.Tasks;

namespace CSF
{
    /// <summary>
    /// 游戏主入口
    /// </summary>
    public class Main : MonoBehaviour
    {

        //本地资源服务器测试
        public bool LocalResServerTest = false;
        //编辑器模式下启动版本检测
        public bool EditorVerCheck = false;

        //加载延时测试
        public bool LoaddelayTest = false;
        //不使用AB包读ILR(只有编辑器下有效果)
        public bool ILRNotABTest = false;

        //测试刘海高度
        public int TestCutoutsHeight = 0;

        public static Main I;
        void Awake()
        {
            I = this;
            //非编辑器模式下，强制改为true
            if (!Application.isEditor)
            {
                AppSetting.ILRNotABTest = false;
            }
            else
            {
                AppSetting.IsLocalResServer = LocalResServerTest;
                AppSetting.LoaddelayTest = LoaddelayTest;
                AppSetting.ILRNotABTest = ILRNotABTest;
                AppSetting.EditorVerCheckt = EditorVerCheck;
            }
            string sysLog = "是否使用本地资源测试:" + LocalResServerTest;
         
            sysLog += "   反射调用未绑定方法:" + AppSetting.HotFixUnbound;
            CLog.Sys(sysLog);
                        
            LogSys();
            DontDestroyOnLoad(this);
        }

        void Start()
        {
            Mgr.Initialize();
            //初始化UI管理器
            Mgr.UI.Initialize();
            StartTask().Run();
        }

        bool isStart = false;
        async CTask StartTask()
        {
            //版本检测....
            await Mgr.VersionCheck.Check();
            await Mgr.Assetbundle.Initialize();
            //初如化UIRoot
            await Mgr.UI.InitializeUIRoot();
            //初始化ILR
            await Mgr.ILR.Initialize();
            //启动热更程序
            Mgr.ILR.StartMain();

            isStart = true;
            //LoadSkeletonData();
        }

        private void Update()
        {
            if(isStart)
                Mgr.ILR.CallHotFixMainUpdate(Time.deltaTime);
        }


        //async void LoadSkeletonData()
        //{
        //    GameObject obj = new GameObject();
        //    SkeletonGraphic s =obj.AddComponent<Spine.Unity.SkeletonGraphic>();
        //    Debug.Log("加载资源。。。。");
        //    SkeletonDataAsset dat  = await Mgr.Assetbundle.LoadSkeletonData("Hero/101/silvia_1_SkeletonData");
        //    s.skeletonDataAsset = dat;
        //    s.Initialize(true);
        //    s.AnimationState.SetAnimation(0, "_1_Attack", true);
        //    s.AnimationState.TimeScale = 1;
        //    Debug.Log("加载资源");
        //}

        //async void LoadScenePrefab()
        //{
        //    CLog.Log("bbbb");
        //    GameObject obj = await Mgr.Assetbundle.LoadPrefab("scene/gameobject");
        //    obj.transform.localPosition = Vector3.one * 300;
        //}



        void LogSys()
        {
            string info = $@"系统信息: Unity版本:{Application.unityVersion} 当前时间:{System.DateTime.Now.ToString()}
<b>Platform:</b>{AppSetting.PlatformName}
<b>StreamingAssetsPath:</b>{Application.streamingAssetsPath}
<b>PersistentDataPath:</b>{Application.persistentDataPath}
<b>UDID:</b>{SystemInfo.deviceUniqueIdentifier}
<b>DeviceName:</b>{SystemInfo.deviceName}
<b>SystemMemorySize:</b>{SystemInfo.systemMemorySize}
<b>BundleIdentifier:</b>{Application.identifier}
<b>Version:</b>{Application.version}
<b>InternetReachability:</b>{Application.internetReachability}";
            CLog.Log(info);
        }


#if UNITY_EDITOR
        /// <summary>
        /// 是否按下了空格键,编辑器下特殊功能用
        /// </summary>
        public static bool Editor_IsKeyDownSpace = false;
        void OnGUI()
        {
            //主动断线，测试断线重连
            if (GUI.Button(new Rect(0, 0, 20, 20), "C"))
            {
                if (Mgr.ILR != null)
                    Mgr.ILR.CallHotFix("CloseSocket");
            }
            //主动断线，测试断线重连
            if (GUI.Button(new Rect(50, 0, 50, 20), "Time"))
            {
                if (Mgr.ILR != null)
                    Mgr.ILR.CallHotFix("LogTime");
            }
            Editor_IsKeyDownSpace = Input.GetKey(KeyCode.Space);
        }
 
        void OnValidate()
        {
            TestCutoutsHeight = Mathf.Clamp(TestCutoutsHeight, 0, 100);
            if (Mgr.UI != null)
            {
                Mgr.UI.canvasAdaptive.CutoutsHeight = TestCutoutsHeight;
                Mgr.UI.canvasAdaptive.CutoutsBottonHeight = TestCutoutsHeight / 2;
                Mgr.UI.SetNodePadding();
                foreach (var bg in Mgr.UI.UIRoot.GetComponentsInChildren<UIAdaptive>())
                    bg.Reset();
            }
        }
#endif

        private void OnApplicationQuit()
        {
            if (Mgr.ILR != null)
                Mgr.ILR.CallHotFix("OnApplicationQuit");
        }

        //游戏失去焦点也就是进入后台时 focus为false 切换回前台时 focus为true
        void OnApplicationFocus(bool focus)
        {

            if (focus)
            {
                //切换到前台时执行，游戏启动时执行一次
                //同步心跳包，保持时间同步
                if (Mgr.ILR != null)
                    Mgr.ILR.CallHotFix("OnFocus");
            }
            else
            {
                //切换到后台时执行
            }
        }
        
    }


}
