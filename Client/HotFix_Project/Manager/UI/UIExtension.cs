using CSF.Tasks;
using DG.Tweening;
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

namespace HotFix_Project
{
    public delegate void BaseDataArgDelegate(object argl, BaseEventData eventData);

    public delegate void PointerDataArgDelegate(object arg, PointerEventData eventData);

    public delegate void AxisDataArgDelegate(object arg, AxisEventData eventData);

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

        /// <summary> GameObject 点击事件,带返回参数,点击点数据</summary>
        public static void AddClick(this GameObject go, PointerDataArgDelegate action, object arg = null)
        {
            EventListener.Get(go).onClick = (data) => { action(arg, data); };
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

        /// <summary> 控件点击事件</summary>
        public static void AddClick<T>(this T go, PointerDataArgDelegate action, object arg = null) where T : Component
        {
            EventListener.Get(go).onClick = (data) => { action(arg, data); };
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
            //修复Unity2019下拉框在不在Default层显示不出来的BUG
            drop.AddClick(() => {
                var list = drop.GetComponentsInChildren<Canvas>();
                Canvas parent = drop.GetComponentInParent<Canvas>();
                if (parent != null)
                {
                    for (int i = 0; i < list.Length; i++)
                        list[i].sortingLayerID = parent.sortingLayerID;
                }
            });
        }

        /// <summary>Toggle改变事件</summary>
        public static void AddChange(this Toggle toogle, UnityAction<bool, Toggle> action)
        {
            toogle.onValueChanged.AddListener((bool value) => action(value, toogle));
        }

        /// <summary>Scrol改变事件</summary>
        public static void AddChange(this ScrollRect scrollRect, UnityAction<Vector2> action)
        {
            scrollRect.onValueChanged.AddListener(action);
        }

        /// <summary>Slider改变事件</summary>
        public static void AddChange(this Slider slider, UnityAction<float> action)
        {
            slider.onValueChanged.AddListener(action);
        }

        /// <summary>input改变事件</summary>
        public static void AddChange(this InputField input, UnityAction<string> action)
        {
            input.onValueChanged.AddListener(action);
        }

        /// <summary>控件拖拽结束事件</summary>
        public static void AddDragEnd(this GameObject go, Action action)
        {
            DragEventListener.Get(go).onEndDrag = (data) => { action(); };
        }

        //================================================================================================================
        /// <summary> GameObject 进入事件</summary>
        public static void AddEnter(this GameObject go, Action action)
        {
            EventListener.Get(go).onEnter = (data) => { action(); };
        }

        /// <summary> GameObject 移出事件</summary>
        public static void AddExit(this GameObject go, Action action)
        {
            EventListener.Get(go).onExit = (data) => { action(); };
        }

        /// <summary> 进入事件</summary>
        public static void AddEnter<T>(this T go, Action action) where T : Component
        {
            EventListener.Get(go).onEnter = (data) => { action(); };
        }

        /// <summary> 移出事件</summary>
        public static void AddExit<T>(this T go, Action action) where T : Component
        {
            EventListener.Get(go).onExit = (data) => { action(); };
        }

        #region Image扩展

        /// <summary>
        /// 设置图片
        /// </summary>
        /// <param name="img">图片对象</param>
        /// <param name="uiAtlas">UIAtlas</param>
        public static async CTask SetSprite(this Image img, string spriteName, string uiAtlas = UIAtlas.ItemIcon,
            bool                                      autoSetSize = false,float size=1f)
        {
            if (img == null) return;
            SpriteAtlas atlas = await CSF.Mgr.Assetbundle.LoadSpriteAtlas(uiAtlas);
            if (img == null) return;
            Sprite sp = atlas.GetSprite(spriteName);
            if (sp == null)
            {
                atlas = await CSF.Mgr.Assetbundle.LoadSpriteAtlas(UIAtlas.PublicBK);
                sp    = atlas.GetSprite("Default");
            }

            img.sprite = sp;
            if (autoSetSize)
            {
                img.SetNativeSize();
                img.rectTransform.sizeDelta *= size;
            }
        }

        /// <summary>
        /// 批量设置相同图片
        /// </summary>
        /// <param name="img">图片对象</param>
        /// <param name="uiAtlas">UIAtlas</param>
        public static async CTask SetSprites(this Image[] images, string spriteName, string uiAtlas = UIAtlas.ItemIcon,
            bool autoSetSize = false)
        {
            if (images == null) return;
            SpriteAtlas atlas = await CSF.Mgr.Assetbundle.LoadSpriteAtlas(uiAtlas);

            for (int i = 0; i < images.Length; i++)
            {
                if (images[i] != null)
                {
                    images[i].sprite = atlas.GetSprite(spriteName);
                    if (autoSetSize)
                        images[i].SetNativeSize();
                }
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
            Texture tex = await LoadHelper.LoadTexture(imgName);
            if (img == null) return;
            img.texture = tex;
        }


        /// <summary>
        /// 设置Graphic透明度
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
            Image img = btn.GetComponent<Image>();
            if (img != null)
                img.SetGray(isGray);
            Image[] imgChild = btn.GetComponentsInChildren<Image>();
            for (int i=0;i<imgChild.Length;i++)
            {
                imgChild[i].SetGray(isGray);
            }
            //btn.SetColorByGray(isGray);
        }
        public static void SetColorByGray(this Button btn, bool isGray = true)
        {
            Text[] txts = btn.GetComponentsInChildren<Text>();

            for (int i = 0; i < txts.Length; i++)
            {
                txts[i].color = Utils.GetColorByRGB(isGray == true ? "525252C8" : "501400C8");
            }


        }

        public static void SetGrayButton(this Button btn, bool isGray = true)
        {
            Image img = btn.image;
            if (isGray)
            {
                img.SetSprite("Btn1_2", UIAtlas.PublicButton).Run();
                btn.GetComponent<Image>().SetSprite("Btn1_BG2", UIAtlas.PublicButton).Run();
            }
            else
            {
                btn.GetComponent<Image>().SetSprite("Btn1_BG", UIAtlas.PublicButton).Run();
            }
        }

        #endregion

        #region 高亮

        private static Material lightMaterial;

        /// <summary>
        /// 设置图片高亮
        /// </summary>
        /// <param name="img"></param>
        /// <param name="isLight">是否高亮,false/还原</param>
        public static void SetLight(this Image img, bool isLight = true)
        {
            img.SetLight(isLight, 1.5f);
        }

        public static void SetLight(this Button btn, bool isLight = true)
        {
            Image img = btn.GetComponent<Image>();
            if (img != null)
                img.SetLight(isLight);
            Image[] imgChild = btn.GetComponentsInChildren<Image>();
            for(int i=0;i< imgChild.Length;i++)
            {
                imgChild[i].SetLight(isLight);
            }
        }

        /// <summary>
        /// 设置图片白色高亮
        /// </summary>
        /// <param name="img"></param>
        /// <param name="isLight">是否高亮,false/还原</param>
        public static void SetLight(this Image img, bool isLight = true, float Bright = 1.5f)
        {
            if (isLight)
            {
                if (lightMaterial == null)
                    lightMaterial = Resources.Load<Material>("Materials/UILight");
                img.material = lightMaterial;
                img.material.SetFloat("_Bright", Bright);
            }
            else
                img.material = null;
        }

        #endregion

        #region 闪烁

        private static Material flashMaterial;

        /// <summary>
        /// 设置图片闪烁
        /// </summary>
        /// <param name="img"></param>
        /// <param name="isflash">是否闪烁,false/还原</param>
        public static void Setflash(this Image img, bool isflash = true)
        {
            if (isflash)
            {
                if (flashMaterial == null)
                    flashMaterial = Resources.Load<Material>("Materials/UIFlash");
                img.material = flashMaterial;
            }
            else
                img.material = null;
        }

        public static void Setflash(this Button btn, bool isflash = true)
        {
            Image img = btn.GetComponent<Image>();
            if (img != null)
                img.Setflash(isflash);
            Image[] imgChild = btn.GetComponentsInChildren<Image>();
        
            for (int i = 0; i < imgChild.Length; i++)
            {
                imgChild[i].Setflash(isflash);
            }
        }

        #endregion

        /// <summary>
        /// 关闭所有效果
        /// </summary>
        /// <param name="img"></param>
        public static void CloseAllLight(this Image img)
        {
            img.material = null;
        }

        #region UI相关释放

        /// <summary>
        /// 释放UI上的Item列表
        /// </summary>
        public static void Dispose<T>(this List<T> list) where T : BaseItem
        {
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                    list[i].Dispose();
                list.Clear();
            }
        }

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

        #region 获取对象的RectTransform

        public static RectTransform MyRectTransform(this GameObject obj)
        {
            RectTransform rect = obj.GetComponent<RectTransform>();
            if (rect == null)
                CLog.Error($"对象[{obj.name}]的RectTransform组件未找到");
            return rect;
        }

        public static RectTransform RectTransform(this GameObject obj)
        {
            return obj.transform as RectTransform;
        }
        #endregion
    }
}