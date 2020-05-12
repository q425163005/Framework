using DG.Tweening;
//using HotFix_Project.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using CSF.Tasks;

namespace HotFix_Project.Manager
{
    public class UIMgr
    {
        public static Dictionary<string, BaseUI> _regUI = new Dictionary<string, BaseUI>();
        private Dictionary<string, BaseUI> _uiList = new Dictionary<string, BaseUI>();

        //private Dictionary<EUINode, GameObject> _uiNodeList = new Dictionary<EUINode, GameObject>();
        private Canvas m_canvas;

        private GameObject m_loading;
        public Image LoadingMask;

        
        public Canvas Canvas
        {
            get
            {
                if (m_canvas == null)
                    m_canvas = CSF.Mgr.UI.canvas.GetComponent<Canvas>();
                return m_canvas;
            }
        }
        
        public GameObject Loading
        {
            get
            {
                if (m_loading == null)
                {
                    m_loading = CSF.Mgr.UI.UIRoot.transform.Find("Loading").gameObject;
                    LoadingMask = m_loading.transform.Find("Mask").GetComponent<Image>();
                    //Mgr.Effect.Show(3, m_loading.transform);
                }
                return m_loading;
            }
        }

        public T Show<T>(params object[] args) where T : BaseUI, new()
        {
            Type type = typeof(T);
            string name = type.Name;
            T ui = null;
            if (_uiList.ContainsKey(name))
                ui = (T)_uiList[name];
            try
            {
                if (ui == null)
                {
                    ui = new T();
                    _uiList.Add(name, ui);
                    ui._openUI(args).Run();
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message + ex.StackTrace);
            }
            return ui;
        }

        /// <summary>
        /// 跟据UI名打开UI
        /// </summary>
        /// <param name="uiName">模块名.UI名</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public BaseUI Open(string uiName, params object[] args)
        {
            Type type = Type.GetType("HotFix_Project." + uiName);
            BaseUI ui = null;
            string name = type.Name;
            if (_uiList.ContainsKey(name))
                ui = _uiList[name];
            try
            {
                if (ui == null)
                {
                    ui = Activator.CreateInstance(type) as BaseUI;
                    _uiList.Add(name, ui);
                    ui._openUI(args).Run();
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message + ex.StackTrace);
            }            
            return ui;
        }

        /// <summary>
        /// 关闭UI
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Close<T>() where T : BaseUI
        {
            string name = typeof(T).Name;
            CloseForName(name);
        }


        /// <summary>
        /// 关闭全部UI
        /// </summary>
        public void CloseAll()
        {
            //for (int i = _uiList.Count; i-- > 0;)
            //    _uiList.Values.ElementAt(i)._closeUI();
            //_uiList.Clear();
            closeList.Clear();
            foreach (string uiName in _uiList.Keys)
                closeList.Add(uiName);
            for (int i = 0; i < closeList.Count; i++)
                CloseForName(closeList[i]);
        }


        /// <summary>
        /// 关闭除HomeUI 和 指引界面的其它所有界面
        /// </summary>
        List<string> closeList = new List<string>();
        public void CloseHomeOther()
        {
            //string uiName;
            //for (int i = _uiList.Count; i-- > 0;)
            //{
            //    uiName = _uiList.Keys.ElementAt(i);
            //    if (uiName != "HomeUI" && uiName != "GuideUI")
            //    {
            //        _uiList[uiName]._closeUI();
            //        _uiList.Remove(uiName);
            //    }
            //}
            closeList.Clear();
            foreach (string uiName in _uiList.Keys)
            {
                if (uiName != "HomeUI" && uiName != "GuideUI" && uiName != "Loading")
                    closeList.Add(uiName);
            }
            for (int i = 0; i < closeList.Count; i++)
                CloseForName(closeList[i]);
        }


        /// <summary>
        /// 跟据UI名字关闭UI
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void CloseForName(string name)
        {
            if (_uiList.ContainsKey(name))
            {
                BaseUI ui = _uiList[name];
                _uiList.Remove(name);
                ui._closeUI().Run();
                CheckGoHomeUI();
            }
        }

        //检测是否触发回到HomeUI
        public void CheckGoHomeUI()
        {
            if (_uiList.Count <= 2)
            {
                //GuideUI guideUI = Mgr.UI.GetUI<GuideUI>();
                ////有指引UI，但是隐藏了 或者只有一个HomeUI
                //if ((guideUI != null && !guideUI.IsVisible) || _uiList.Count==1)  
                //{
                //    HomeUI ui = GetUI<HomeUI>();
                //    if (ui != null && ui.homeState == EHomeState.InHome)
                //    {
                //        //CLog.Error("GOHome");
                //        Alliance.AllianceMgr.I.OpenTitanRequest();
                //    }
                //}
            }
        }

