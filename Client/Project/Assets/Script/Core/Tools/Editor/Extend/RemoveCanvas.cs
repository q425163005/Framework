
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using CSF;
using UnityEngine.UI;

public class RemoveCanvas
{
    //static Dictionary<string, int> dicLayer = new Dictionary<string, int>()
    //{
    //    { "UIMain_Hill1",0},
    //    { "UIMain_Hill2",10},
    //    { "UIMain_Map",20},
    //    { "UIMain_Build",30},
    //    { "UIMain",100},
    //    { "UIWindow",200},
    //    { "UIPopup",300},
    //    { "UIWarBkg",400},
    //    { "WarMonster",400},
    //    { "UIWar",400},
    //    { "UITip",500},
    //    { "UIAlert",600},
    //    { "UIStory",700},
    //    { "Loading",800},
    //    { "UIMessage",900},
    //    { "LongScreenMask",1000},
    //    { "Top",1000},
    //};

    [MenuItem("Assets/★工具★/Remove Canvas", false, 10)]
    static private void Remove()
    {
        //CLog.Error(Selection.activeObject);

        // CLog.Error(AssetDatabase.GetAssetPath(Selection.activeObject));
        //AssetDatabase.LoadAllAssetsAtPath(Selection.activeObject.)

        List<GameObject> editList = new List<GameObject>();        
        if (Selection.activeObject is GameObject)
            editList.Add(Selection.activeObject as GameObject);
        else
        {
            string[] assets = AssetDatabase.FindAssets("t:Prefab", new string[] { AssetDatabase.GetAssetPath(Selection.activeObject) });
            foreach (string path in assets)
            {
                GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(path));
                if (obj != null)
                    editList.Add(obj);
            }
        }       

        int editNum = 0;
        if (editList.Count > 0)
        {
            for (int i = 0; i < editList.Count; i++)
            {                
                GameObject obj = editList[i];
                string path = AssetDatabase.GetAssetPath(obj);

                if (obj.name == "GuideUI" || obj.name == "UICanvas")
                {
                    CLog.Error("跳过特殊预制:" + path);
                    continue;
                }

                Canvas[] canvas = obj.GetComponentsInChildren<Canvas>(true);
                bool isEdit = false;
                foreach (var can in canvas)
                {
                    editNum++;
                    GraphicRaycaster grap = can.GetComponent<GraphicRaycaster>();
                    if (grap != null)
                        GameObject.DestroyImmediate(grap, true);

                    LayerManager manges = can.GetComponent<LayerManager>();
                    if (manges != null)
                        GameObject.DestroyImmediate(manges, true);


                    CanvasScaler sca = can.GetComponent<CanvasScaler>();
                    if (sca != null)
                        GameObject.DestroyImmediate(sca, true);  
                    GameObject.DestroyImmediate(can, true);
                }

                
                if (isEdit)
                    ToolsHelper.Log(path, false);                
            }
            ToolsHelper.Log($"操作完成!!选中{editList.Count}个, 修改{editNum}个");
        }
        
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    [MenuItem("Assets/★工具★/RemoveCanvas", true)]
    static private bool VRemove()
    {
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        return (!string.IsNullOrEmpty(path));
    }

    static private string GetRelativeAssetsPath(string path)
    {
        return "Assets" + Path.GetFullPath(path).Replace(Path.GetFullPath(Application.dataPath), "").Replace('\\', '/');
    }
}
