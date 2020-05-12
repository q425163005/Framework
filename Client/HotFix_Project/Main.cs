using HotFix_Project.Common;
using HotFix_Project.Login;
using HotFix_Project.Module.Login.DataMgr;
using UnityEngine;
using CSF.Tasks;
using CSF;

namespace HotFix_Project
{
    /// <summary>
    /// 游戏热更入口
    /// </summary>
    public class Main
    {
        //开始游戏
        public static void Start()
        {
            CLog.Log("=====进入热更主程序,启动游戏!!!!=====");
            Initialize().Run();
            Application.targetFrameRate = 30;
            QualitySettings.vSyncCount  = 0;

            Application.runInBackground = true;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        private static async CTask Initialize()
        {
            await Mgr.Initialize();

            await LoadHelper.Initialize();
            //账密登陆，进入选服
            if (CSF.AppSetting.PlatformType != EPlatformType.PC)
            {               
                //SDK初始化
                //SDKManager.I.SDK_Init();               
            }



            // 打开登录界面选服
            await Mgr.UI.Show<LoginUI>().Await();

            //ServerListMgr.I.ReqServerList().Run(); //请求服务器列表
            //new Effect.ClickEffectTrigger();

            //关闭版本检测界面
            CSF.Mgr.VersionCheck.Close().Run();
        }

        

        public static void Update(float deltaTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Mgr.UI.GetUI<ItemTipUI>()?.Close();
            }
        }

        //private async static void Update()
        //{
        //    WaitForFixedUpdate wait = new WaitForFixedUpdate();
        //    while (true)
        //    {
        //        if (Input.GetKeyUp(KeyCode.Escape))
        //        {
        //            Confirm.ShowLang(() => {
        //                Application.Quit();
        //            }, null, "Quit.Confirm");
        //        }
        //        await wait;
        //    }
        //}



        
    }
}