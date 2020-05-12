using CSF.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MapEditor
{
    public class MapUI : MonoBehaviour
    {
        // Start is called before the first frame update

        private GameObject sceneObject;
        public async CTask Refresh()
        {
            string mapName = MapEditor.I.SceneConfig.mapScene;
            GameObject obj = await CSF.Mgr.Assetbundle.LoadPrefab($"Prefabs/scene/" + mapName);//SceneVolcanos01
            obj.transform.SetParent(transform, false);

            if (sceneObject != null)
                GameObject.Destroy(sceneObject);

            sceneObject = obj;
        }
    }
}
