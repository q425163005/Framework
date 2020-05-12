/// <summary>
/// 工具生成，不要修改
/// </summary>
using System.Collections.Generic;
using CSF.Tasks;
using UnityEngine;

namespace HotFix_Project
{
    public partial class ConfigMgr
    {
        private bool IsCSV = false;
        /// <summary> 任务</summary>
        public readonly Dictionary<object, TaskConfig> dicTask = new Dictionary<object, TaskConfig>();
        /// <summary> 常规任务_类型名</summary>
        public readonly Dictionary<object, LanguageConfig> dicLanguage = new Dictionary<object, LanguageConfig>();
        /// <summary> 新手指引</summary>
        public readonly Dictionary<object, GuideConfig> dicGuide = new Dictionary<object, GuideConfig>();
        /// <summary> 新手指引步骤</summary>
        public readonly Dictionary<object, GuideStepConfig> dicGuideStep = new Dictionary<object, GuideStepConfig>();
        /// <summary> 指引战斗关</summary>
        public readonly Dictionary<object, GuideLevelConfig> dicGuideLevel = new Dictionary<object, GuideLevelConfig>();
        /// <summary> 功能开放</summary>
        public readonly Dictionary<object, FunOpenConfig> dicFunOpen = new Dictionary<object, FunOpenConfig>();
        /// <summary> 物品配置</summary>
        public readonly Dictionary<object, ItemConfig> dicItem = new Dictionary<object, ItemConfig>();
        /// <summary> 常规特效</summary>
        public readonly Dictionary<object, EffectConfig> dicEffect = new Dictionary<object, EffectConfig>();

        /// <summary> 系统设置</summary>
        public SettingConfig settingConfig;

        public async CTask Initialize()
        {
            readConfig(dicTask).Run();
            readConfig(dicLanguage).Run();
            readConfig(dicGuide).Run();
            readConfig(dicGuideStep).Run();
            readConfig(dicGuideLevel).Run();
            readConfig(dicFunOpen).Run();
            readConfig(dicItem).Run();
            readConfig(dicEffect).Run();

            //读取竖表配置
            settingConfig = await readConfigV<SettingConfig>();

            //等待全部加载完再执行自定义解析
            await waitLoadComplate();
            customRead();
        }
    }
}
