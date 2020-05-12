using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSF;
using CSF.Tasks;
using HotFix_Project.Common;
using HotFix_Project.Login;
using HotFix_Project.Module.Login.DataMgr.Data;
using UnityEngine;
using UnityEngine.Networking;

namespace HotFix_Project.Module.Login.DataMgr
{
    /// <summary>
    /// 服务器列表数据
    /// </summary>
    public class ServerListMgr : BaseDataMgr<ServerListMgr>, IDisposable
    {
        private string Prefs_ServerURL = "Prefs_ServerURL";

        /// <summary>服务器列表</summary>
        public Dictionary<string, ServerItemData> dicServerList = new Dictionary<string, ServerItemData>();

        /// <summary>
        /// 选中的服务器地址
        /// </summary>
        public string ServerURL = string.Empty;


        /// <summary>
        /// 是否获取到服务器信息
        /// </summary>
        public bool IsGetServerData = false;

        public ServerListMgr()
        {
            ServerURL =  PlayerPrefs.GetString(Prefs_ServerURL, string.Empty);
        }

        /// <summary>
        /// 获取当前选中的服务器数据
        /// </summary>
        public ServerItemData GetSelectServer()
        {
            dicServerList.TryGetValue(ServerURL, out var data);
            if (data == null && dicServerList.Count > 0)
                return dicServerList.Values.ElementAt(0);
            return data;
        }

        public int GetSelectServerId()
        {
            ServerItemData data;
            if (dicServerList.TryGetValue(ServerURL, out data))
                return data.ServerId;
            return 1;
        }

        public void SetServerId(string url)
        {
            //if (ServerURL != url)
            //{
            //    ServerURL = url;
            //    if (Mgr.Net.IsConnect == true)
            //        Mgr.Net.Close(false);
            //    Mgr.UI.GetUI<LoginUI>()?.ResetConnectInt();
            //}

            //PlayerPrefs.SetString(Prefs_ServerURL, ServerURL);
            //PlayerPrefs.Save();
            //Mgr.UI.GetUI<LoginUI>()?.SetServerInfo();
        }


        /// <summary>
        /// 请求服务器列表
        /// </summary>
        public async CTask ReqServerList()
        {
            string serverListFilesURL = CSF.AppSetting.VersionURL + "ServerList.txt?t=" + DateTime.Now.ToString("u");
            CLog.Log(serverListFilesURL);
            UnityWebRequest request = UnityWebRequest.Get(serverListFilesURL);
            await request.SendWebRequest();
            if (request.error != null)
            {
                CLog.Error($"URL Error[{serverListFilesURL}]:{request.error} ");
                //请求资源信息错误
                Confirm.AlertLangTop(() => { ReqServerList().Run(); }, "ServerList.Failed", null).Run();
                return;
            }

            dicServerList = new Dictionary<string, ServerItemData>();
            List<ServerItemData> serverList =
                LitJson.JsonMapper.ToObject<List<ServerItemData>>(request.downloadHandler.data.GetUTF8String());
            foreach (ServerItemData data in serverList)
                dicServerList.Add(data.URL, data);


            //编辑器模型加二个开发服务器
            if (Application.isEditor)
            {
                ServerItemData item = new ServerItemData();
                item.ServerId   = 1;
                item.ServerName = "外网测试服";
                item.URL        = "ws://180.76.242.101:2016";
                if (!dicServerList.ContainsKey(item.URL))
                    dicServerList.Add(item.URL, item);

                item            = new ServerItemData();
                item.ServerId   = 1;
                item.ServerName = "陈俊红-开发服(2026)";
                item.URL        = "ws://192.168.0.115:2026";
                if (!dicServerList.ContainsKey(item.URL))
                    dicServerList.Add(item.URL, item);

                item            = new ServerItemData();
                item.ServerId   = 1;
                item.ServerName = "段思进-开发服(2026)";
                item.URL        = "ws://192.168.0.108:2026";
                if (!dicServerList.ContainsKey(item.URL))
                    dicServerList.Add(item.URL, item);

                item = new ServerItemData();
                item.ServerId = 1;
                item.ServerName = "外网正式";
                item.URL = "ws://8.208.9.56:10001";
                if (!dicServerList.ContainsKey(item.URL))
                    dicServerList.Add(item.URL, item);
            }

            if (CSF.AppSetting.PlatformType == EPlatformType.PC)
            {
                //Mgr.UI.GetUI<LoginUI>()?.SetServerInfo();
            }
          
            IsGetServerData = true;
        }
        


        public override void Dispose()
        {
        }
    }
}