using LitJson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace MapEditor
{
    public partial class ConfigMgr
    {
        private int loadCount = 0; //加载资源数
        private int loadedCount = 0; //已经加载资源数

        /// <summary>
        /// 配置表资源文件
        /// </summary>
        private string configAssetbundle = CSF.AppSetting.ConfigBundleDir.TrimEnd('/');
        /// <summary>
        /// 读取配置表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        private  void readConfig<T>(Dictionary<object, T> source) where T : BaseConfig
        {
            loadCount += 1;
            string fileName = typeof(T).Name;
            string path = "Assets/GameRes/BundleRes/Data/Config/";
            string configObj =  File.ReadAllText(path + fileName + ".txt",System.Text.Encoding.UTF8);
            //string configObj=  TextAsset configObj = AssetDatabase.LoadAssetAtPath(path + fileName+ ".txt", typeof(TextAsset)) as TextAsset;
            //Debug.Log(path + fileName + ".txt");
            if (!string.IsNullOrEmpty(configObj))
            {
                string strconfig = configObj;
                List<T> list = JsonMapper.ToObject<List<T>>(strconfig);
                for (int i = 0; i < list.Count; i++)
                {
                    if (source.ContainsKey(list[i].UniqueID))
                        Debug.LogError($"表[{fileName}]中有相同键({list[i].UniqueID})");
                    else
                        source.Add(list[i].UniqueID, list[i]);
                }
            }
            else
            {
                Debug.LogError($"配置文件不存在{fileName}");
            }
            loadedCount += 1;
        }
        
    
        /// <summary>
        /// 重新加载配置表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public async Task ReloadConfig<T>(Dictionary<object, T> source) where T : BaseConfig
        {
            string fileName = typeof(T).Name;
            source.Clear();
            UnityEngine.Object configObj = await CSF.Mgr.Assetbundle.LoadAsset<UnityEngine.Object>(configAssetbundle, fileName);
            if (configObj != null)
            {
                string strconfig = configObj.ToString();
                List<T> list = JsonMapper.ToObject<List<T>>(strconfig);
                for (int i = 0; i < list.Count; i++)
                {
                    if (source.ContainsKey(list[i].UniqueID))
                        Debug.LogError($"表[{fileName}]中有相同键({list[i].UniqueID})");
                    else
                        source.Add(list[i].UniqueID, list[i]);
                }
            }
            else
            {
                Debug.LogError($"配置文件不存在{fileName}");
            }
        }
         


        /// <summary>
        /// 读取竖表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        private async Task<T> readConfigV<T>() where T : BaseConfig
        {
            string fileName = typeof(T).Name;
            UnityEngine.Object configObj = await CSF.Mgr.Assetbundle.LoadAsset<UnityEngine.Object>(configAssetbundle, fileName);
            if (configObj != null)
            {
                string strconfig = configObj.ToString();                
                List<T> list = JsonMapper.ToObject<List<T>>(strconfig);
                if (list.Count > 0)
                    return list[0];
            }
            else
            {
                Debug.LogError($"配置文件不存在{fileName}");                
            }
            return null;
        }

        private async Task waitLoadComplate()
        {
            await new WaitUntil(() => { return loadCount == loadedCount; });
        }

        //读取地图配置
        private List<MapConfig> LoadMapConfig()
        {
            string path = Path.GetFullPath("../../ProtoConfig/Config/Maps/MapConfig.txt");
            FileInfo info = new FileInfo(path);
            if (!info.Exists)return null;
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(fs, Encoding.GetEncoding("UTF-8")))
                {
                    string content = reader.ReadToEnd();
                    List<MapConfig> list = JsonMapper.ToObject<List<MapConfig>>(content);
                    return list;
                }
            }
        }
        public void SaveMapConfig()
        {
            string path = Path.GetFullPath("../../ProtoConfig/Config/Maps/MapConfig.txt");
            FileInfo info = new FileInfo(path);
            List<MapConfig> list = new List<MapConfig>();
            foreach (var dic in dicMapsList.Values)
            {
                foreach (var l in dic.Values)
                    list.Add(l);
            }
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter sWriter = new StreamWriter(fs, Encoding.GetEncoding("UTF-8")))
                {
                    sWriter.WriteLine(JsonMapper.ToJson(list)); 
                }
            }
        }
    }
}
