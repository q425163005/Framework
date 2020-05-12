using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapEditor;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace CSF
{
    public class UIExtendMenu
    {
        [MenuItem("GameObject/★UI扩展★/★创建★/AttriSprite", false, 10)]
        static void CreateAttriSpriteObject(MenuCommand menuCommadn)
        {
            GameObject parent = menuCommadn.context as GameObject;
            if (parent != null && parent.GetComponentInParent<Canvas>() != null)
            {
                GameObject go = new GameObject("AttriSprite");
                GameObjectUtility.SetParentAndAlign(go, parent);
                RectTransform rect = go.AddComponent<RectTransform>();
                rect.sizeDelta=Vector2.zero;
                go.AddComponent<ImageSpriteIndex>();
                go.transform.SetAsFirstSibling();
            }
            else
            {
                ToolsHelper.Log("只能在UI下创建AttriSprite");
            }
        }
        [MenuItem("GameObject/★UI扩展★/★创建★/创建UI", false, 10)]
        static void CreateUIObject(MenuCommand menuCommadn)
        {
            GameObject go     = new GameObject("New UI");
            GameObject parent = GameObject.Find("UICanvas");
            if (parent == null)
                parent = menuCommadn.context as GameObject;
            GameObjectUtility.SetParentAndAlign(go, parent);
            RectTransform rect = go.AddComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            go.AddComponent<SpriteAtlasList>();
            go.AddComponent<UIOutlet>();
            Selection.activeObject = go;
        }

        [MenuItem("GameObject/★UI扩展★/★创建★/创建UI Item", false, 10)]
        static void CreateUIObjectItem(MenuCommand menuCommadn)
        {
            GameObject go     = new GameObject("New Item");
            GameObject parent = GameObject.Find("UICanvas");
            if (parent == null)
                parent = menuCommadn.context as GameObject;

            GameObjectUtility.SetParentAndAlign(go, parent);
            RectTransform rect = go.AddComponent<RectTransform>();
            go.AddComponent<UIOutlet>();
            Selection.activeObject = go;
        }

        [MenuItem("GameObject/★UI扩展★/★创建★/创建UI ItemPlulic", false, 10)]
        static void CreateUIObjectItemPlulic(MenuCommand menuCommadn)
        {
            GameObject go     = new GameObject("New ItemP");
            GameObject parent = GameObject.Find("UICanvas");
            if (parent == null)
                parent = menuCommadn.context as GameObject;

            GameObjectUtility.SetParentAndAlign(go, parent);
            RectTransform rect = go.AddComponent<RectTransform>();
            go.AddComponent<UIOutlet>();
            Selection.activeObject = go;
        }

        [MenuItem("GameObject/★UI扩展★/★创建★/创建多语言 Text", false, 10)]
        static void CreateTextLang(MenuCommand menuCommadn)
        {
            GameObject parent = menuCommadn.context as GameObject;
            if (parent != null && parent.GetComponentInParent<Canvas>() != null)
            {
                GameObject go = new GameObject("New Lang Text");
                GameObjectUtility.SetParentAndAlign(go, parent);
                go.AddComponent<UILangText>();
                Text          txt  = go.AddComponent<Text>();
                RectTransform rect = go.GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(200, 22);
                txt.alignment  = TextAnchor.MiddleLeft;
                txt.fontSize   = 22;
                Color outColor = Color.white;
                ColorUtility.TryParseHtmlString("#FFFFFF", out outColor);
                txt.color                = outColor;
                txt.text                 = "New Lang Text";
                txt.resizeTextForBestFit = true;
                txt.supportRichText      = true;
                Font font = AssetDatabase.LoadAssetAtPath<Font>("Assets/GameRes/BundleRes/Font/Default.TTF");
                txt.font = font;
                txt.fontStyle = FontStyle.Bold;
            }
            else
            {
                ToolsHelper.Log("只能在UI下创建LangText");
            }
        }

        [MenuItem("GameObject/★UI扩展★/★创建★/创建 Text", false, 10)]
        static void CreateText(MenuCommand menuCommadn)
        {
            GameObject parent = menuCommadn.context as GameObject;
            if (parent != null && parent.GetComponentInParent<Canvas>() != null)
            {
                GameObject go = new GameObject("New Text");
                GameObjectUtility.SetParentAndAlign(go, parent);
                Text          txt  = go.AddComponent<Text>();
                RectTransform rect = go.GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(200, 22);
                txt.alignment  = TextAnchor.MiddleLeft;
                txt.fontSize   = 22;
                txt.text       = "New Text";
                Color outColor = Color.white;
                ColorUtility.TryParseHtmlString("#FFFFFF", out outColor);
                txt.color                = outColor;
                txt.resizeTextForBestFit = true;
                txt.supportRichText      = true;
                Font font = AssetDatabase.LoadAssetAtPath<Font>("Assets/GameRes/BundleRes/Font/Default.TTF");
                txt.font = font;
                txt.fontStyle = FontStyle.Bold;
            }
            else
            {
                ToolsHelper.Log("只能在UI下创建Text");
            }
        }

        [MenuItem("GameObject/★UI扩展★/★创建★/创建多语言 Button", false, 10)]
        static void CreateButtonLang(MenuCommand menuCommadn)
        {
            GameObject parent = menuCommadn.context as GameObject;
            if (parent != null && parent.GetComponentInParent<Canvas>() != null)
            {
                GameObject goBtn = new GameObject("New Button");
                GameObjectUtility.SetParentAndAlign(goBtn, parent);
                Image image = goBtn.AddComponent<Image>();
                image.sprite =
                    AssetDatabase.LoadAssetAtPath<Sprite>("Assets/GameRes/ArtRes/UIAtlas/PublicButton/Btn1_BG.png");
                image.SetNativeSize();

                GameObject BtnIcon = new GameObject("Icon");
                BtnIcon.transform.SetParent(goBtn.transform, false);
                Image IconImg = BtnIcon.AddComponent<Image>();
                IconImg.sprite =
                    AssetDatabase.LoadAssetAtPath<Sprite>("Assets/GameRes/ArtRes/UIAtlas/PublicButton/Btn1_1.png");
                RectTransform IconImgrect = IconImg.GetComponent<RectTransform>();
                IconImgrect.anchorMin = Vector2.zero;
                IconImgrect.anchorMax = Vector2.one;
                IconImgrect.offsetMin = Vector2.zero;
                IconImgrect.offsetMax = Vector2.zero;
                IconImgrect.sizeDelta = new Vector2(-16, -16);


                Button btn = goBtn.AddComponent<Button>();
                btn.image = IconImg;

                GameObject goTxt = new GameObject("Text");
                GameObjectUtility.SetParentAndAlign(goTxt, goBtn);
                goTxt.AddComponent<UILangText>();
                Text  txt   = goTxt.AddComponent<Text>();
                Color color = Color.black;
                ColorUtility.TryParseHtmlString("#FFFFFF", out color);
                txt.color = color;
                RectTransform rect = goTxt.GetComponent<RectTransform>();

                rect.anchorMin = Vector2.zero;
                rect.anchorMax = Vector2.one;
                rect.offsetMin = Vector2.zero;
                rect.offsetMax = Vector2.zero;
                rect.sizeDelta = new Vector2(-40, -34);
                txt.fontSize   = 24;
                txt.alignment  = TextAnchor.MiddleCenter;
                txt.text       = "Lang Button";
                //txt.resizeTextForBestFit = true;
                //txt.supportRichText = true;
                Font font = AssetDatabase.LoadAssetAtPath<Font>("Assets/GameRes/BundleRes/Font/Default.TTF");
                txt.font = font;
                txt.fontStyle = FontStyle.Bold;
                Shadow shadow = goTxt.AddComponent<Shadow>();
                shadow.effectColor    = new Color(0, 0, 0, 1);
                shadow.effectDistance = new Vector2(1, -3);

            }
            else
            {
                ToolsHelper.Log("只能在UI下创建LangText");
            }
        }

        [MenuItem("GameObject/★UI扩展★/★生成★/生成Item脚本", false, 21)]
        static void CreateItemScript(MenuCommand menuCommadn)
        {
            GameObject target = menuCommadn.context as GameObject;
            if (target != null && (target.name.EndsWith("Item")))
            {
                UIOutlet uiObj = target.GetComponent<UIOutlet>();
                if (uiObj != null)
                {
                    UIScriptExport.ExportItemScript(uiObj);
                    ToolsHelper.Log("生成成功!!!");
                    return;
                }
            }

            if (target != null && (target.name.EndsWith("ItemP")))
            {
                ToolsHelper.Log("对于ItemP类型的Item，请在对应预设文件夹生成Item");
                return;
            }

            ToolsHelper.Log("请选择有效果的Item对象!!!,Item包含UIOutlet脚本，并且以Item命名结尾");
        }

        [MenuItem("GameObject/★UI扩展★/★生成★/生成 UI 脚本", false, 21)]
        static void CreateUIScript(MenuCommand menuCommadn)
        {
            GameObject target = menuCommadn.context as GameObject;
            if (target != null && target.transform.parent.name == "UICanvas" && target.name.EndsWith("UI"))
            {
                if (target.name.StartsWith("New UI"))
                {
                    ToolsHelper.Log("请修改UI名称!!!");
                    return;
                }

                UIOutlet uiObj = target.GetComponent<UIOutlet>();
                if (uiObj != null)
                {
                    UIScriptExport.ExportUIScript(uiObj);
                    ToolsHelper.Log("生成成功!!!");
                    return;
                }
            }

            ToolsHelper.Log("请选择有效果的UI对象!!!");
        }
        //[MenuItem("GameObject/★UI扩展★/生成公有Item 脚本", false, 22)]
        //static void CreateItemPublicScript(MenuCommand menuCommadn)
        //{
        //    GameObject target = menuCommadn.context as GameObject;
        //    if (target != null && target.name.EndsWith("ItemP"))
        //    {
        //        UIOutlet uiObj = target.GetComponent<UIOutlet>();
        //        if (uiObj != null)
        //        {
        //            UIScriptExport.ExportPublicItemScript(uiObj);
        //            ToolsHelper.Log("生成成功!!!");
        //            return;
        //        }
        //    }
        //    ToolsHelper.Log("请选择有效果的ItemP对象!!!");
        //}

        //[MenuItem("Assets/★UI扩展★/生成公有Item 脚本", false, 22)]
        //static void CreateItemPublicScript()
        //{
        //    GameObject target = Selection.activeGameObject;
        //    if (target != null && target.name.EndsWith("ItemP"))
        //    {
        //        string   Path = AssetDatabase.GetAssetPath(target);
        //        string[] str  = Path.Split('/');
        //        if (str.Length < 2 || str[str.Length - 2] != "PublicItem")
        //        {
        //            ToolsHelper.Log("请将公有的Item放在PublicItem文件夹下");
        //            return;
        //        }

        //        UIOutlet uiObj = target.GetComponent<UIOutlet>();
        //        if (uiObj != null)
        //        {
        //            UIScriptExport.ExportPublicItemScript(uiObj);
        //            ToolsHelper.Log("生成成功!!!");
        //            return;
        //        }
        //    }

        //    ToolsHelper.Log("请选择有效果的ItemP对象!!!");
        //}


        [MenuItem("GameObject/★UI扩展★/★增加组件★/BulidVisualArea", false, 11)]
        static void CreateBulidVisualArea()
        {
            var goRoot = Selection.activeGameObject;
            if (goRoot == null)
                return;

            var button = goRoot.GetComponent<Button>();

            if (button == null)
            {
                Debug.Log("Selecting Object is not a button!");
                return;
            }

            var           polygon = new GameObject("BulidVisualArea");
            RectTransform rect    = polygon.AddComponent<RectTransform>();
            polygon.transform.SetParent(goRoot.transform, false);
            rect.pivot     = Vector2.zero;
            rect.sizeDelta = new Vector2(100, 100);
            Image image = polygon.AddComponent<Image>();
            image.raycastTarget = false;
            Color outColor = new Color32(255, 233, 8, 89);
            image.color = outColor;
        }
        
        
        
        [MenuItem("GameObject/★UI扩展★/★增加组件★/UI背景适配脚本", false, 11)]
        static void CreateUIBGAdaptive()
        {
            var goRoot = Selection.activeGameObject;
            if (goRoot == null || goRoot.GetComponent<RectTransform>() == null)
            {
                CLog.Error("只能加在RectTransform上");
                return;
            }
            goRoot.AddComponent<UIAdaptive>();
        }
      

        [MenuItem("GameObject/★添加描边阴影★", false, -100)]
        static void CreateUIFont(MenuCommand menuCommadn)
        {
            GameObject goRoot = (GameObject)menuCommadn.context;
            if (goRoot == null)
                return;
                
            if (goRoot.GetComponent<Text>() == null)
            {
                Debug.Log("Selecting Object is not a text!");
                return;
            }

            Shadow shadow = goRoot.GetComponent<Shadow>();
            if (shadow==null)
            {
                shadow=goRoot.AddComponent<Shadow>();
            }
            shadow.effectDistance = new Vector2(0, -2);

            Outline outline = goRoot.GetComponent<Outline>();
            if (outline == null)
            {
                outline = goRoot.AddComponent<Outline>();
            }
            outline.effectDistance = new Vector2(1, -1);
            
        }

        #region 工具
        [MenuItem("GameObject/★UI扩展★/★工具★/打印Image引用对象", false, 21)]
        static void PrintImagUse(MenuCommand menuCommadn)
        {
            GameObject target = menuCommadn.context as GameObject;
            Image[] imgs = target.GetComponentsInChildren<Image>(true);
            Dictionary<SpriteAtlas, List<string>> saDict = new Dictionary<SpriteAtlas, List<string>>();
           
            string imgPath;
            string spriteAtlasPath;
            SpriteAtlas sa;
            List<string> infos;
            foreach (Image img in imgs)
            {
                imgPath = AssetDatabase.GetAssetPath(img.sprite);
                if (imgPath.IndexOf("/UIAtlas/") == -1) continue;
                imgPath = imgPath.Substring(0, imgPath.LastIndexOf("/"));
                spriteAtlasPath = imgPath.Replace("/ArtRes/UIAtlas/", "/BundleRes/UIAtlas/") + ".spriteatlas";
                sa = AssetDatabase.LoadAssetAtPath<SpriteAtlas>(spriteAtlasPath);
                if (!saDict.TryGetValue(sa, out infos))
                {
                    infos = new List<string>();
                    saDict.Add(sa, infos);
                }
                infos.Add(img.name + "######" + imgPath+"/"+ img.sprite.name);
            }
            foreach (var kv in saDict)
            {
                CLog.Error(kv.Key.name+"=============");
                foreach (var sp in kv.Value)
                    CLog.Log(sp);
            }
        }
        #endregion
    }
}