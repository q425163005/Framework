using CSF.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;
using static EventListener;

namespace MapEditor
{
    /// <summary>
    /// 地图编辑器用
    /// </summary>
    public static class UIExtension
    {
        /// <summary> GameObject 点击事件</summary>
        public static void AddClick(this GameObject go, Action action)
        {
            EventListener.Get(go).onClick = (data) => { action(); };
        }

        /// <summary> GameObject 点击事件,带1参数</summary>
        public static void AddClick<T1>(this GameObject go, Action<T1> action, T1 arg1)
        {
            EventListener.Get(go).onClick = (data) => { action(arg1); };
        }

        /// <summary> GameObject 点击事件,带2参数</summary>
        public static void AddClick<T1, T2>(this GameObject go, Action<T1, T2> action, T1 arg1, T2 arg2)
        {
            EventListener.Get(go).onClick = (data) => { action(arg1, arg2); };
        }

        //=========================================================

        /// <summary> 控件点击事件</summary>
        public static void AddClick<T>(this T go, Action action) where T : Component
        {
            EventListener.Get(go).onClick = (data) => { action(); };
        }

        /// <summary> 控件点击事件</summary>
        public static void AddClick<T, T1>(this T go, Action<T1> action, T1 arg1) where T : Component
        {
            EventListener.Get(go).onClick = (data) => { action(arg1); };
        }

        /// <summary> 控件点击事件</summary>
        public static void AddClick<T, T1, T2>(this T go, Action<T1, T2> action, T1 arg1, T2 arg2) where T : Component
        {
            EventListener.Get(go).onClick = (data) => { action(arg1, arg2); };
        }


        /// <summary> 按钮增加点击事件(滑动窗口中使用，不然不能拖动)</summary>
        public static void AddClick(this Button btn, Action action)
        {
            btn.onClick.AddListener(() => { action(); });
        }

        /// <summary>下拉框改变事件</summary>
        public static void AddChange(this Dropdown drop, UnityAction<int> action)
        {
            drop.onValueChanged.AddListener(action);
        }

        public static void AddChange<T>(this Slider slider, UnityAction<float, Slider,T> action,T arg)
        {
            slider.onValueChanged.AddListener((float value) => action(value, slider, arg));
        }
        public static void AddChange<T>(this InputField slider, UnityAction<string, InputField, T> action, T arg)
        {
            slider.onValueChanged.AddListener((string value) => action(value, slider, arg));
        }

        public static void AddChange<T>(this Toggle toogle, UnityAction<Toggle, T> action, T arg)
        {
            toogle.onValueChanged.AddListener((bool value) => action(toogle, arg));
        }
        public static void AddChange<T>(this Toggle toogle, UnityAction<T> action, T arg)
        {
            toogle.onValueChanged.AddListener((bool value) => action(arg));
        }


        /// <summary>Toggle改变事件</summary>
        public static void AddChange(this Toggle toogle, UnityAction<bool, Toggle> action)
        {
            toogle.onValueChanged.AddListener((bool value) => action(value, toogle));
        }
        public static void AddChange(this Toggle toogle, UnityAction<bool> action)
        {
            toogle.onValueChanged.AddListener((bool value) => action(value));
        }

        /// <summary>Scrol改变事件</summary>
        public static void AddChange(this ScrollRect scrollRect, UnityAction<Vector2> action)
        {
            scrollRect.onValueChanged.AddListener(action);
        }

        #region Image扩展
        /// <summary>
        /// 设置图片
        /// </summary>
        /// <param name="img">图片对象</param>
        /// <param name="uiAtlas">UIAtlas</param>
        public static async CTask SetSprite(this Image img, string spriteName, string uiAtlas = "ItemIcon",
            bool autoSetSize = false)
        {
            if (img == null) return;
            SpriteAtlas atlas = await CSF.Mgr.Assetbundle.LoadSpriteAtlas(uiAtlas);
            if (img == null) return;
            Sprite sp = atlas.GetSprite(spriteName);           
            img.sprite = sp;
            if (autoSetSize)
            {
                img.SetNativeSize();
            }
        }
        /// <summary>
        /// 加载贴图
        /// </summary>
        /// <param name="img"></param>
        /// <param name="imgName"></param>
        /// <param name="imgName">IsAutoShow</param>
        public static async CTask SetTextures(this RawImage img, string imgName)
        {
            if (img == null) return;
            Texture tex = await CSF.Mgr.Assetbundle.LoadTexture(imgName);

            if (img == null) return;
            img.texture = tex;
        }
        /// <summary>
        /// 设置图片透明度
        /// </summary>
        /// <param name="img"></param>
        /// <param name="alpha"></param>
        public static void SetAlpha<T>(this T img, float alpha) where T : MaskableGraphic
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);
        }

        #endregion

        #region 透明区域不可点击

        public static void AlphaUnClick(this Button btn)
        {
            btn.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.3f;
        }

        #endregion

        #region 变灰

        private static Material grayMaterial;

        /// <summary>
        /// 设置图片变灰
        /// </summary>
        /// <param name="img"></param>
        /// <param name="isGray">是否变灰,false/还原</param>
        public static void SetGray(this Image img, bool isGray = true)
        {
            if (isGray)
            {
                if (grayMaterial == null)
                    grayMaterial = Resources.Load<Material>("Materials/UIGray");
                img.material = grayMaterial;
            }
            else
                img.material = null;
        }

        public static void SetGray(this Button btn, bool isGray = true)
        {
            if (btn == null)
                return;
            Image img = btn.GetComponent<Image>();
            if (img != null)
                img.SetGray(isGray);
            Image[] imgChild = btn.GetComponentsInChildren<Image>();
            foreach (var variable in imgChild)
            {
                variable.SetGray(isGray);
            }
        }

        #endregion

        #region UI相关释放

        ///// <summary>
        ///// 释放UI上的Item列表
        ///// </summary>
        //public static void Dispose<T>(this List<T> list) where T : BaseItem
        //{
        //    if (list != null)
        //        list.ForEach((item) => { item.Dispose(); });
        //    list.Clear();
        //}

        #endregion

        #region 获取子对象

        /// <summary>
        /// 获取GameObject子对像，不包含自己
        /// </summary>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static List<Transform> GetChildrenTransform(this GameObject tran)
        {
            List<Transform> list = new List<Transform>();
            foreach (Transform child in tran.transform)
                list.Add(child);
            return list;
        }

        #endregion
    }
}