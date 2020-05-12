using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CSF;
using HeroModelEditor;
using LitJson;
using UnityEngine;

public class PolygonEditorScript : MonoBehaviour
{
    public List<BuildPolygonSetting> datas;

    [ContextMenu("Collect")]
    void Collect()
    {
        datas =new List<BuildPolygonSetting>();
        PolygonCollider2D[] polygons = transform.GetComponentsInChildren<PolygonCollider2D>();
        foreach (var variable in polygons)
        {
            BuildPolygonSetting info =new BuildPolygonSetting();
            info.name = variable.transform.parent.name;
            info.pos=new List<float[]>();
            foreach (var variable2 in variable.points)
            {
                info.pos.Add(new float[]{variable2.x,variable2.y});
            }
            datas.Add(info);
        }
    }

    [ContextMenu("Save")]
    void Save()
    {
        string                 path = Path.GetFullPath(AppSetting.BundleResDir + "Data/BuildPolygonSetting.txt");
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

    
}

[Serializable]
public class BuildPolygonSetting
{
    public string    name { get; set; }

    public List<float[]> pos { get; set; }
    
}