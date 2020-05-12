
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
using Coffee.UIExtensions;

public class FindMissing
{
    [MenuItem("Assets/★工具★/FindMissing", false, 10)]
    static private void Find()
    {
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

        int num = 0;
        if (editList.Count > 0)
        {
            for (int i = 0; i < editList.Count; i++)
            {
                string path = AssetDatabase.GetAssetPath(Selection.activeObject);
                GameObject obj = editList[i];
                Component[] comps = obj.GetComponentsInChildren<Component>(true);
                foreach (var com in comps)
                {
                    if (com == null)
                    {
                        num++;
                        string s = obj.name;
                        Transform t = obj.transform;
                        while (t.parent != null)
                        {
                            s = t.parent.name + "/" + s;
                            t = t.parent;
                        }
                        Debug.Log("Missing: " +s);
                    }
                }
            }
            ToolsHelper.Log($"查找到{num}个");
        }
        
        //AssetDatabase.SaveAssets();
       //AssetDatabase.Refresh();
    }

    [MenuItem("Assets/★工具★/FindMissing", true)]
    static private bool VFind()
    {
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        return (!string.IsNullOrEmpty(path));
    }

    static private string GetRelativeAssetsPath(string path)
    {
        return "Assets" + Path.GetFullPath(path).Replace(Path.GetFullPath(Application.dataPath), "").Replace('\\', '/');
    }
}
