using AssetBundles;
using CSF.Tasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace CSF
{
    public partial class VersionCheckMgr : BaseMgr<VersionCheckMgr>
    {
        /// <summary>
        /// 资源信息
        /// </summary>
        public Dictionary<string, ABResFile> dicABRes = new Dictionary<string, ABResFile>();

        /// <summary>
        /// 资源版本号
        /// </summary>
        public int ResVersion = 0;

        /// <summary>远程服务器版本信息</summary>
        private VersionInfo remoteVersion;

        /// <summary>远程服务器AB资源版本信息</summary>
        private ABResInfo remoteABRes;

        /// <summary>StreamingAssets AB资源版本信息</summary>
        private ABResInfo streamingABRes;

        /// <summary>PersistentDataPath AB资源版本信息</summary>
        private ABResInfo persistentABRes;

        /// <summary>需要下载的队列</summary>
        private Queue<ABResFile> needUpdateList;
        /// <summary>总公需要下载的大小</summary>
        private ulong totalSizeVer = 0;
        /// <summary>已经下载的大小</summary>
        private ulong downSizeVer = 0;

        /// <summary>
        /// 验证版本信息
        /// </summary>
        private async CTask ValidationVersion()
        {
            //获取远程版本信息
            if (AppSetting.IsVersionCheck)
            {
                string versionFilesURL = AppSetting.VersionURL + AppSetting.VersionFile;
                CLog.Error("VersionURL:" + versionFilesURL);
                UnityWebRequest request = UnityWebRequest.Get(versionFilesURL);
                await request.SendWebRequest();
                if (request.error != null)
                {
                    CLog.Error($"URL Error[{versionFilesURL}]:{request.error} ");
                    //请求资源信息错误
                    checkUI.Confirm(() =>
                    {
                        ValidationVersion().Run();
                    }, null, VerCheckLang.Request_Version_Error, VerCheckLang.ErrorTitle);
                    return;
                }
                List<VersionInfo> verInfos = LitJson.JsonMapper.ToObject<List<VersionInfo>>(request.downloadHandler.text);
                string platformName = AppSetting.PlatformName.ToLower();
                foreach (VersionInfo ver in verInfos)
                {
                    if (ver.Platform.ToLower() == platformName)
                    {
                        remoteVersion = ver;
                        remoteVersion.ResURL = remoteVersion.ResURL.TrimEnd('/') + "/";
                        AppSetting.IsForcedUpdate = ver.IsForcedUpdate;
                    }
                }
                if (remoteVersion == null)
                {
                    //请求资源信息错误
                    checkUI.Confirm(() =>
                    {
                        ValidationVersion().Run();
                    }, null, VerCheckLang.Version_Platform_Error, VerCheckLang.ErrorTitle);
                    return;
                }
            }
            else
            {
                remoteVersion = new VersionInfo();
                CLog.Error("Not Version Check!");
            }
            //版本号过低,重新下载APK
            if (Application.version.CompareTo(remoteVersion.AppVersion) < 0)
            {
                checkUI.Confirm(() =>
                {
                    string updateURL = remoteVersion.AppDownloadURL;
                    //if (AppSetting.PlatformType == EPlatformType.HY_MC)
                    //{
                    //    if (!string.IsNullOrEmpty(remoteVersion.AppDownloadURL_MC))
                    //        updateURL = remoteVersion.AppDownloadURL_MC;
                    //}
                    if (!string.IsNullOrEmpty(updateURL))
                        Application.OpenURL(updateURL);
                    Application.Quit();
                }, null, VerCheckLang.Version_Low, VerCheckLang.ErrorTitle);
                return;
            }
            else //版本号相同
            {
                await loadPersistentABFiles();
                if (persistentABRes != null)
                    SetVersion(persistentABRes.Version);
                //本地资源版本较低
                if (persistentABRes == null || persistentABRes.Version < remoteVersion.ResVersion)
                {
                    //加载远程版本信息
                    loadRemoteABFiles().Run();
                }
                else
                {
                    SetComplate(persistentABRes);
                }
            }
        }
        /// <summary>
        /// 设置更新完成状态
        /// </summary>
        /// <param name="abRes"></param>
        private void SetComplate(ABResInfo abRes)
        {
            ResVersion = abRes.Version;
            foreach (ABResFile file in abRes.dicFileInfo.Values)
            {
                if (file.isStreaming)
                    AssetBundleManager.assetBundleURL.Add(file.File, AppSetting.StreamingAssetsURL + file.File);
                else
                    AssetBundleManager.assetBundleURL.Add(file.File, AppSetting.PersistentDataURL + file.File);
            }

            if (remoteABRes != null)
                remoteABRes.Dispose();
            if (streamingABRes != null)
                streamingABRes.Dispose();
            if (persistentABRes != null)
                persistentABRes.Dispose();
            isUpdateCheckComplete = true;
        }

        /// <summary>
        /// 获取本地资源版本信息
        /// </summary>
        private async CTask loadPersistentABFiles()
        {
            await loadStreamingABFiles();
            if (streamingABRes != null && streamingABRes.Version >= remoteVersion.ResVersion)
            {
                persistentABRes = streamingABRes;
                CLog.Error("Use StreamingABFiles");
            }
            else
            {
                string abFiles = AppSetting.PersistentDataURL + AppSetting.ABFiles;
                UnityWebRequest request = UnityWebRequest.Get(abFiles);
                await request.SendWebRequest();
                if (request.error == null)
                {
                    string[] line = request.downloadHandler.text.Split('\n');
                    persistentABRes = new ABResInfo(request.downloadHandler.text);
                }
                else
                {
                    await loadStreamingABFiles();
                    persistentABRes = streamingABRes;
                }
            }
        }

        /// <summary>
        /// 获取StreamingAssets目录AB文件信息
        /// </summary>
        private async CTask loadStreamingABFiles()
        {
            string abFiles = AppSetting.StreamingAssetsURL + AppSetting.ABFiles;
			CLog.Error(abFiles);
            UnityWebRequest request = UnityWebRequest.Get(abFiles);
            await request.SendWebRequest();
            if (request.error == null)
            {
                string[] line = request.downloadHandler.text.Split('\n');
                streamingABRes = new ABResInfo(request.downloadHandler.text, true);
            }
            else
            {
                CLog.Error("Load StreamingAssets Error:" + request.error);
            }
        }

        /// <summary>
        /// 获取远程AB文件信息
        /// </summary>
        /// <returns></returns>
        private async CTask loadRemoteABFiles()
        {
            //获取远程版本信息
            string abFiles = remoteVersion.ResURL + AppSetting.ABFiles;
            UnityWebRequest request = UnityWebRequest.Get(abFiles);
            await request.SendWebRequest();
            if (request.error != null)
            {
                CLog.Error($"URL Error[{abFiles}]:{request.error} ");
                isUpdateCheckComplete = false;
                //请求资源信息错误
                checkUI.Confirm(() =>
                {
                    loadRemoteABFiles().Run();
                }, null, VerCheckLang.Request_Version_Error, VerCheckLang.ErrorTitle);
                return;
            }
            remoteABRes = new ABResInfo(request.downloadHandler.text);
            needUpdateList = new Queue<ABResFile>();
            totalSizeVer = 0;
            int md5 = 0;
            int exists = 0;
            if (persistentABRes == null) //全部需要重新下载
            {
                foreach (ABResFile file in remoteABRes.dicFileInfo.Values)
                {
                    file.Version = remoteABRes.Version;
                    needUpdateList.Enqueue(file);
                    totalSizeVer += file.Size;
                }
            }
            else
            {
                ABResFile localFile;
                foreach (ABResFile file in remoteABRes.dicFileInfo.Values)
                {
                    persistentABRes.dicFileInfo.TryGetValue(file.File, out localFile);
                    if (localFile == null || (localFile.MD5 != file.MD5 && remoteABRes.Version > localFile.Version))
                    {
                        file.Version = remoteABRes.Version;
                        file.isStreaming = false;
                        needUpdateList.Enqueue(file);
                        totalSizeVer += file.Size;
                        md5++;
                    }
                    else
                    {
                        bool isExists = false;
                        string path;
                        if (localFile.isStreaming)
                        {
                            //path = Path.Combine(AppSetting.StreamingAssetsPath, localFile.File);
                            //isExists = File.Exists(path);
                            isExists = true;
                        }
                        else
                        {
                            path = Path.Combine(AppSetting.PersistentDataPath, localFile.File);
                            isExists = File.Exists(path);
                        }

                        if (!isExists) //配置里存在，但文件不存在，重新下载
                        {
                            file.Version = remoteABRes.Version;
                            file.isStreaming = false;
                            needUpdateList.Enqueue(file);
                            totalSizeVer += file.Size;
                            exists++;
                        }
                        else
                        {
                            if (localFile.Version == remoteABRes.Version)
                            {
                                file.Version = remoteABRes.Version;
                                totalSizeVer += file.Size;
                                downSizeVer += file.Size;
                            }
                            else
                            {
                                file.isStreaming = localFile.isStreaming;
                                file.Version = localFile.Version;
                            }
                        }
                    }
                }
            }
            if (needUpdateList.Count == 0)
            {
                SetComplate(persistentABRes);
            }
            else
            {
                CLog.Error("需要更新资源数：" + needUpdateList.Count);
                string content = string.Format(VerCheckLang.Version_Update_Confirm_Content, getSizeString(totalSizeVer - downSizeVer));
                checkUI.Confirm(() =>
                {
                    updateABFile().Run();
                }, Utils.ApplicationQuit, content, VerCheckLang.Version_Update_Confirm_Title, false);
                return;
            }
        }

      

        /// <summary>
        /// 开始更新资源
        /// </summary>
        private async CTask updateABFile()
        {
            string str = string.Format(VerCheckLang.Version_Update, getSizeString(downSizeVer), getSizeString(totalSizeVer), "0K");
            SetTitle(str);
            SetValue((downSizeVer) / (float)totalSizeVer, true);
            int downloadNum = Math.Min(5, needUpdateList.Count);

            List<ABDownLoad> downList = new List<ABDownLoad>();
            ABDownLoad downLoad;
            ABResFile file;
            ulong loadCompleteSize = 0; //已完成的下载
            ulong loadingSize = 0;          //正在进行的下载
            ulong totalLoadSize = 0;
            bool isAllLoadComplate = false;
            long downTime = 0;
            ulong secDownSize = 0;
            ulong currDownSize = 0;
            bool isFirstSce = true;
            int saveABFileTime = 0;
            int newDownloadSuccFile = 0; //新下载的文件数
            for (int i = 0; i < downloadNum; i++)
            {
                downLoad = new ABDownLoad(remoteVersion.ResURL);
                downList.Add(downLoad);
            }

            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (!isAllLoadComplate)
            {
                loadingSize = 0;
                for (int i = downList.Count; --i >= 0;)
                {
                    downLoad = downList[i];
                    if (downLoad.IsCanDownload)
                    {
                        if (downLoad.IsDownload) //加到下载完成列表
                        {
                            newDownloadSuccFile++;
                        }

                        loadCompleteSize += downLoad.ByteDownloaded;
                        if (needUpdateList.Count > 0)
                        {
                            file = needUpdateList.Dequeue();
                            downLoad.DownloadAsync(file).Run();
                            await CTask.WaitForNextFrame();
                        }
                        else
                        {
                            downList.RemoveAt(i);
                            break;
                        }
                    }
                    else
                    {
                        loadingSize += downLoad.ByteDownloaded;
                    }
                }
                totalLoadSize = loadCompleteSize + loadingSize + downSizeVer;
                downTime += sw.ElapsedMilliseconds;
                sw.Reset();
                sw.Start();
                if (isFirstSce)
                    currDownSize = totalLoadSize;
                if (downTime > 1000)
                {
                    if (totalLoadSize > secDownSize)
                        currDownSize = totalLoadSize - secDownSize;
                    else
                        currDownSize = 0;
                    secDownSize = totalLoadSize;
                    downTime -= 1000;
                    isFirstSce = false;
                    saveABFileTime++;
                    if (saveABFileTime > 1 && newDownloadSuccFile > 0 && downList.Count > 0)
                    {
                        saveABFileInfo(false);
                        saveABFileTime = 0;
                        newDownloadSuccFile = 0;
                    }
                }

                str = string.Format(VerCheckLang.Version_Update, getSizeString(totalLoadSize), getSizeString(totalSizeVer), getSizeString(currDownSize));
                SetInfo(str, (totalLoadSize) / (float)totalSizeVer);
                await CTask.WaitForNextFrame();
                if (downList.Count == 0)
                {
                    saveABFileInfo(true);
                    isAllLoadComplate = true;
                    sw.Stop();
                    SetInfo(VerCheckLang.Version_Update_Complate, 1);
                    await CTask.WaitForSeconds(0.5f);
                    SetComplate(remoteABRes);
                }
            }
        }

        /// <summary>
        /// 保存ABFile信息
        /// </summary>
        private void saveABFileInfo(bool isAllUpdate)
        {
            string path = Path.Combine(AppSetting.PersistentDataPath, AppSetting.ABFiles);
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                StreamWriter sWriter = new StreamWriter(fs);//Encoding.GetEncoding("UTF-8")
                StringBuilder sb = new StringBuilder();
                sb.AppendLine((isAllUpdate ? remoteABRes.Version : 0) + "|" + remoteABRes.VersionData);
                foreach (ABResFile file in remoteABRes.dicFileInfo.Values)
                {
                    sb.AppendLine(file.GetFileString());
                }
                sWriter.Write(sb);
                sWriter.Flush();
                sWriter.Close();
            }
        }

        /// <summary>
        /// 获取资源版本
        /// </summary>
        /// <returns></returns>
        public async CTask<bool> CheckRemoteUpdate()
        {
            string versionFilesURL = AppSetting.VersionURL + AppSetting.VersionFile;
            UnityWebRequest request = UnityWebRequest.Get(versionFilesURL);
            await request.SendWebRequest();
            if (request.error != null)
                return false;

            List<VersionInfo> verInfos = LitJson.JsonMapper.ToObject<List<VersionInfo>>(request.downloadHandler.text);
            string platformName = AppSetting.PlatformName.ToLower();
            foreach (VersionInfo ver in verInfos)
            {
                if (ver.Platform.ToLower() == platformName)
                    return ver.ResVersion > ResVersion && ver.IsForcedUpdate;
            }
            return false;
        }

        /// <summary>
        /// 显示错误提示
        /// </summary>
        /// <param name="error"></param>
        public void ShowError(string error)
        {
            checkUI.Confirm(null, null, error);
        }
        private string getSizeString(ulong size)
        {
            if (size > 1048576)
                return (size / 1048576f).ToString("f2") + "M";
            else
                return (size / 1024f).ToString("f2") + "K";
        }
    }
}
