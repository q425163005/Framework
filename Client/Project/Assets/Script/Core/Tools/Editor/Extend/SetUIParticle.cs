
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

public class SetUIParticle
{

    [MenuItem("Assets/★工具★/SetUIParticle", false, 10)]
    static private void Set()
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
        int editNum = 0;
        if (editList.Count > 0)
        {
            for (int i = 0; i < editList.Count; i++)
            {               
                GameObject obj = editList[i];
                string path = AssetDatabase.GetAssetPath(obj);
                ParticleSystem[] partices = obj.GetComponentsInChildren<ParticleSystem>(true);
                if (partices.Length > 0)
                {                   
                    GameObject newObj = GameObject.Instantiate(obj) as GameObject;
                    partices = newObj.GetComponentsInChildren<ParticleSystem>(true);
                    bool isEdit = false;
                    foreach (var par in partices)
                    {
                        UIParticle uipar = par.GetComponent<UIParticle>();
                        if (uipar == null)
                        {
                            isEdit = true;
                            par.gameObject.AddComponent<UIParticle>();                          
                        }
                    }
                    if (isEdit)
                    {                       
                        editNum += 1;
                        PrefabUtility.SaveAsPrefabAsset(newObj, path);
                        GameObject.DestroyImmediate(newObj);
                    }
                }
               
            }
            ToolsHelper.Log($"操作完成!!选中{editList.Count}个, 修改{editNum}个");
        }
        
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    [MenuItem("Assets/★工具★/SetUIParticle", true)]
    static private bool VSet()
    {
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        return (!string.IsNullOrEmpty(path));
    }

    static private string GetRelativeAssetsPath(string path)
    {
        return "Assets" + Path.GetFullPath(path).Replace(Path.GetFullPath(Application.dataPath), "").Replace('\\', '/');
    }
}
