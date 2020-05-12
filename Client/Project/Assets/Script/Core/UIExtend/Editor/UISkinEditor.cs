using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
[CustomEditor(typeof(UISkin))]
public class UISkinEditor : Editor
{
    UIOutlet uIOutlet;
    UISkin uiSkin;
    /// <summary>
    /// 能够添加的UI对象名称
    /// </summary>
    List<string> lastObj=new List<string>();
    int currAddRectName;



    void GetLastObj(SkinInfo skinInfo)
    {
        lastObj.Clear();
        
        //比较皮肤里面的对象和UIOutlet里面的对象 得出目前还能够控制得对象
        foreach (var item in uIOutlet.OutletInfos)
        {
            if (item.Object == null)
                continue;
            //Debug.Log("item" + item.Name);
            ((GameObject)item.Object).SetVisible(true);
            bool IsCanAdd = true;
            foreach (var RectInfo in skinInfo.objRectInfo)
            {
                if (RectInfo.gameObject == null || item.Name == RectInfo.gameObject.name)
                {
                    IsCanAdd = false;
                    break;
                }

            }
            //Debug.Log("item"+ item.Name + IsCanAdd);
            if (IsCanAdd)
            {
                lastObj.Add(item.Name);
                ((GameObject)item.Object).SetVisible(false);
            }
        }
    }
    public override void OnInspectorGUI()
    {
        if (Application.isPlaying)
        {
            base.OnInspectorGUI();
            return;
        }
        EditorGUI.BeginChangeCheck();
        uiSkin = target as UISkin;
        SkinInfo CurrSkinInfo = uiSkin.CurrSkin;
        uIOutlet = uiSkin.GetComponent<UIOutlet>();

        
        if (uiSkin.dicSkinInfo.Count <= 0)
        {
            if (GUILayout.Button("Add NewSkin"))
            {
                SkinInfo skinInfo = new SkinInfo();
                skinInfo.id = uiSkin.dicSkinInfo.Count;
                uiSkin.dicSkinInfo.Add(skinInfo);
                skinInfo.objRectInfo.Add(new RectInfo(uiSkin.gameObject));
                uiSkin.currSkinId = skinInfo.id;
                CurrSkinInfo = skinInfo;
                //ChangeSkin();
                Debug.Log("添加新皮肤");
            }
            return;
        }
        GUILayout.BeginHorizontal("Box");
        GUILayout.Label("皮肤选择");
        List<string> NameStr = new List<string>();
        foreach (var item in uiSkin.dicSkinInfo)
        {
            NameStr.Add($"Skein[{item.id}]");
        }
        int currSkinID = CurrSkinInfo.id;
        uiSkin.currSkinId = EditorGUILayout.Popup("", uiSkin.currSkinId, NameStr.ToArray());
       if(currSkinID!= uiSkin.currSkinId)
        {
            CurrSkinInfo = uiSkin.CurrSkin;
            //读取皮肤记录的属性 并赋值
            foreach (var item in CurrSkinInfo.objRectInfo)
            {
                item.ApplyRectTramsform();
            }
        }

        EditorUtility.SetDirty(target);
        GetLastObj(CurrSkinInfo);


        if (GUILayout.Button("+"))
        {
            Undo.RecordObject(target, "Insert OutletInfo");
            SkinInfo skinInfo = new SkinInfo();
            skinInfo.id = uiSkin.dicSkinInfo.Count;
            uiSkin.dicSkinInfo.Add(skinInfo);
            uiSkin.currSkinId = skinInfo.id;
            skinInfo.objRectInfo.Add(new RectInfo(uiSkin.gameObject));
            
        }
        if (GUILayout.Button("-"))
        {
            Undo.RecordObject(target, "Insert OutletInfo");
            uiSkin.dicSkinInfo.Remove(CurrSkinInfo);
            if (uiSkin.currSkinId >= uiSkin.dicSkinInfo.Count)
                uiSkin.currSkinId = uiSkin.dicSkinInfo.Count - 1;
        }

        GUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        //显示皮肤控制的物体
        if (CurrSkinInfo == null)
            return;
        //预防物体被删除
        int count = CurrSkinInfo.objRectInfo.Count;
        for (int i = count-1; i >=0; i--)
        {
            if (CurrSkinInfo.objRectInfo[i].gameObject == null)
                CurrSkinInfo.objRectInfo.Remove(CurrSkinInfo.objRectInfo[i]);
        }
        for (int i = 0; i < CurrSkinInfo.objRectInfo.Count; i++)
        {
            RectInfo info = CurrSkinInfo.objRectInfo[i];
            info.IsShow = EditorGUILayout.Foldout(info.IsShow, info.gameObject.name);

            if (info.IsShow)
            {
                EditorGUILayout.BeginVertical("BOX");
                EditorGUILayout.ObjectField("target", info.gameObject, typeof(Object), true);
                EditorGUILayout.Vector3Field("anchoredPosition", info.anchoredPosition);
                EditorGUILayout.Vector2Field("anchorMax", info.anchorMax);
                EditorGUILayout.Vector2Field("anchorMin", info.anchorMin);
                EditorGUILayout.Vector2Field("sizeDelta", info.sizeDelta);
                EditorGUILayout.Vector2Field("pivot", info.pivot);
                if(info.gameObject!= uiSkin.gameObject)
                {
                    if (GUILayout.Button("Delete"))
                    {
                        Undo.RecordObject(target, "Delete RectInfo");
                        CurrSkinInfo.objRectInfo.Remove(info);
                    }
                }
                EditorGUILayout.EndVertical();
            }
        }
       
        if (lastObj.Count > 0)
        {
            EditorGUILayout.BeginVertical("BOX");
            currAddRectName = EditorGUILayout.Popup("可选对象", currAddRectName, lastObj.ToArray());
            if (GUILayout.Button("Add RectTransform"))
            {
                foreach (var item in uIOutlet.OutletInfos)
                {
                    if (item.Name == lastObj[currAddRectName])
                    {
                        RectInfo rectInt = new RectInfo((GameObject)item.Object);
                        CurrSkinInfo.objRectInfo.Add(rectInt);
                    }
                }
                currAddRectName = 0;
                //GetLastObj();
            }
            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        if (GUILayout.Button("Save Skin", GUI.skin.button,GUILayout.Width(200),GUILayout.Height(50)))
        {
            Undo.RecordObject(target, "Save Skin");
            foreach (var item in CurrSkinInfo.objRectInfo)
            {
                item.RefreshTramsform();
            }
        }
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "GUI Change Check");
        }
    }


}
