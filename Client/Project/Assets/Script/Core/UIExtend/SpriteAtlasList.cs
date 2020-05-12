using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.U2D;
/// <summary>
/// UI使用的图集信息
/// </summary>
public class SpriteAtlasList : MonoBehaviour
{
    [ReadOnly] [SerializeField] public SpriteAtlas[] AtlasList;
}
