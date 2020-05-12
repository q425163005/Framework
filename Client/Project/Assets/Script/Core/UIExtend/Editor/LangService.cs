using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace CSF
{
    public class LanguageConfig
    {
        /// <summary>
        /// id
        /// 例UI:UILogin.btnLoing
        /// 例表:Test/id或Test/字段/id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 中文
        /// </summary>
        public string zh_cn { get; set; }
        /// <summary>
        /// 繁体
        /// </summary>
        public string zh_tw { get; set; }
        /// <summary>
        /// 英语
        /// </summary>
        public string en { get; set; }
        /// <summary>
        /// 日语
        /// </summary>
        public string ja { get; set; }
        /// <summary>
        /// 韩语
        /// </summary>
        public string ko { get; set; }
    }
    public enum ELangType
    {
        ZH_CN,   //简体中文
        ZH_TW,  //繁体中文
        EN,         //英文
        JA,         //日语
        KO,        //韩语 
    }

    [ExecuteInEditMode]
    public class LangService
    {
        private static Dictionary<string, LanguageConfig> dicLang = new Dictionary<string, LanguageConfig>();
        private static LangService _instance;
        public static LangService Instance
        {
            get { return _instance ?? (_instance = new LangService()); }
        }

        private static ELangType _LangType = ELangType.ZH_TW;
        public ELangType LangType
        {
            get
            {
                _LangType = (ELangType)EditorPrefs.GetInt("Editor_LangType", 0);
                return _LangType;
            }
            set
            {
                _LangType = value;
                EditorPrefs.SetInt("Editor_LangType", (int)value);
                RefAllText();
            }
        }

        public LangService()
        {
            LoadConfig();
        }

        public string Get(string key)
        {
            return Get(key, LangType);
        }
        public string Get(string key, ELangType type)
        {
            LanguageConfig config = null;
            string str = null;
            if (dicLang.TryGetValue(key, out config))
            {
                switch (type)
                {
                    case ELangType.ZH_CN:
                        str = config.zh_cn;
                        break;
                    case ELangType.ZH_TW:
                        str = config.zh_tw;
                        break;
                    case ELangType.EN:
                        str = config.en;
                        break;
                    case ELangType.JA:
                        str = config.ja;
                        break;
                    case ELangType.KO:
                        str = config.ko;
                        break;
                }
            }
            if(str!=null)
                return str.Replace("\\n", "\n");
            return $"Not Find[{ key}]";
        }

        public void RefAllText()
        {
            GameObject go = GameObject.Find("UICanvas");
            UILangText[] list = go.GetComponentsInChildren<UILangText>();
            bool isGameRunState = false;
            if (Application.isPlaying && Mgr.ILR != null)
                isGameRunState = true;
            for (int i = list.Length; --i >= 0;)
            {
                if (isGameRunState)
                    list[i].Value = Mgr.ILR.CallHotFixGetLang(list[i].Key, (int)LangService.Instance.LangType);
                else {
                    list[i].Value = Get(list[i].Key);
                    EditorUtility.SetDirty(list[i].gameObject);
                }                
            }
        }

        /// <summary>
        /// 加载语言表
        /// </summary>
        public void LoadConfig()
        {
            dicLang.Clear();
            string assetPath = AppSetting.ConfigBundleDir.TrimEnd('/') + AppSetting.ExtName;
            string[] assetPaths = AssetDatabase.GetAssetPathsFromAssetBundleAndAssetName(assetPath.ToLower(), "LanguageConfig");
            if (assetPaths.Length > 0)
            {
                UnityEngine.Object target = AssetDatabase.LoadMainAssetAtPath(assetPaths[0]);
                List<LanguageConfig> list = JsonMapper.ToObject<List<LanguageConfig>>(target.ToString());
                for (int i = 0; i < list.Count; i++)
                {
                    if (dicLang.ContainsKey(list[i].id))
                        CLog.Error($"表[LanguageConfig]中有相同键({list[i].id})");
                    else
                        dicLang.Add(list[i].id, list[i]);
                }
            }
        }
    }
}