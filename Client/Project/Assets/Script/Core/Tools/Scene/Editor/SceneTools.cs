using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using CSF;
using UnityEngine.SceneManagement;

public class SceneTools
{
    //[MenuItem("GameObject/★场景扩展★/1.创建场景预制根节点", false, 21)]
    static void CreateSceneRootScript(MenuCommand menuCommadn)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        GameObject go = new GameObject(sceneName);        
        SceneLightMapSetting setting = go.AddComponent<SceneLightMapSetting>();
        Selection.activeObject = go;
        ToolsHelper.Log("场景预制根节点创建成功，请将场景下的模型移到"+ sceneName + "节点下");
    }

    //[MenuItem("GameObject/★场景扩展★/2.保存场景预制", false, 21)]
    static void SaveScenePrefab(MenuCommand menuCommadn)
    {
        GameObject target = menuCommadn.context as GameObject;
        if (target != null && target.GetComponent<SceneLightMapSetting>()==null)
        {
            ToolsHelper.Log("请先创建场景预制根节点!!!");
            return;
        }        
        SceneLightMapSetting slms = target.GetComponent<SceneLightMapSetting>();
        Renderer[] savers = Transform.FindObjectsOfType<Renderer>();
        RendererLightMapSetting rlms = null;
        foreach (Renderer s in savers)
        {
            if (s.lightmapIndex != -1)
            {
                rlms = s.gameObject.GetComponent<RendererLightMapSetting>();
                if (rlms == null)
                {
                    rlms = s.gameObject.AddComponent<RendererLightMapSetting>();
                }
            }
        }
        slms.SaveSettings();
        EditorSceneManager.SaveOpenScenes();

        string path = "Assets/GameRes/BundleRes/Scene/" + target.name + ".prefab";
        GameObject newPrefab = PrefabUtility.CreatePrefab(path, target);
        Selection.activeObject = newPrefab;
        ToolsHelper.Log("场景预制创建成功！！" + path);
    }
}