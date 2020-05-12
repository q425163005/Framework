using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CSF;
using HeroModelEditor;
using LitJson;
using UnityEngine;

public class FBMaplEditorScript : MonoBehaviour
{
    public Transform MapContent;

    public List<FBItemPosData> datas;

    [ContextMenu("Collect")]
    void Collect()
    {
        datas =new List<FBItemPosData>();

        foreach (RectTransform child in MapContent)
        {
            FBItemPosData fbdata=new FBItemPosData();
            fbdata.item_Pos = new[] {child.anchoredPosition.x, child.anchoredPosition.y};

            RectTransform imglock = child.Find("imgLock").GetComponent<RectTransform>();
            fbdata.lock_Pos = new[] { imglock.anchoredPosition.x, imglock.anchoredPosition.y};

            RectTransform imgFB = child.Find("imgFB").GetComponent<RectTransform>();
            fbdata.title_Pos = new[] { imgFB.anchoredPosition.x, imgFB.anchoredPosition.y };

            Transform LevelContent = child.Find("LevelContent");

            fbdata.levelItem_data=new List<LevelItemPosData>();
            foreach (RectTransform variable in LevelContent)
            {
                LevelItemPosData leveldata = new LevelItemPosData();
                leveldata.item_Pos = new[] {variable.anchoredPosition.x, variable.anchoredPosition.y};

                RectTransform imgPoint1 = variable.Find("imgPoint1").GetComponent<RectTransform>();
                leveldata.point1_Pos = new[] { imgPoint1.anchoredPosition.x, imgPoint1.anchoredPosition.y };

                RectTransform imgPoint2 = variable.Find("imgPoint2").GetComponent<RectTransform>();
                leveldata.point2_Pos = new[] { imgPoint2.anchoredPosition.x, imgPoint2.anchoredPosition.y };

                fbdata.levelItem_data.Add(leveldata);
            }
            datas.Add(fbdata);
        }
    }

    [ContextMenu("Save")]
    void Save()
    {
        string                 path = Path.GetFullPath(AppSetting.BundleResDir + "Data/FBMaplPosSetting.txt");
        FileInfo               info = new FileInfo(path);
        
        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            using (StreamWriter sWriter = new StreamWriter(fs, Encoding.GetEncoding("UTF-8")))
            {
                sWriter.WriteLine(JsonMapper.ToJson(datas));
            }
        }
#if UNITY_EDITOR
        UnityEditor.EditorUtility.DisplayDialog("提示信息", "全部保存成功", "确定");
#endif
    }


    [Serializable]
    public class FBItemPosData
    {
        public float[] item_Pos;
        public float[] lock_Pos;
        public float[] title_Pos;

        public List<LevelItemPosData> levelItem_data;
    }

    [Serializable]
    public class LevelItemPosData
    {
        public float[] item_Pos;
        public float[] point1_Pos;
        public float[] point2_Pos;
    }
    
}