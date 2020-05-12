using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ChangeFontToolsEditor : EditorWindow
{
    ChangeFontToolsEditor()
    {
        this.titleContent = new GUIContent("修改字体参数工具");
    }


    //[MenuItem("Tools/修改字体参数工具")]
    static void CreatWindow()
    {
        EditorWindow.GetWindow(typeof(ChangeFontToolsEditor));
    }

    private GameObject obj;

    private void OnGUI()
    {
        EditorGUILayout.LabelField("*说明*");
        EditorGUILayout.LabelField("*更换字体文件为：Assets/GameRes/BundleRes/Font/Default.TTF");
        EditorGUILayout.LabelField("*Text加粗");
        EditorGUILayout.LabelField("*Outline.effectDistance = new Vector2(1, -1)");
        EditorGUILayout.LabelField("*Shadow.effectDistance = new Vector2(0, -2)");
        EditorGUILayout.LabelField("*说明*");

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("*模式一");
        EditorGUILayout.LabelField("更换Assets/GameRes/BundleRes/UI/ 下所有UI预制体");

        if (GUILayout.Button("更换1"))
        {
            ChangeFont();
        }
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("*模式二");
        EditorGUILayout.LabelField("更换单个预制体");

        obj = EditorGUILayout.ObjectField(new GUIContent("obj:", ""), obj, typeof(GameObject), false) as GameObject;
        if (GUILayout.Button("更换2"))
        {
            ChangeFont2();
        }
    }


    void ChangeFont()
    {
        Debug.Log($"开始更换");

        //更换逻辑

        //物体存放路径
        string fullpath = "Assets/GameRes/BundleRes/UI/";

        DirectoryInfo dirInfo = new DirectoryInfo(fullpath + "/");
        FileInfo[]    files   = dirInfo.GetFiles("*", SearchOption.AllDirectories); //包括子目录

        Font font = AssetDatabase.LoadAssetAtPath<Font>("Assets/GameRes/BundleRes/Font/Default.TTF");

        for (int i = 0; i < files.Length; i++)
        {
            if (files[i].Name.EndsWith(".prefab"))
            {
                string     path      = files[i].FullName.Remove(0, 30);
                GameObject targetObj = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;
                Text[]     texArr    = targetObj.GetComponentsInChildren<Text>(true);

                foreach (var variable in texArr)
                {
                    variable.fontStyle = FontStyle.Bold;
                    variable.font      = font;
                }

                Shadow[] shadowArr = targetObj.GetComponentsInChildren<Shadow>(true);

                foreach (var variable in shadowArr)
                {
                    variable.effectDistance = new Vector2(0, -2);
                }

                Outline[] oulinrArr = targetObj.GetComponentsInChildren<Outline>(true);

                foreach (var variable in oulinrArr)
                {
                    variable.effectDistance = new Vector2(1, -1);
                }

                EditorUtility.SetDirty(targetObj);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                Debug.Log($" {i + 1} ----{files[i].Name}----");
            }
        }

        Debug.Log($"更换完成");
    }


    void ChangeFont2()
    {
        string     path      = UnityEditor.PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(obj);
        GameObject targetObj = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;
        if (targetObj == null)
        {
            Debug.LogError("targetObj is null");
            return;
        }

        Debug.Log(targetObj.name);

        Text[] texArr = targetObj.GetComponentsInChildren<Text>(true);
        Font font = AssetDatabase.LoadAssetAtPath<Font>("Assets/GameRes/BundleRes/Font/Default.TTF");
        foreach (var variable in texArr)
        {
            variable.fontStyle = FontStyle.Bold;
            variable.font = font;
        }

        Shadow[] shadowArr = targetObj.GetComponentsInChildren<Shadow>(true);

        foreach (var variable in shadowArr)
        {
            variable.effectDistance = new Vector2(0, -2);
        }

        Outline[] oulinrArr = targetObj.GetComponentsInChildren<Outline>(true);

        foreach (var variable in oulinrArr)
        {
            variable.effectDistance = new Vector2(1, -1);
        }

        EditorUtility.SetDirty(targetObj);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"更换完成");
    }




}