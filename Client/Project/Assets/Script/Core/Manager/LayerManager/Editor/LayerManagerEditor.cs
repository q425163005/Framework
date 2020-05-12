using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CSF;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

[InitializeOnLoad]
[CustomEditor(typeof(LayerManager))]
public class LayerManagerEditor : Editor
{
    private int              layerVal;
    private bool             rayCaster;
    private GraphicRaycaster graphicRaycaster;

    private bool customLayer; //自定义层级(默认跟随界面层级)

    public override void OnInspectorGUI()
    {
        LayerManager layerManager = target as LayerManager;

        //射线
        rayCaster = EditorGUILayout.Toggle("射线", layerManager.rayCaster);
        //跟随界面层级/自定义层级
        customLayer = EditorGUILayout.BeginToggleGroup("自定义层级(默认跟随界面层级)", layerManager.customLayer);
        if (customLayer)
        {
            string[] layerArry        = GetSortingLayerNames();
            string   selLayerstr      = layerManager.sortingLayer;
            var      currentTypeIndex = 0;
            if (selLayerstr != string.Empty)
            {
                currentTypeIndex = layerArry.ToList().IndexOf(selLayerstr);
            }

            var typeIndex = EditorGUILayout.Popup("", currentTypeIndex, layerArry);
            layerManager.sortingLayer = layerArry[typeIndex];
        }

        layerManager.customLayer = customLayer;
        EditorGUILayout.EndToggleGroup();

        graphicRaycaster = layerManager.GetComponent<GraphicRaycaster>();
        if (rayCaster)
        {
            if (graphicRaycaster == null)
            {
                graphicRaycaster = layerManager.gameObject.AddComponent<GraphicRaycaster>();
            }
            else
            {
                graphicRaycaster.enabled = true;
            }
        }
        else
        {
            if (graphicRaycaster != null)
            {
                graphicRaycaster.enabled = false;
            }
        }

        layerVal               = EditorGUILayout.IntField("层级", layerManager.layer);
        layerManager.rayCaster = rayCaster;
        layerManager.layer     = layerVal;
    }

    public string[] GetSortingLayerNames()
    {
        Type internalEditorUtilityType = typeof(InternalEditorUtility);
        PropertyInfo sortingLayersProperty =
            internalEditorUtilityType.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
        return (string[]) sortingLayersProperty.GetValue(null, new object[0]);
    }
}