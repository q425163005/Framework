using System.Collections;
using System.Collections.Generic;
using CSF;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class ImageSpriteIndexData
{
    public Image Image;
    public Sprite[] Sprites;
}

[AddComponentMenu("自定义/ImageSpriteIndex")]
[ExecuteInEditMode]
public class ImageSpriteIndex : MonoBehaviour
{
    public List<ImageSpriteIndexData> ImageList=new List<ImageSpriteIndexData>();

    public int Index;

    public void SetIndex(int index)
    {
        Index = index;
        for (int i = 0; i < ImageList.Count; i++)
        {
            if (ImageList[i].Image!=null && ImageList[i].Sprites[Index]!=null)
            {
                ImageList[i].Image.sprite = ImageList[i].Sprites[Index];
            }
        }
           
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        SetIndex(Index);
    }
#endif
}
