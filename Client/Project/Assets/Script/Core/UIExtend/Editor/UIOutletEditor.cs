using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;

[InitializeOnLoad]
[CustomEditor(typeof(UIOutlet))]
public class UIOutletEditor : Editor
{

    static Dictionary<GameObject, string[]> _outletObjects = new Dictionary<GameObject, string[]>();

    static UIOutletEditor()
    {      
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyItemCB;
        //UIWindowAssetEditor.CustomInspectorGUIAfter += (UIWindowAsset target) =>
        //{
        //    if (target.gameObject.GetComponent<UILuaOutlet>() == null)
        //    {
        //        if (GUILayout.Button("Add UILuaOutlet"))
        //        {
        //            target.gameObject.AddComponent<UILuaOutlet>();
        //        }
        //    }
        //};
    }

    private static void HierarchyItemCB(int instanceid, Rect selectionrect)
    {
        var obj = EditorUtility.InstanceIDToObject(instanceid) as GameObject;
        if (obj != null)
        {
            if (_outletObjects.ContainsKey(obj))
            {
                Rect r = new Rect(selectionrect);
                r.x = r.x+ GetStringWidth(obj.name)+25;
                r.y += 2;
                GUIStyle style = new GUIStyle();
                style.normal.textColor = Color.green;
                //GUI.Label(r, string.Format("{0} [{1}]", _outletObjects[obj][0], _outletObjects[obj][1]), style);
                GUI.Label(r, string.Format("[{0}]", _outletObjects[obj][1]), style);
            }
        }
    }
    private static float GetStringWidth(string str)
    {
        Font font = GUI.skin.font;        
        font.RequestCharactersInTexture(str, font.fontSize, FontStyle.Normal);
        CharacterInfo characterInfo;
        float width = 0f;
        for (int i = 0; i < str.Length; i++)
        {
            font.GetCharacterInfo(str[i], out characterInfo, font.fontSize);            
            width += characterInfo.advance;
        }
        return width;

    }

    private static string ToVariableName(string str)
    {
        str = str.Replace("(Clone)", "");
        str = str.Replace(" ", "_");
        StringBuilder retStr = new StringBuilder();
        for (int i = 0; i < str.Length; i++)
        {
            char ch = str[i];
            if (ch >= 'a' && ch <= 'z')
            {
                retStr.Append(ch);
            }
            else if (ch >= 'A' && ch <= 'Z')
            {
                retStr.Append(ch);
            }
            else if (ch >= '0' && ch <= '9')
            {
                retStr.Append(ch);
            }
            else if (ch == '_')
            {
                retStr.Append(ch);
            }
        }

        return retStr.ToString();
    }

    GUIStyle GreenFont;
    GUIStyle RedFont;

    private HashSet<string> _cachedPropertyNames = new HashSet<string>();

    void OnEnable()
    {
        GreenFont = new GUIStyle();
        GreenFont.fontStyle = FontStyle.Bold;
        GreenFont.fontSize = 11;
        GreenFont.normal.textColor = Color.green;
        RedFont = new GUIStyle();
        RedFont.fontStyle = FontStyle.Bold;
        RedFont.fontSize = 11;
        RedFont.normal.textColor = Color.red;
    }

    public override void OnInspectorGUI()
    {
        _cachedPropertyNames.Clear();

        EditorGUI.BeginChangeCheck();

        UIOutlet outlet = target as UIOutlet;

        #region 扩展功能

        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        //拖拽添加
        var aEvent = Event.current;
        var dragArea = GUILayoutUtility.GetRect(105, 85);
        //在Inspector 窗口上创建区域，向区域拖拽资源对象，获取到拖拽到区域的对象
        GUI.Box(dragArea, "\n\n拖拽添加");

        switch (aEvent.type)
        {
            case EventType.DragUpdated:
            case EventType.DragPerform:
                if (!dragArea.Contains(aEvent.mousePosition))
                {
                    break;
                }

                UnityEngine.Object temp = null;
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                if (aEvent.type == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();
                    Undo.RecordObject(target, "Drag Insert");
                    for (int i = 0; i < DragAndDrop.objectReferences.Length; ++i)
                    {
                        temp = DragAndDrop.objectReferences[i];
                        if (temp == null)
                        {
                            break;
                        }

                        //改名并添加
                        //temp.name = ToVariableName(temp.name);
                        outlet.OutletInfos.Insert(0,
                            new UIOutlet.OutletInfo { Object = temp });
                    }
                }

                Event.current.Use();
                break;
            default:
                break;
        }

        GUILayout.Space(20);
        GUILayout.BeginVertical();

        EditorGUILayout.HelpBox("命名规则：禁止中文、特殊字符、空格", MessageType.Warning);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("简易命名", new[] { GUILayout.Height(35) }))
        {
            if (outlet.OutletInfos != null || outlet.OutletInfos.Count != 0)
            {
                Undo.RecordObject(target, "Rename");
                for (var j = outlet.OutletInfos.Count - 1; j >= 0; j--)
                {
                    if (outlet.OutletInfos[j].Object != null)
                    {
                        outlet.OutletInfos[j].Object.name = ToVariableName(outlet.OutletInfos[j].Object.name);
                    }
                }
            }
        }

