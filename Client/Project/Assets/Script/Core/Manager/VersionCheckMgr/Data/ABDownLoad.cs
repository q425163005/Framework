using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;
using System.IO;
using CSF.Tasks;

namespace CSF
{
    public class ABDownLoad
    {
        TaskCompletionSource<bool> tcs;
        UnityWebRequest request;
        bool isDownload = false;

        public ABResFile ABFile;
        private string rootURL;

        private int downErrorNum = 0;
        public ABDownLoad(string rooturl)
        {
            rootURL = rooturl;
        }
        public ulong ByteDownloaded
        {
            get
            {
                if (request == null|| request.downloadProgress==0)
                    return 0;
                return request.downloadedBytes;
            }
        }
        public bool IsCanDownload => isDownload|| request==null;

        public bool IsDownload => isDownload;

        public byte[] Data => request.downloadHandler.data;


        public async CTask DownloadAsync(ABResFile file, bool isReLoad = false)
        {
            ABFile = file;
            isDownload = false;
            if(!isReLoad)
                downErrorNum = 0;
            string url = rootURL + ABFile.File;
            request = UnityWebRequest.Get(url);
            await request.SendWebRequest();
            if (request.error != null) //文件下载失败
            {
                CLog.Error($"DownLoad Error:{url}" + "  " + request.error);
                downErrorNum++;
                await CTask.WaitForSeconds(2);
                DownloadAsync(file, true).Run(); //尝试重新下载
                if (downErrorNum >= 5)
                {
                    downErrorNum = 0;
                    Mgr.VersionCheck.ShowError(string.Format(VerCheckLang.Version_Update_Error,file.File));
                }
                return;
            }
            string downMD5 = MD5Utils.MD5ByteFile(Data);
            if (downMD5 == file.MD5)
            {
                try
                {
                    //保存文件
                    string path = Path.Combine(AppSetting.PersistentDataPath, file.File);
                    FileInfo info = new FileInfo(path);
                    if (!Directory.Exists(info.DirectoryName))
                        Directory.CreateDirectory(info.DirectoryName);

                    using (FileStream fs = new FileStream(path, FileMode.Create))
                    {
                        fs.Write(Data, 0, Data.Length);
                        fs.Flush();
                    }
                   
                    isDownload = true;
                }
                catch (Exception ex)
                {
                    CLog.Error($"文件保存失败！！！！{file.File}\n{ex.Message}");
                    await CTask.WaitForSeconds(2);
                    DownloadAsync(file, true).Run();     //尝试重新下载
                }
            }
            else //MD5效验失败
            {
                downErrorNum++;
                if (downErrorNum >= 5)
                {
                    downErrorNum = 0;
                    Mgr.VersionCheck.ShowError(string.Format(VerCheckLang.Version_Update_MD5Error, file.File));
                }
                CLog.Error($"文件MD5值错误 配置MD5:{downMD5}  实际下载MD5:{file.MD5}");
                await CTask.WaitForSeconds(2); ;
                DownloadAsync(file, true).Run();     //尝试重新下载
            }
        }

    }
}
