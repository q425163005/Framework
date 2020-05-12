using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using CSF.Tasks;

namespace CSF
{
    public class UIMgr : BaseMgr<UIMgr>
    {
        public Canvas canvas;
        public CanvasAdaptive canvasAdaptive;

        public Camera UICamera;
        public RectTransform UIRoot;
        private Dictionary<string, RectTransform> _uiNodeList = new Dictionary<string, RectTransform>();

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            canvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
            UICamera = canvas.worldCamera;
            canvasAdaptive = canvas.GetComponent<CanvasAdaptive>();
            DontDestroyOnLoad(canvas);
        }
        public async CTask InitializeUIRoot()
        {
            GameObject obj = await Mgr.Assetbundle.LoadPrefab("UI/UIRoot", "UIRoot");
            UIRoot = obj.GetComponent<RectTransform>();
            UIRoot.transform.SetParent(canvas.transform);
            UIRoot.anchorMin = Vector2.zero;
            UIRoot.anchorMax = Vector2.one;
            UIRoot.offsetMin = Vector2.zero;
            UIRoot.offsetMax = Vector2.zero;
            UIRoot.localScale = Vector3.one;

            foreach (var node in obj.GetComponentsInChildren<RectTransform>())
            {
                if (node.name.StartsWith("UI") && node != UIRoot)
                    _uiNodeList.Add(node.name, node);
            }
#if UNITY_EDITOR
            if (Main.I != null)
            {
                canvasAdaptive.CutoutsHeight = Main.I.TestCutoutsHeight;
                canvasAdaptive.CutoutsBottonHeight = Main.I.TestCutoutsHeight / 2;
            }
#endif
            SetNodePadding();
        }

        /// <summary>
        /// 刷新语言
        /// </summary>
        public void RefreshLang()
        {
            UILangText[] list = canvas.GetComponentsInChildren<UILangText>();
            for (int i = list.Length; --i >= 0;)
                list[i].Refresh();
        }
        /// <summary>
        /// 设置流海UI节点缩进
        /// </summary>
        public void SetNodePadding()
        {
            foreach (var kv in _uiNodeList)
            {
                //Top
                kv.Value.offsetMax = new Vector2(0, -canvasAdaptive.CutoutsHeight);
                //Botton
                kv.Value.offsetMin = new Vector2(0, canvasAdaptive.CutoutsBottonHeight);
            }
        }
        /// <summary>
        /// 设置UI背景图片缩进,填充流海缩进的大小
        /// </summary>
        public void SetUIBgPadding(RectTransform rect)
        {
            if (rect == null) return;
            rect.offsetMax = new Vector2(0, canvasAdaptive.CutoutsHeight);
            rect.offsetMin = new Vector2(0, -canvasAdaptive.CutoutsBottonHeight);
        }

        /// <summary>
        /// 加载UI
        /// </summary>
        /// <param name="uiPath">相对于UI目录下的路径如:Login/Login</param>
        public async CTask<GameObject> LoadUI(string uiPath, string uiNode)
        {
            string uiName = uiPath.Substring(uiPath.LastIndexOf("/") + 1);
            GameObject go = await Mgr.Assetbundle.LoadPrefab("UI/" + uiPath, uiName);
            await CTask.WaitForNextFrame();
            go.transform.SetParent(_GetUINode(uiNode));
            RectTransform rect = go.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
#if UNITY_EDITOR
            //模拟手机加载延时效果...
            if (AppSetting.LoaddelayTest)
                await CTask.WaitForSeconds(0.5f);
#endif
            return go;
        }
        /// <summary>
        /// 获取UI节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private Transform _GetUINode(string node)
        {
            RectTransform tran;
            if (!_uiNodeList.TryGetValue(node, out tran))
            {
                tran = UIRoot.transform.Find(node) as RectTransform;
                if (tran == null)
                {
                    CLog.Error("未找到UI节点:" + node);
                    tran = UIRoot;
                }
                _uiNodeList.Add(node, tran);
            }
            return tran;
        }

        /// <summary>
        /// UI渐隐渐显效果(主工程用)
        /// </summary>
        /// <param name="targetGO">菜单游戏对象</param>
        public async CTask UIAnim(GameObject target, EUIAnim anim)
        {
            if (anim == EUIAnim.None) return;
            //UI淡入淡出效果
            if (anim == EUIAnim.FadeIn || anim == EUIAnim.FadeOut)
            {
                Graphic[] comps = target.GetComponentsInChildren<Graphic>();
                for (int i = comps.Length; --i >= 0;)
                {
                    if (anim == EUIAnim.FadeIn)
                        comps[i].DOFade(0, 0.5f).From();
                    else
                        comps[i].DOFade(0, 0.5f);
                }
                await CTask.WaitForSeconds(0.5f);
            }
            else if (anim == EUIAnim.ScaleIn || anim == EUIAnim.ScaleOut)
            {
                if (anim == EUIAnim.ScaleIn)
                {
                    target.transform.DOScale(0, 0.5f).SetEase(Ease.OutBack).From();
                    await CTask.WaitForSeconds(0.5f);
                }
                else
                {
                    target.transform.DOScale(0, 0.3f).SetEase(Ease.InBack);
                    await CTask.WaitForSeconds(0.3f);
                }
            }
        }
    }
}
