using CSF;
using CSF.Tasks;
using System.Collections;
using UnityEngine;
using UnityEngine.U2D;

public class AtlasLoader : MonoBehaviour
{
    void OnEnable()
    {
        SpriteAtlasManager.atlasRequested += RequestAtlas;
    }

    void OnDisable()
    {
        SpriteAtlasManager.atlasRequested -= RequestAtlas;
    }
    void RequestAtlas(string tag, System.Action<SpriteAtlas> callback)
    {
        if (Mgr.Assetbundle != null)
            loadSpriteAtlas(tag, callback).Run();
    }

    async CTask loadSpriteAtlas(string tag, System.Action<SpriteAtlas> callback)
    {
        SpriteAtlas objs = await Mgr.Assetbundle.LoadSpriteAtlas(tag);
        callback(objs);
    }
}