        /// <summary>
        /// 刷新UI
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Refresh<T>() where T : BaseUI
        {
            T ui = GetUI<T>();
            if (ui != null)
                ui.Refresh();
        }

        /// <summary>
        /// 设置UI隐藏和显示
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void SetActive<T>(bool isActive) where T : BaseUI
        {
            T ui = GetUI<T>();
            if (ui != null)
                ui.SetActive(isActive);
        }

        /// <summary>
        /// 获取UI,UI没打开时返回null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetUI<T>() where T : BaseUI
        {
            string name = typeof(T).Name;
            if (_uiList.ContainsKey(name))
                return (T)_uiList[name];
            return null;
        }

        public bool IsOpen<T>() where T : BaseUI
        {
            var ui = GetUI<T>();
            if (ui == null) return false;
            return ui.IsVisible && ui.IsActive;
        }

        /// <summary>
        /// 跟据UI名字获取UI
        /// </summary>
        /// <param name="name"></param>
        public BaseUI GetUI(string name)
        {
            if (_uiList.ContainsKey(name))
            {
                BaseUI ui = _uiList[name];
                return ui;
            }

            return null;
        }

        /// <summary>
        /// 获取当前在Node下的界面个数
        /// </summary>
        /// <returns></returns>
        public int GetNodeNum(EUINode val)
        {

            int num = _uiList.Values.Count(s => s.UINode == val);
            return num;
        }


        /// <summary>
        /// 获取当前在Window下的界面个数
        /// </summary>
        /// <returns></returns>
        public int GetWindowNum()
        {
            int num = 0;
            foreach (var variable in _uiList.Values)
            {
                if (variable.UINode == EUINode.UIWindow)
                {
                    num++;
                }
            }

            return num;
        }
        /// <summary>
        /// 获取当前在Window下的界面个数
        /// </summary>
        /// <returns></returns>
        public int GetWarNum()
        {
            int num = 0;
            foreach (var variable in _uiList.Values)
            {
                if (variable.UINode == EUINode.UIWar)
                {
                    num++;
                }
            }

            return num;
        }


        /// <summary>
        /// 渐现菜单
        /// </summary>
        /// <param name="targetGO">菜单游戏对象</param>
        public async CTask UIAnim(GameObject target, EUIAnim anim)
        {
            float time = 0.5f;
            switch (anim)
            {
                case EUIAnim.FadeOut:
                    time = 0.35f;
                    break;
                case EUIAnim.ScaleOut:
                    time = 0.2f;
                    break;
            }

            await UIUtils.ObjectAnim(target, anim, time);
        }

        /// <summary>
        /// 跳转到主界面场景
        /// </summary>
        /// <returns></returns>
        public async CTask GoHomeScene()
        {
            //await Mgr.UI.Show<HomeUI>().Await();
            if (SceneManager.GetActiveScene().name != "Home")
            {
                await CSF.Mgr.Assetbundle.LoadScene("Home");
            }
            await CTask.WaitForSeconds(0.25f);
        }
              
        /// <summary>
        /// 加载UI
        /// </summary>
        /// <param name="uiPath">相对于UI目录下的路径如:Login/Login</param>
        public async CTask<GameObject> LoadUI(string uiPath, string uiNode)
        {
            string uiName = uiPath.Substring(uiPath.LastIndexOf("/") + 1);
            GameObject go = await CSF.Mgr.Assetbundle.LoadPrefab("UI/" + uiPath, uiName);
            go.transform.SetParent(_GetUINode(uiNode));
            RectTransform rect = go.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            return go;
        }
        private Dictionary<string, Transform> _uiNodeList = new Dictionary<string, Transform>();
        /// <summary>
        /// 获取UI节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private Transform _GetUINode(string node)
        {
            Transform tran;
            if (!_uiNodeList.TryGetValue(node, out tran))
            {
                tran = CSF.Mgr.UI.UIRoot.transform.Find(node);
                if (tran == null)
                {
                    CLog.Error("未找到UI节点:" + node);
                    tran = CSF.Mgr.UI.canvas.transform;
                }
                _uiNodeList.Add(node, tran);
            }
            return tran;
        }
        public Transform GetUINode(EUINode node)
        {
            return _GetUINode(node.ToString());
        }
    }
}