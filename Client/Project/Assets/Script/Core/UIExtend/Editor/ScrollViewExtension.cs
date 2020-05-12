using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[CustomEditor(typeof(MapScrollRect), true)]
public class ScrollViewExtension : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}