        GUILayout.Space(5);

        if (GUILayout.Button("清理空项", new[] { GUILayout.Height(35) }))
        {
            if (outlet.OutletInfos != null || outlet.OutletInfos.Count != 0)
            {
                Undo.RecordObject(target, "Remove AllNull");
                for (var j = outlet.OutletInfos.Count - 1; j >= 0; j--)
                {
                    if (outlet.OutletInfos[j].Object == null)
                    {
                        outlet.OutletInfos.RemoveAt(j);
                    }
                }
            }
        }

        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

        #endregion


        if (outlet.OutletInfos == null || outlet.OutletInfos.Count == 0)
        {
            if (GUILayout.Button("Add New Outlet"))
            {
                if (outlet.OutletInfos == null)
                    outlet.OutletInfos = new List<UIOutlet.OutletInfo>();
                else
                {
                    outlet.OutletInfos.Clear();
                    _outletObjects.Clear();
                }
                Undo.RecordObject(target, "Add OutletInfo");
                outlet.OutletInfos.Add(new UIOutlet.OutletInfo());
            }

        }
        else
        {

            // outlet ui edit

            for (var j = outlet.OutletInfos.Count - 1; j >= 0; j--)
            {
                var currentTypeIndex = -1;
                UIOutlet.OutletInfo outletInfo = outlet.OutletInfos[j];
                string[] typesOptions = new string[0];

                var isValid = outletInfo.Object != null && !_cachedPropertyNames.Contains(outletInfo.Name);

                _cachedPropertyNames.Add(outletInfo.Name);

                if (outletInfo.Object != null)
                {
                    if (outletInfo.Object is GameObject)
                    {
                       
                        var gameObj = outletInfo.Object as GameObject;
                        var components = gameObj.GetComponents<Component>();

                        if (components.Length == 1)
                            currentTypeIndex = 0;
                        else
                            currentTypeIndex = components.Length;// 设置默认类型,默认选中最后一个
                        string objTypeName = "";

                        typesOptions = new string[components.Length+1];
                        typesOptions[0] = gameObj.GetType().FullName;
                        if (typesOptions[0] == outletInfo.ComponentType)
                        {
                            currentTypeIndex = 0;
                            objTypeName = gameObj.GetType().Name;
                        }

                        for (var i = 1; i <= components.Length; i++)
                        {
                            var com = components[i - 1];
                            var typeName = typesOptions[i] = com.GetType().FullName;
                            if (typeName == outletInfo.ComponentType)
                            {
                                currentTypeIndex = i;
                                objTypeName = com.GetType().Name;
                            }
                        }
                        _outletObjects[gameObj] = new string[] { outletInfo.Name, objTypeName };

                        if(string.IsNullOrEmpty(outletInfo.Name))
                            outletInfo.Name = gameObj.name;
                    }

                }


                EditorGUILayout.Separator();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(string.Format("Property: {0}", outletInfo.Name), isValid ? GreenFont : RedFont);
                EditorGUILayout.Space();
                if (GUILayout.Button("+"))
                {
                    Undo.RecordObject(target, "Insert OutletInfo");
                    outlet.OutletInfos.Insert(j, new UIOutlet.OutletInfo());
                }
                if (GUILayout.Button("-"))
                {
                    Undo.RecordObject(target, "Remove OutletInfo");
                    if (outlet.OutletInfos[j].Object != null)
                    {
                        _outletObjects.Remove(outlet.OutletInfos[j].Object as GameObject);
                    }
                    outlet.OutletInfos.RemoveAt(j);
                }
                if (GUILayout.Button("↑", GUILayout.MaxWidth(20))&&j < outlet.OutletInfos.Count-1)
                {
                    UIOutlet.OutletInfo curr = outlet.OutletInfos[j];
                    outlet.OutletInfos[j] = outlet.OutletInfos[j + 1];
                    outlet.OutletInfos[j + 1] = curr;
                }
                if (GUILayout.Button("↓", GUILayout.MaxWidth(20))&&j>0)
                {
                    UIOutlet.OutletInfo curr = outlet.OutletInfos[j];
                    outlet.OutletInfos[j] = outlet.OutletInfos[j - 1];
                    outlet.OutletInfos[j - 1] = curr;
                }
                GUILayout.Space(20);

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                ///outletInfo.Name = EditorGUILayout.TextField("Name:", outletInfo.Name);
                if (outletInfo.Object != null)
                {
                    outletInfo.Name = outletInfo.Object.name;
                    outletInfo.Object = EditorGUILayout.ObjectField("", outletInfo.Object, typeof(UnityEngine.Object), true);
                }
                else
                {
                    outletInfo.Name = "Select Object";
                    outletInfo.Object = EditorGUILayout.ObjectField("", outletInfo.Object, typeof(UnityEngine.Object), true);
                }

                if (currentTypeIndex >= 0)
                {
                    var typeIndex = EditorGUILayout.Popup("", currentTypeIndex, typesOptions);
                    outletInfo.ComponentType = typesOptions[typeIndex].ToString();

                }
                EditorGUILayout.EndHorizontal();
            }
        }
        //base.OnInspectorGUI ();
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "GUI Change Check");
        }
    }


}
