using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class LayerManager : MonoBehaviour
{
    private Canvas canvas;

    [SerializeField] public int    layer;
    [SerializeField] public bool   rayCaster    = true;
    [SerializeField] public bool   customLayer    = false; //自定义层级(默认跟随界面层级)
    [SerializeField] public string sortingLayer = string.Empty;

    private void OnEnable()
    {
        canvas = this.GetComponent<Canvas>();
    }

    public void SetLayer(string UINode, int baselayer)
    {
        canvas.overrideSorting = true;
        if (!customLayer)
        {
            canvas.sortingLayerID = SortingLayer.NameToID(UINode);
            canvas.sortingOrder   = baselayer + layer;
        }
        else
        {
            canvas.sortingLayerID = SortingLayer.NameToID(sortingLayer);
            canvas.sortingOrder   = layer;
        }
    }
}