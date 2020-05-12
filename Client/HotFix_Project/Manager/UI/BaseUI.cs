using CSF.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace HotFix_Project
{
    public class BaseUI : UIObject
    {
        /// <summary>
        /// UI所属节点类型
        /// </summary>
        public EUINode UINode = EUINode.UIWindow;

        /// <summary>
        /// 打开UI传进来的参数
        /// </summary>
        protected object[] _args;

        /// <summary>
        /// 打开UI的效果
        /// </summary>
        protected EUIAnim OpenAnim = EUIAnim.None;

        /// <summary>
        /// 关闭UI的效果
        /// </summary>
        protected EUIAnim CloseAnim = EUIAnim.None;

        /// <summary>
        /// 是否启用加载等待动画(默认不开启)
        /// </summary>
        protected bool EnableLoading = false;

        public Action CloseAction;

        /// <summary>
        /// UI路径名,自动获取,跟据UI命名空间(如果不符合自己重写此方法)
        /// UI预制路径和UI类名保持一致
        /// 获取结果为:Login/LoginUI
        /// </summary>
        public virtual string UIPath
        {
            get
            {
                Type type = this.GetType();
                return type.Namespace.Substring(type.Namespace.LastIndexOf(".") + 1) + "/" + type.Name;
            }
        }

        /// <summary>刷新界面</summary>
        public virtual void Refresh()
        {
        }

        /// <summary>打开UI</summary>
        public async CTask _openUI(params object[] args)
        {
            _args = args;
            showLoading();
            GameObject obj = await Mgr.UI.LoadUI(UIPath, UINode.ToString());
            if (m_isDispose) //UI已经销毁掉了
            {
                GameObject.DestroyImmediate(obj);
                return;
            }

            if (obj == null) return;
            initGameObject(obj);
            //addCanvas();
            closeLoading();
            Refresh();
            onTriggerOpenUI();
            await Mgr.UI.UIAnim(obj, OpenAnim);
            OpenLater();
        }
        //触发打开UI事件，某个UI如果需要单位处理重写此方法
        protected virtual void onTriggerOpenUI()
        {
            //GuideTrigger.OpenUI(this);
        }

        /// <summary>打开界面之后执行，在执行动画完成后</summary>
        protected virtual void OpenLater()
        {
        }

        protected virtual void showLoading()
        {
            if (EnableLoading)
                Mgr.UI.Loading.SetVisible(true);
        }

        protected virtual void closeLoading()
        {
            if (EnableLoading)
                Mgr.UI.Loading.SetVisible(false);
        }

        void addCanvas()
        {
            Canvas canvas = transform.GetComponent<Canvas>();
            if (canvas == null)
                canvas = transform.gameObject.AddComponent<Canvas>();
            canvas.overrideSorting = true;
            canvas.sortingOrder    = Mgr.UI.GetNodeNum(UINode) * 100;
            if (UINode == EUINode.UIWar)
            {
                canvas.additionalShaderChannels = AdditionalCanvasShaderChannels.TexCoord1 |
                                                  AdditionalCanvasShaderChannels.Normal    |
                                                  AdditionalCanvasShaderChannels.Tangent;
            }

            canvas.sortingLayerID = SortingLayer.NameToID(UINode.ToString());

            if (transform.GetComponent<GraphicRaycaster>() == null)
                gameObject.AddComponent<GraphicRaycaster>();

            //子集canvas层级处理
            LayerManager[] layers = this.transform.GetComponentsInChildren<LayerManager>();
            for (int i=0;i< layers.Length;i++)
                layers[i].SetLayer(UINode.ToString(), canvas.sortingOrder);
        }

        /// <summary>
        /// 等待界面加载完成
        /// </summary>
        public virtual async CTask Await()
        {
            await CTask.WaitUntil(() => { return gameObject != null || m_isDispose; });
        }

        /// <summary>关闭当前UI</summary>
        public virtual void CloseSelf()
        {
            CloseAction?.Invoke();

            Mgr.UI.CloseForName(GetType().Name);
        }

        /// <summary>关闭UI</summary>
        public async CTask _closeUI()
        {
            CloseBefore();
            await Mgr.UI.UIAnim(gameObject, CloseAnim);
            m_isDispose = true;
            //await CTask.WaitForEndOfFrame();
            Dispose();
            GameObject.DestroyImmediate(gameObject);
        }

        /// <summary>关闭之前执行，在执行动画前面</summary>
        protected virtual void CloseBefore()
        {
        }

        /// <summary>
        /// 获得指定数据
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>数据</returns>
        public T GetArg<T>(int index)
        {
            return _args != null && _args.Length > index ? (T) _args[index] : default(T);
        }

        /// <summary>
        /// 获得第一个指定类型的数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>数据</returns>
        public T GetArg<T>()
        {
            T value = default(T);
            for (int i=0;i< _args.Length;i++)
            {
                if (_args[i] is T)
                    return (T)_args[i];
            }

            return value;
        }

        //父节点是否显示
        public bool IsParentVisible()
        {
            GameObject node =  Mgr.UI.GetUINode(UINode).gameObject;
            //界面隐藏了
            return node.IsVisible() && node.activeSelf;
        }

        public override void Dispose()
        {
            base.Dispose();
            _args = null;
        }
    }
}