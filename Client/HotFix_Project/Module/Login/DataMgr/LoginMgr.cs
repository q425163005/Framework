using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using CSF;
using Facebook.Unity;
using HotFix_Project.Common;
using HotFix_Project.Login;
using HotFix_Project.Module.Login.DataMgr.Data;
using LitJson;
using UnityEngine;
using UnityEngine.Networking;
using CSF.Tasks;

namespace HotFix_Project.Module.Login.DataMgr
{
    /// <summary>
    /// 登录数据管理器
    /// </summary>
    public class LoginMgr : BaseDataMgr<LoginMgr>, IDisposable
    {
        private const string Prefs_UserName  = "Login_UserName";
        private const string Prefs_VisitorId = "Login_VisitorId";

        /// <summary>用户名</summary>
        public string UserName = string.Empty;

        /// <summary>游客Id</summary>
        public string VisitorId = string.Empty;

        /// <summary>密码</summary>
        public string Password = string.Empty;

        /// <summary>参数1</summary>
        public string Param1 = string.Empty;

        /// <summary>游戏平台</summary>
        //public Enum_login_platform Platform = Enum_login_platform.LpAccountPwd;

        /// <summary>平台渠道Id</summary>
        public string PfChId = string.Empty;


        //登陆类型
        public ELoginPlatform LoginType = ELoginPlatform.Visitor;

        /// <summary>平台用户Id</summary>
        public string PlatformId = string.Empty;

        public string Token = string.Empty;

        public LoginMgr()
        {
            UserName  = PlayerPrefs.GetString(Prefs_UserName, string.Empty);
            VisitorId = PlayerPrefs.GetString(Prefs_VisitorId, string.Empty);
        }

        /// <summary>是否登录</summary>
        public bool IsLogin => UserName != string.Empty;

        /// <summary>
        /// 登录成功后保存用户名和密码
        /// </summary>
        public void LoginSucc(string name)
        {
            //if (CSF.AppSetting.PlatformType == EPlatformType.AccountPwd)
            //{
            //    UserName = name;
            //    PlayerPrefs.SetString(Prefs_UserName, UserName);
            //    PlayerPrefs.Save();
            //}
            //else if (LoginType == ELoginPlatform.Visitor)
            //{
            //    VisitorId = name;
            //    PlayerPrefs.SetString(Prefs_VisitorId, name);
            //    PlayerPrefs.Save();
            //}
        }

        /// <summary>登出</summary>
        public void Logout()
        {
            UserName = string.Empty;
            PlayerPrefs.SetString(Prefs_UserName, string.Empty);
            PlayerPrefs.Save();
        }


        public async CTask CheckVersionUpdate()
        {
            bool isUpdate = await CSF.Mgr.VersionCheck.CheckRemoteUpdate();
            if (isUpdate)
            {
                await CTask.WaitForNextFrame();
                Confirm.AlertLangTop(() =>
                {
                    Mgr.Dispose();
                    Mgr.UI.Show<LoginUI>();
                    //Mgr.Net.Close(false);
                }, "Version.Update", "Version.UpdateTitle").Run(); //更新提示
            }
        }

        public async CTask ShowNotice()
        {
            string versionFilesURL =
                CSF.AppSetting.VersionURL + "Notice.txt?t=" + DateTime.Now.ToString("u");
            UnityWebRequest request = UnityWebRequest.Get(versionFilesURL);
            await request.SendWebRequest();
            if (request.error != null)
                return;

            string[] line    = request.downloadHandler.text.Split('\n');
            string   title   = line[0];
            string   content = request.downloadHandler.text.Remove(0, title.Length + 1);
        }


        public bool isConnectIng = false;
        /// <summary>
        /// 登陆请求
        /// </summary>
        public void Login()
        {
            //连接服务器
            if (isConnectIng) return;

            ////服务器数据
            //ServerItemData server = ServerListMgr.I.GetSelectServer();
            //isConnectIng = true;
            //if (AppSetting.HttpServerType == EAppServerType.LocalServer)
            //{
            //    server.ServerName = "段思进-服务器";
            //    server.URL = "ws://192.168.0.108:3021";
            //}

            //if (!Mgr.Net.IsConnect)
            //    await Mgr.Net.Connect(server.URL);
            //if (Mgr.Net.IsConnect)
            //{
            //    CS_login_verify msg = new CS_login_verify();
            //    msg.PlatformId = PlatformId;
            //    msg.Token      = Token;
            //    switch (LoginType)
            //    {
            //        case ELoginPlatform.Visitor:
            //            msg.PlatformId = VisitorId;
            //            msg.LoginType  = Enum_login_type.LtGuest;
            //            break;
            //        case ELoginPlatform.Google:
            //            msg.PlatformId = UserName;
            //            msg.LoginType  = Enum_login_type.LtGuest;
            //            break;
            //        case ELoginPlatform.FaceBook:
            //            msg.PlatformId = PlatformId;
            //            msg.LoginType  = Enum_login_type.LtFacebook;
            //            break;
            //        case ELoginPlatform.Apple:
            //            msg.PlatformId = PlatformId;
            //            msg.LoginType = Enum_login_type.LtApple;
            //            break;
            //    }

            //    msg.Platform = Enum_login_platform.LpOwn;
            //    msg.ServerId = server.ServerId;
            //    msg.Param1   = Param1; //参数1
            //    //msg.ChId     = AppSetting.PlatformType == EPlatformType.OWN_GP ? "101" : "102";
            //    msg.Lang     = (int) Mgr.Lang.LangType;

            //    Mgr.Net.Send(msg);
            //}
            //else
            //{
            //    //请求资源信息错误
            //    Confirm.AlertLang(() => { Login().Run(); }, "Net.ConnectFailed", null); //连接服务器失败
            //    isConnectIng = false;
            //}
        }

        /// <summary>
        /// 清理游客登录缓存
        /// </summary>
        public void ClearVisitorCache()
        {
            if (!string.IsNullOrEmpty(PlayerPrefs.GetString(Prefs_UserName)))
            {
                PlayerPrefs.DeleteKey(Prefs_UserName);
            }
            if (!string.IsNullOrEmpty(PlayerPrefs.GetString(Prefs_VisitorId)))
            {
                PlayerPrefs.DeleteKey(Prefs_VisitorId);
            }
        }


        public override void Dispose()
        {
            Param1     = string.Empty;
            PfChId     = string.Empty;
            PlatformId = string.Empty;
            Token      = string.Empty;
            isConnectIng = false;
        }
    }
}