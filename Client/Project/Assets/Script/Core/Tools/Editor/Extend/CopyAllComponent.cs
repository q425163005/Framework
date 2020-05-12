using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditor.UI;
using UnityEngine.UI;

public class CopyAllComponent : EditorWindow
{
    static Component[] copiedComponents;
    [MenuItem("GameObject/复制组件属性 #&C")]
    static void Copy()
    {
        copiedComponents = Selection.activeGameObject.GetComponents<Component>();
    }

    [MenuItem("GameObject/粘贴组件属性 #&V")]
    static void PasteV()
    {
        Paste(false);
    }
    [MenuItem("GameObject/粘贴组件属性(含RectTransform) #&R")]
    static void PasteR()
    {
        Paste(true);
    }

    static void Paste(bool isPasteRectTransform)
    {
        foreach (var targetGameObject in Selection.gameObjects)
        {
            if (!targetGameObject || copiedComponents == null) continue;

            //删除描边
            var outline = targetGameObject.GetComponent<Outline>();
            if (outline != null)
                GameObject.DestroyImmediate(outline);
            var shadow = targetGameObject.GetComponent<Shadow>();
            if (shadow != null)
                GameObject.DestroyImmediate(shadow);

            foreach (var copiedComponent in copiedComponents)
            {
                if (!copiedComponent) continue;
                var type = copiedComponent.GetType();
                if (type == typeof(UILangText)|| type == typeof(Dropdown) || type == typeof(ScrollRect)) continue;


                if (copiedComponent.GetType() == typeof(RectTransform))
                {
                    if (!isPasteRectTransform)
                        continue;
                }

                UnityEditorInternal.ComponentUtility.CopyComponent(copiedComponent);

                if (targetGameObject.TryGetComponent(copiedComponent.GetType(), out var comp))
                    UnityEditorInternal.ComponentUtility.PasteComponentValues(comp);
                else
                    UnityEditorInternal.ComponentUtility.PasteComponentAsNew(targetGameObject);
            }
        }
    }
}