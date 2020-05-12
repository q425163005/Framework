using CSF.Tasks;
using LitJson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace HotFix_Project
{
    public partial class ConfigMgr
    {
        private int loadCount = 0; //加载资源数
        private int loadedCount = 0; //已经加载资源数

        private char splitFieldChar = '∴';
        /// <summary>
        /// 配置表资源文件
        /// </summary>
        private string configAssetbundle = CSF.AppSetting.ConfigBundleDir.TrimEnd('/');
        /// <summary>
        /// 读取配置表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        private async CTask readConfig<T>(Dictionary<object, T> source) where T : BaseConfig, new()
        {
            loadCount += 1;
            string fileName = typeof(T).Name;
            UnityEngine.Object configObj = await CSF.Mgr.Assetbundle.LoadAsset<UnityEngine.Object>(configAssetbundle, fileName);
            if (configObj != null)
            {
                string strconfig = configObj.ToString();
                if (IsCSV)
                {
                    using (StringReader sr = new StringReader(strconfig))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            T config = CSF.ConfigUtils.ToObject<T>(line.Split(splitFieldChar));
                            if (source.ContainsKey(config.UniqueID))
                                CLog.Error($"表[{fileName}]中有相同键({config.UniqueID})");
                            else
                                source.Add(config.UniqueID, config);
                        }
                    }
                }
                else
                {
                    List<T> list = JsonMapper.ToObject<List<T>>(strconfig);
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (source.ContainsKey(list[i].UniqueID))
                            CLog.Error($"表[{fileName}]中有相同键({list[i].UniqueID})");
                        else
                            source.Add(list[i].UniqueID, list[i]);
                    }
                }
            }
            else
            {
                CLog.Error($"配置文件不存在{fileName}");
            }
            loadedCount += 1;
        }

        /// <summary>
        /// 重新加载配置表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public async CTask ReloadConfig<T>(Dictionary<object, T> source) where T : BaseConfig, new()
        {
            string fileName = typeof(T).Name;
            source.Clear();
            UnityEngine.Object configObj = await CSF.Mgr.Assetbundle.LoadAsset<UnityEngine.Object>(configAssetbundle, fileName);
            if (configObj != null)
            {
                string strconfig = configObj.ToString();
                if (IsCSV)
                {
                    using (StringReader sr = new StringReader(strconfig))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            T config = CSF.ConfigUtils.ToObject<T>(line.Split(splitFieldChar));
                            if (source.ContainsKey(config.UniqueID))
                                CLog.Error($"表[{fileName}]中有相同键({config.UniqueID})");
                            else
                                source.Add(config.UniqueID, config);
                        }
                    }
                }
                else
                {
                    List<T> list = JsonMapper.ToObject<List<T>>(strconfig);
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (source.ContainsKey(list[i].UniqueID))
                            CLog.Error($"表[{fileName}]中有相同键({list[i].UniqueID})");
                        else
                            source.Add(list[i].UniqueID, list[i]);
                    }
                }
            }
            else
            {
                CLog.Error($"配置文件不存在{fileName}");
            }
        }



        /// <summary>
        /// 读取竖表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        private async CTask<T> readConfigV<T>() where T : BaseConfig, new()
        {
            string fileName = typeof(T).Name;
            UnityEngine.Object configObj = await CSF.Mgr.Assetbundle.LoadAsset<UnityEngine.Object>(configAssetbundle, fileName);
            if (configObj != null)
            {
                string strconfig = configObj.ToString();
                if (IsCSV)
                {
                    using (StringReader sr = new StringReader(strconfig))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            T config = CSF.ConfigUtils.ToObject<T>(line.Split(splitFieldChar));
                            return config;
                        }
                    }
                }
                else
                {
                    List<T> list = JsonMapper.ToObject<List<T>>(strconfig);
                    if (list.Count > 0)
                        return list[0];
                }
            }
            else
            {
                CLog.Error($"配置文件不存在{fileName}");
            }
            return default(T);
        }

        private async CTask waitLoadComplate()
        {
            await CTask.WaitUntil(() => { return loadCount == loadedCount; });
        }
    }
}
