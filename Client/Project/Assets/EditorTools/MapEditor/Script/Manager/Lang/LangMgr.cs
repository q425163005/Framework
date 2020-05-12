
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MapEditor
{
    public class LangMgr
    {
        /// <summary>
        /// 默认语言
        /// </summary>
        private ELangType defaultType = ELangType.ZH_CN;
        private LanguageConfig config;
        public LangMgr()
        {
            //defaultType = (ELangType)PlayerPrefs.GetInt("ELangType", (int)ELangType.ZH_TW);
        }
        /// <summary>
        /// 设置语言
        /// </summary>
        /// <param name="type"></param>
        public ELangType LangType
        {
            get
            {
                return defaultType;
            }
            set
            {
                if (defaultType != value)
                {
                    defaultType = value;                    
                    CSF.Mgr.UI.RefreshLang();
                    PlayerPrefs.SetInt("ELangType", (int)value);
                    PlayerPrefs.Save();
                }
            }           
        }
        /// <summary>
        /// 跟据Key值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get(string key)
        {
            return Get(key, defaultType).Replace("\\n", "\n");
        }
        public string GetFormat(string key, params object[] args)
        {
            return GetFormat(key, defaultType, args);
        }
        public string GetFormat(string key, ELangType type,params object[] args)
        {
            string str = Get(key, type).Replace("\\n", "\n");
            str = string.Format(str, args);
            return str;
        }
        public string Get(string key, ELangType type)
        {
            if (MapEditor.I.Config.dicLanguage.TryGetValue(key, out config))
            {
                switch (type)
                {
                    case ELangType.ZH_CN:
                        return config.zh_cn;
                    case ELangType.ZH_TW:
                        return config.zh_tw;
                    case ELangType.EN:
                        return config.en;
                    case ELangType.JA:
                        return config.ja;
                    case ELangType.KO:
                        return config.ko;
                }
            }
            return $"{ key}";
        }
    }
}
