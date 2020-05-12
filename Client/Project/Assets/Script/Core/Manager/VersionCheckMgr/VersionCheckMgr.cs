using AssetBundles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using CSF.Tasks;

namespace CSF
{
    public partial class VersionCheckMgr : BaseMgr<VersionCheckMgr>
    {
        private VersionCheckUI checkUI;

        /// <summary>更新检测是否完成</summary>
        private bool isUpdateCheckComplete = false;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="initOK"></param>
        public async CTask Check()
        {
            //创建检测UI
            UnityEngine.Object obj = Resources.Load("VersionCheck/VersionCheckUI", typeof(GameObject));
            GameObject         go  = Instantiate(obj) as GameObject;
            go.SetActive(true);
            checkUI = go.GetComponent<VersionCheckUI>();
            RectTransform tran = checkUI.GetComponent<RectTransform>();
            checkUI.transform.SetParent(Mgr.UI.canvas.transform);
            tran.offsetMin          = Vector2.zero;
            tran.offsetMax          = Vector2.zero;
            go.transform.localScale = Vector3.one;
            await CTask.WaitForNextFrame();
            SetVersion();
            SetTitle(VerCheckLang.CheckResInfo); //检测资源信息

            //版本验证并更新
            bool checkRes = true;
            //WebGL不检测资源更新
            if (AppSetting.PlatformType == EPlatformType.WebGL)
                checkRes = false;
            else if (Application.isEditor && !AppSetting.EditorVerCheckt)
                checkRes = false;

            if (checkRes)
            {
                ValidationVersion().Run();
                await CTask.WaitUntil(() => isUpdateCheckComplete);
            }
            SetValue(0, true);
            //初始化资源           
            SetInfo(VerCheckLang.InitRes, 0.8f);
            await CTask.WaitForNextFrame();
        }

        /// <summary>
        /// 设置标题
        /// </summary>
        public void SetTitle(string title)
        {
            checkUI.txtInfo.text = title;
        }

        /// <summary>
        /// 设置进度(0-1)
        /// </summary>
        public void SetValue(float val, bool immediately = false)
        {
            checkUI.DOKill(false);
            if (immediately)
            {
                checkUI.sliderProg.value = val;
            }
            else
            {
                if (val < checkUI.sliderProg.value && val != 0) return;
                checkUI.sliderProg.DOValue(val, val - checkUI.sliderProg.value);
            }
        }

        /// <summary>
        /// 设置版本号
        /// </summary>
        public void SetVersion(int resVersion = 0)
        {
            string ver = Application.version;
            if (resVersion > 0)
                ver += "." + resVersion;
            checkUI.txtVersion.text = ver;
        }

        /// <summary>
        /// 设置标题和进度
        /// </summary>
        /// <param name="title"></param>
        /// <param name="val"></param>
        public void SetInfo(string title, float val)
        {
            SetTitle(title);
            SetValue(val);
        }

        /// <summary>
        /// 关闭版本检测界面
        /// </summary>
        public async CTask Close()
        {
            SetValue(1f);
            await CTask.WaitForSeconds(0.3f);
            await Mgr.UI.UIAnim(checkUI.gameObject, EUIAnim.FadeOut);
            Dispose();
        }

        public override void Dispose()
        {
            base.Dispose();
            Destroy(checkUI.gameObject);
        }
    }
}