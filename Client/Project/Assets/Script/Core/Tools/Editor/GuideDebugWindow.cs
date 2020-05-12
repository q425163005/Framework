using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CSF
{
    /// <summary>
    /// 打开指引调试
    /// </summary>
    public class GuideDebugWindow : EditorWindow
    {
        private Canvas canvas;


        private GUIStyle buttonStyle;
        private GUIStyle labelStyle;
        private GUIStyle textStyle;

        private string[] lastUIInfo;
        private GameObject lastClickObject;
        private Vector2 shotMoustPost;
        private Vector2 shotOffsetPost;
        private bool isDebugGuide = false;
        public GuideDebugWindow()
        {
        }
        private void reset()
        {
            lastUIInfo = new string[] { string.Empty, string.Empty };
            lastClickObject = null;
            shotMoustPost = Vector2.zero;
            shotOffsetPost = Vector2.zero;
            isDebugGuide = false;
        }

        private void OnGUI()
        {
            buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.fontSize = 14;

            labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.fontSize = 14;

            textStyle = new GUIStyle(GUI.skin.textField);
            textStyle.fontSize = 14;

            if (!Application.isPlaying)
            {
                GUILayout.Label("运行模式下才能使用!!!", labelStyle);
                reset();
            }
            else
            {
                if (canvas == null)
                {
                    GameObject obj = GameObject.Find("UICanvas");
                    if (obj != null)
                        canvas = obj.GetComponent<Canvas>();
                    reset();
                }
                if (canvas == null)
                {
                    GUILayout.Label("UICanvas下找到Canvas组件!!! ", labelStyle);
                    return;
                }

                GUILayout.Space(20);
                showLastUIObject();
                GUILayout.Space(30);
                showMouseOffset();
                GUILayout.Space(30);
                showGuideDebug();
                GUILayout.Space(30);
                showFunOpenDebug();
                GUILayout.Space(30);
                showGuideGoDebug();
                GUILayout.Space(30);
                showMultAttackDebug();
            }
        }

        #region UI点击对象
        /// <summary>
        /// 显示最后点击的UI对象
        /// </summary>
        private void showLastUIObject()
        {
            setLastUIObjInfo();
            GUILayout.BeginHorizontal();
            GUILayout.Label("最后点击的UI对象信息:", labelStyle);
            if (lastUIInfo[1] != string.Empty)
            {
                if (GUILayout.Button("定位到点击对象", buttonStyle, GUILayout.Width(120)))
                {
                    if (lastClickObject != null)
                        Selection.activeObject = lastClickObject;
                }
            }
            GUILayout.EndHorizontal();

            //UI名称
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("复制", buttonStyle, GUILayout.Width(80)))
            {
                GUIUtility.systemCopyBuffer = lastUIInfo[0];
            }
            GUILayout.Label("UI名:" + lastUIInfo[0], labelStyle);
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
            //触发对象
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("复制", buttonStyle, GUILayout.Width(80)))
            {
                GUIUtility.systemCopyBuffer = lastUIInfo[1];
            }           
            GUILayout.Label("点击对象:" + lastUIInfo[1], labelStyle, GUILayout.MaxWidth(300));           
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
            if (GUILayout.Button("复制 UI名称和点击对象", buttonStyle))
            {
                GUIUtility.systemCopyBuffer = lastUIInfo[0] + "\t" + lastUIInfo[1];
            }
        }
      
        /// <summary>
        /// 最后点击的UI对象
        /// </summary>
        private void setLastUIObjInfo()
        {
            string uiName = string.Empty;
            string path = string.Empty;
            GameObject go = EventSystem.current.currentSelectedGameObject;
            if (go == null && EventSystem.current.currentInputModule != null)
            {
                Type type = EventSystem.current.currentInputModule.GetType();
                FieldInfo field = type.GetField("m_InputPointerEvent", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (field != null)
                {
                    PointerEventData data = (PointerEventData)field.GetValue(EventSystem.current.currentInputModule);
                    if (data != null)
                    {
                        if(data.pointerPress!=null)
                            go = data.pointerPress;
                        else if(data.lastPress!=null)
                            go = data.lastPress;
                    }
                }
            }
            if (go != null)
            {
                Transform transform = go.transform;
                path = transform.name;
                while (transform.parent != null)
                {
                    transform = transform.parent;

                    uiName = GetUIName(transform);
                    if (uiName != string.Empty) break;
                    path = transform.name + "/" + path;
                }
                lastUIInfo[0] = uiName;
                lastUIInfo[1] = path;
                lastClickObject = go;
            }
        }
        private string GetUIName(Transform transform)
        {
            if (transform.parent != null && transform.parent.parent != null && transform.parent.parent.name == "UIRoot(Clone)")
                return transform.name.Replace("(Clone)","");
            return string.Empty;
        }
        #endregion

        #region 当前鼠标位置相对最后点击的UI对象偏移量
        private void showMouseOffset()
        {
            Vector2 mousePos = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out mousePos);

            Vector2 offsetPos = Vector2.zero;
            if (lastClickObject != null)
            {
                Vector2 objScenePos = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, lastClickObject.transform.position);
                Vector2 objPos = Vector2.zero;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, objScenePos, canvas.worldCamera, out objPos);
                RectTransform clickRect = lastClickObject.transform as RectTransform;
                Vector2 offset = Vector2.zero;
                if (clickRect!=null)
                    offset = new Vector2(clickRect.rect.x + clickRect.rect.width / 2, clickRect.rect.y + clickRect.rect.height / 2);
                offsetPos = mousePos - objPos-offset;
            }

            string isKeyDown = Main.Editor_IsKeyDownSpace ? "已按下" : "未按";
            GUILayout.Label($"长按[空格键]可以截取坐标信息  {isKeyDown}", labelStyle);
            if (Main.Editor_IsKeyDownSpace)
            {
                shotMoustPost = new Vector2(mousePos.x, mousePos.y);
                if(lastClickObject!=null)
                    shotOffsetPost = new Vector2(offsetPos.x, offsetPos.y);
            }
            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("复制(截取)", buttonStyle, GUILayout.Width(80)))
            {
                GUIUtility.systemCopyBuffer = Vector2VoStr(shotMoustPost);
            }
            GUILayout.Label($"鼠标坐标全局:      截取:{Vector2VoStr(shotMoustPost)}   时实:{Vector2VoStr(mousePos)}", labelStyle);
            GUILayout.EndHorizontal();

            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("复制(截取)", buttonStyle, GUILayout.Width(80)))
            {
                GUIUtility.systemCopyBuffer = Vector2VoStr(shotOffsetPost);
            }
            GUILayout.Label($"相对点击对象坐标:截取:{Vector2VoStr(shotOffsetPost)}   时实:{Vector2VoStr(offsetPos)}", labelStyle);
            GUILayout.EndHorizontal();
        }
        private string Vector2VoStr(Vector2 vec)
        {
            return $"{(int)vec.x};{(int)vec.y}";
        }
        #endregion


        #region 指引调试相关
        string debugGuideId = "1";
        private void showGuideDebug()
        {
            GUILayout.Label("---------------------------指引调试---------------------------", labelStyle);
            string isDebug = isDebugGuide ? "调试状态，不会保存指引数据" : "非调试状态";
            GUILayout.Label($"当前指引为  {isDebug}", labelStyle);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("重新加载指引配置", buttonStyle, GUILayout.Width(200)))
            {
                Mgr.ILR.CallHotFix("ReloadGuideConfig");
            }
            GUILayout.Label($"需要重新调试指引生效果", labelStyle);
            GUILayout.EndHorizontal();

            GUILayout.Space(5);
            GUILayout.BeginHorizontal();            
            debugGuideId = GUILayout.TextField(debugGuideId, textStyle, GUILayout.Width(80));
            if (GUILayout.Button("调试指定指引", buttonStyle, GUILayout.Width(100)))
            {
                isDebugGuide = true;
                int guideId = Convert.ToInt32(debugGuideId);
                Mgr.ILR.CallHotFix("GuideDebug", guideId);
            }
            GUILayout.Label($"执行后变成调试状态", labelStyle);
            GUILayout.EndHorizontal();

            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("完成当前执行的指引", buttonStyle, GUILayout.Width(200)))
            {
                Mgr.ILR.CallHotFix("GuideComplete");
            }
            if (GUILayout.Button("关闭所有指引", buttonStyle, GUILayout.Width(200)))
            {
                Mgr.ILR.CallHotFix("GuideCloseAll");
            }
            GUILayout.EndHorizontal();
        }
        #endregion

        #region 功能开放
        string debugFunOpenId = "HorseTrain";
        private void showFunOpenDebug()
        {
            GUILayout.Label("-------------------------功能开放调试---------------------------", labelStyle);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("重新加载功能开放配置", buttonStyle, GUILayout.Width(200)))
            {
                Mgr.ILR.CallHotFix("ReloadFunOpenConfig");
            }
            GUILayout.Label($"需要重新调试指引生效果", labelStyle);
            GUILayout.EndHorizontal();

            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.Label($"功能名称", labelStyle, GUILayout.Width(60));
            debugFunOpenId = GUILayout.TextField(debugFunOpenId, textStyle, GUILayout.Width(250));
            if (GUILayout.Button("粘贴", buttonStyle, GUILayout.Width(50)))
            {
                debugFunOpenId = GUIUtility.systemCopyBuffer;
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(5);
            GUILayout.BeginHorizontal();           
            if (GUILayout.Button("开启功能", buttonStyle, GUILayout.Width(200)))
            {
                Mgr.ILR.CallHotFix("FunOpenDebug", debugFunOpenId,true);
            }
            if (GUILayout.Button("关闭功能", buttonStyle, GUILayout.Width(200)))
            {
                Mgr.ILR.CallHotFix("FunOpenDebug", debugFunOpenId, false);
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("开启所有功能", buttonStyle, GUILayout.Width(200)))
            {
                Mgr.ILR.CallHotFix("FunOpenAllDebug");
            }
            if (GUILayout.Button("开启所有副本关卡", buttonStyle, GUILayout.Width(200)))
            {
                Mgr.ILR.CallHotFix("FunOpenAllFBLevel");
            }
            GUILayout.EndHorizontal();
           
        }
        #endregion

        #region 前往参数调试
        string debugGuideGoArgString= "Bag.BagUI";
        string debugGuideGoArgInt1 = "0";
        string debugGuideGoArgInt2 = "0";
        private void showGuideGoDebug()
        {
            GUILayout.Label("-------------------------前往调试---------------------------", labelStyle);           
            GUILayout.BeginHorizontal();
            GUILayout.Label($"打开UI参数", labelStyle, GUILayout.Width(60));
            debugGuideGoArgString = GUILayout.TextField(debugGuideGoArgString, textStyle, GUILayout.Width(250));
            if (GUILayout.Button("粘贴", buttonStyle, GUILayout.Width(50)))
            {
                debugGuideGoArgString = GUIUtility.systemCopyBuffer;
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label($"Int参数1", labelStyle, GUILayout.Width(60));
            debugGuideGoArgInt1 = GUILayout.TextField(debugGuideGoArgInt1, textStyle, GUILayout.Width(250));            
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label($"Int参数2", labelStyle, GUILayout.Width(60));
            debugGuideGoArgInt2 = GUILayout.TextField(debugGuideGoArgInt2, textStyle, GUILayout.Width(250));
            GUILayout.EndHorizontal();

            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("调试前往", buttonStyle, GUILayout.Width(200)))
            {
                Mgr.ILR.CallHotFix("GuideGoDebug", debugGuideGoArgString, Convert.ToInt32(debugGuideGoArgInt1), Convert.ToInt32(debugGuideGoArgInt2));
            }
            GUILayout.EndHorizontal();
        }
        #endregion

        string debugMultValue= "10";
        private void showMultAttackDebug()
        {
            GUILayout.Label("-------------------------战斗多倍攻击特效调试---------------------------", labelStyle);
     
            GUILayout.BeginHorizontal();
            GUILayout.Label($"设置倍数", labelStyle, GUILayout.Width(60));
            debugMultValue = GUILayout.TextField(debugMultValue, textStyle, GUILayout.Width(250));
            GUILayout.EndHorizontal();

            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("设置调试", buttonStyle, GUILayout.Width(200)))
            {
                Mgr.ILR.CallHotFix("WarMultAttackDebug", Convert.ToInt32(debugMultValue));
            }
            if (GUILayout.Button("关闭调试", buttonStyle, GUILayout.Width(200)))
            {
                Mgr.ILR.CallHotFix("WarMultAttackDebug", 0);
            }
            GUILayout.EndHorizontal();
        }

    }
}
