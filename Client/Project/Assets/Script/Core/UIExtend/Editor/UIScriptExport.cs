using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CSF
{
    /// <summary>
    /// UI脚本导出
    /// </summary>
    public class UIScriptExport
    {
        /// <summary>导出UI</summary>
        /// <param name="ui"></param>
        public static void ExportUIScript(UIOutlet ui)
        {
            string modName = SceneManager.GetActiveScene().name;
            string uiName = ui.gameObject.name;
            CreateUI(modName, uiName, ui);
            CreateUIView(modName, uiName, ui);
            ////如果UI中含有Item,导出Item
            //foreach(UIOutlet.OutletInfo info in ui.OutletInfos)
            //{
            //    if (info.Name.EndsWith("Item"))
            //    {
            //        UIOutlet itemOut = (info.Object as GameObject).GetComponent<UIOutlet>();
            //        if (itemOut != null)
            //        {
            //            ExportItemScript(itemOut);
            //        }
            //    }
            //}
        }

        /// <summary>导出私有Item</summary>
        public static void ExportItemScript(UIOutlet ui)
        {
            string modName = SceneManager.GetActiveScene().name;
            string uiName = ui.gameObject.name.Replace("prefab", ""); ;
            CreateItem(modName, uiName, ui);
            CreateItemView(modName, uiName, ui);
        }


        /// <summary>导出公有Item</summary>
        public static void ExportPublicItemScript(UIOutlet ui)
        {
            string modName = "PublicItem";
            string uiName = ui.gameObject.name.Replace("prefab", "");
            uiName = uiName.Substring(0, uiName.Length - 1);
            CreateItem(modName, uiName, ui);
            CreateItemView(modName, uiName, ui);
        }

        #region 创建UI       
        private static void CreateUI(string modName, string uiName, UIOutlet ui)
        {
            string saveFilePath = $"{ExportScriptDir}{modName}/UI/{uiName}.cs";
            StringBuilder eventAddStrs = new StringBuilder();
            StringBuilder eventStrs = new StringBuilder();
            UIOutlet.OutletInfo info;
            string objType;
            for (int i = ui.OutletInfos.Count; --i >= 0;)
            {
                info = ui.OutletInfos[i];
                if (info == null) continue;
                objType = getTypeName(info.ComponentType);
                if (info.Object.name.StartsWith("btn")) //自动生成按钮事件
                {
                    eventAddStrs.AppendLine($"            {info.Object.name}.AddClick({info.Object.name}_Click);   //");
                    eventStrs.AppendLine($@"        /// <summary></summary>
        void {info.Object.name}_Click()
        {{
        }}");
                }
            }

            string fieldStr = $@"using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
namespace {AppSetting.HotFixName}.{modName}
{{
    public partial class {uiName} : BaseUI
    {{
        /// <summary>XXXX界面</summary>
        public {uiName}()
        {{
            UINode    = EUINode.UIWindow; //UI节点
            OpenAnim  = EUIAnim.None;     //UI打开效果
            CloseAnim = EUIAnim.None;     //UI关闭效果 
        }}
        
        /// <summary>添加事件监听</summary>
        protected override void Awake()
        {{ 
{eventAddStrs}
        }}
        
         /// <summary>刷新</summary>
        public override void Refresh()
        {{
        }}

{eventStrs}
        /// <summary>释放UI引用</summary>
        public override void Dispose()
        {{
        }}
    }}
}}
";
            ToolsHelper.SaveFile(saveFilePath, fieldStr, false);
        }
        #endregion

        #region 创建UIView 
        private static void CreateUIView(string modName, string uiName, UIOutlet ui)
        {
            string saveFilePath = $"{ExportScriptDir}{modName}/UI/View/{uiName}View.cs";

            StringBuilder fieldStrs = new StringBuilder();
            StringBuilder getStrs = new StringBuilder();
            UIOutlet.OutletInfo info;
            string useSpine = string.Empty;
            string objType;
            for (int i = ui.OutletInfos.Count; --i >= 0;)
            {
                info = ui.OutletInfos[i];
                if (info == null) continue;
                objType = getTypeName(info.ComponentType);
                fieldStrs.AppendLine($"        private {objType} {info.Name};");
                if(objType=="GameObject")
                    getStrs.AppendLine($@"            {info.Name} = Get(""{info.Name}"");");
                else
                    getStrs.AppendLine($@"            {info.Name} = Get<{objType}>(""{info.Name}"");");
                if (objType == "SkeletonGraphic")
                    useSpine = "using Spine.Unity;";
            }

            string fieldStr = $@"//工具生成不要修改
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
{useSpine}
namespace {AppSetting.HotFixName}.{modName}
{{
    public partial class {uiName} : BaseUI
    {{
{fieldStrs} 
        /// <summary>初始化UI控件</summary>
        protected override void InitializeComponent()
        {{
{getStrs}
        }}
    }}
}}
";
            ToolsHelper.SaveFile(saveFilePath, fieldStr, true);
        }
        #endregion

        #region 创建Item       
        private static void CreateItem(string modName, string uiName, UIOutlet ui)
        {
            string saveFilePath = $"{ExportScriptDir}{modName}/UI/Item/{uiName}.cs";
            StringBuilder eventAddStrs = new StringBuilder();
            StringBuilder eventStrs = new StringBuilder();
            UIOutlet.OutletInfo info;
            string objType;
            for (int i = ui.OutletInfos.Count; --i >= 0;)
            {
                info = ui.OutletInfos[i];
                if (info == null) continue;
                objType = getTypeName(info.ComponentType);
                if (info.Object.name.StartsWith("btn")) //自动生成按钮事件
                {
                    eventAddStrs.AppendLine($"            {info.Object.name}.AddClick({info.Object.name}_Click);   //");
                    eventStrs.AppendLine($@"        /// <summary></summary>
        void {info.Object.name}_Click()
        {{
        }}");
                }
            }

            string selClick = "             gameObject.AddClick(self_Click);     //当前对象点击事件";
            if (ui.GetComponent<Button>() != null)
                selClick = "             gameObject.GetComponent<Button>().AddClick(self_Click);  //当前对象点击事件";
            string fieldStr = $@"using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
namespace {AppSetting.HotFixName}.{modName}
{{
    public partial class {uiName} : BaseItem
    {{
        public Action<{uiName}> onClick; //Item点击事件

        /// <summary>添加事件监听</summary>
        protected override void Awake()
        {{
{selClick}             {eventAddStrs}
        }}
        /// <summary>设置数据</summary>
        /*public void SetData()
        {{
            Refresh();
        }}*/
        /// <summary>刷新Item</summary>
        public override void Refresh()
        {{
        }}

        /// <summary>当前对象点击事件</summary>
        void self_Click()
        {{
            onClick?.Invoke(this);
        }}
{ eventStrs}
        /// <summary>释放Item引用</summary>
        public override void Dispose()
        {{
            onClick = null;
        }}
    }}
}}
";
            ToolsHelper.SaveFile(saveFilePath, fieldStr, false);
        }
        #endregion

        #region 创建UIView 
        private static void CreateItemView(string modName, string uiName, UIOutlet ui)
        {
            string saveFilePath = $"{ExportScriptDir}{modName}/UI/Item/View/{uiName}View.cs";

            StringBuilder fieldStrs = new StringBuilder();
            StringBuilder getStrs = new StringBuilder();
            StringBuilder skinStrsRect = new StringBuilder();
            StringBuilder skinStrs = new StringBuilder();
            UIOutlet.OutletInfo info;
            string useSpine = string.Empty;
            string objType;
            for (int i = ui.OutletInfos.Count; --i >= 0;)
            {
                info = ui.OutletInfos[i];
                if (info == null) continue;
                objType = getTypeName(info.ComponentType);
                fieldStrs.AppendLine($"        private {objType} {info.Name};");
                if (objType == "GameObject")
                    getStrs.AppendLine($@"            {info.Name} = Get(""{info.Name}"");");
                else
                    getStrs.AppendLine($@"            {info.Name} = Get<{objType}>(""{info.Name}"");");

                if(objType == "SkeletonGraphic")
                    useSpine = "using Spine.Unity;";
            }

            UISkin skin = ui.gameObject.GetComponent<UISkin>();
            if (skin != null)
            {
                skinStrs.AppendLine($@"           HideAllObject();
          switch (useSkinId)
            {{");
                foreach (var item in skin.dicSkinInfo)
                {
                    skinStrs.AppendLine($@"              case {item.id}:");
                    foreach (var rectInfo in item.objRectInfo)
                    {
                        string RectName = rectInfo.gameObject.name;
                        if (rectInfo.gameObject.name == skin.gameObject.name)
                            RectName = "this";
                        skinStrs.AppendLine($@"                 {RectName}.gameObject.SetVisible(true);");
                        skinStrs.AppendLine($@"                 RectTransform Skin{item.id}_{RectName}Rect={RectName}.gameObject.GetComponent<RectTransform>();");
                        skinStrs.AppendLine($@"                 Skin{item.id}_{RectName}Rect.anchoredPosition=new Vector2({rectInfo.anchoredPosition.x}f,{rectInfo.anchoredPosition.y}f);");
                        skinStrs.AppendLine($@"                 Skin{item.id}_{RectName}Rect.anchorMax=new Vector2({rectInfo.anchorMax.x}f,{rectInfo.anchorMax.y}f);");
                        skinStrs.AppendLine($@"                 Skin{item.id}_{RectName}Rect.anchorMin=new Vector2({rectInfo.anchorMin.x}f,{rectInfo.anchorMin.y}f);");
                        skinStrs.AppendLine($@"                 Skin{item.id}_{RectName}Rect.sizeDelta=new Vector2({rectInfo.sizeDelta.x}f,{rectInfo.sizeDelta.y}f);");
                        skinStrs.AppendLine($@"                 Skin{item.id}_{RectName}Rect.pivot=new Vector2({rectInfo.pivot.x}f,{rectInfo.pivot.y}f);");
                    }
                    skinStrs.AppendLine($@"              break;");
                }

                skinStrs.AppendLine($@"              }}");
            }

            string fieldStr = $@"//工具生成不要修改
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
{useSpine}
namespace {AppSetting.HotFixName}.{modName}
{{
    public partial class {uiName} : BaseItem
    {{
{fieldStrs} 
 
        /// <summary>初始化UI控件</summary>
        override protected void InitializeComponent()
        {{
{getStrs}
        }}
        /// <summary>初始化皮肤设置</summary>
        protected override void InitializeSkin()
        {{
{skinStrs}
            
        }}
    }}
}}
";
            ToolsHelper.SaveFile(saveFilePath, fieldStr, true);
        }
        #endregion

        private static string getTypeName(string fullName)
        {
            return fullName.Substring(fullName.LastIndexOf(".") + 1);
        }
        private static string ExportScriptDir
        {
            get { return Path.GetFullPath("../" + AppSetting.HotFixName + "/Module/").Replace("\\", "/"); }
        }
    }

}
