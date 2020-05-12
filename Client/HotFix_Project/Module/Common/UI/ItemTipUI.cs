using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using HotFix_Project.Module.Common;
using CSF.Tasks;

namespace HotFix_Project.Common
{
    public partial class ItemTipUI : BaseUI
    {
        private List<GameObject> starList = new List<GameObject>();

        private RectTransform imgBGrect;

        private Vector2 screenSize;

        private RectTransform target;

        private int       dowerItemID;
        public  bool      isSel = false;

        /// <summary>提示信息界面</summary>
        public ItemTipUI()
        {
            UINode        = EUINode.UITip; //UI节点
            OpenAnim      = EUIAnim.None;  //UI打开效果
            CloseAnim     = EUIAnim.None;  //UI关闭效果 
            EnableLoading = false;
        }

        protected override void Awake()
        {
            imgBGrect   = imgBG.GetComponent<RectTransform>();
            screenSize  = CSF.Mgr.UI.canvas.GetComponent<RectTransform>().sizeDelta;

            btnDowerStudy.AddClick(btnDowerStudy_Click); //学习天赋
            imgBG.GetComponent<Image>().AddEnter(() => { isSel = true; });
            imgBG.GetComponent<Image>().AddExit(() => { isSel  = false; });
            //for (int i = 0; i < StarContent.transform.childCount; i++)
            //{
            //    starList.Add(StarContent.transform.GetChild(i).gameObject);
            //}
        }

        public async CTask SetData(GameObject _target, string title, string content, int starNum = 0)
        {
            if (_target == null || posObj == null) return;
            posObj.transform.position = _target.transform.position;
            this.target               = _target.GetComponent<RectTransform>();
            StarContent.SetActive(starNum != 0);
            //emUtils.CreateItems(starList, starNum, imgStar);
            //CreatStar(starNum);
            
            texTitle.text = title;
            texDes.text   = content;
            imgBG.enabled = false;
            await CTask.WaitForNextFrame();
            if (imgBG == null)
                return;
            imgBG.enabled = true;
            await CTask.WaitForNextFrame();
            if (imgBGrect == null)
                return;
            Vector2 targetPos = posObj.transform.GetComponent<RectTransform>().anchoredPosition;
            targetPos = new Vector2(
                targetPos.x + target.sizeDelta.x * (0.5f - target.pivot.x),
                targetPos.y + target.sizeDelta.y * (0.5f - target.pivot.y));           
            imgBGrect.anchoredPosition = new Vector2(GetPos_x(targetPos.x), GetPos_y(targetPos.y));
            imgBG.gameObject.SetVisible(true);
            await Mgr.UI.UIAnim(imgBG.gameObject, EUIAnim.ScaleIn);
        }

      
        /// <summary>学习天赋</summary>
        void btnDowerStudy_Click()
        {
            CloseSelf();
        }


        float GetPos_x(float target_x)
        {
            float _x;
            float val = target_x - imgBGrect.sizeDelta.x / 2;

            if (val < 0) //左
            {
                _x = imgBGrect.sizeDelta.x / 2 - screenSize.x / 2;
            }
            else if (val == 0)
            {
                _x = target_x - screenSize.x / 2;
            }
            else
            {
                if (target_x + imgBGrect.sizeDelta.x / 2 > screenSize.x)
                {
                    _x = screenSize.x / 2 - imgBGrect.sizeDelta.x / 2;
                }
                else
                {
                    _x = target_x - screenSize.x / 2;
                }
            }

            return _x;
        }

        float GetPos_y(float target_y)
        {
            target_y += (CSF.Mgr.UI.canvasAdaptive.CutoutsHeight + CSF.Mgr.UI.canvasAdaptive.CutoutsBottonHeight)/2;
            float _y;
            float val = target_y - (target.sizeDelta.y / 2) - imgBGrect.sizeDelta.y - 15;
            if (val > 0) //下
            {
                _y = val + imgBGrect.sizeDelta.y / 2 - screenSize.y / 2;
            }
            else //上
            {
                _y = target_y + target.sizeDelta.y / 2 + imgBGrect.sizeDelta.y / 2 + 15 - screenSize.y / 2;
            }
            return _y;
        }

        void CreatStar(int num)
        {
            for (int i = num; i < starList.Count; i++)
            {
                starList[i].SetActive(false);
            }

            for (int i = 0; i < num; i++)
            {
                if (i == starList.Count)
                {
                    GameObject star = Object.Instantiate(imgStar, StarContent.transform);
                    starList.Add(star);
                }

                starList[i].SetActive(true);
            }
        }

        public void Close()
        {
            if (isSel) return;
            CloseSelf();
        }


        /// <summary>
        /// 弹出Tips提示
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="starNum">星级</param>
        public static void Show(GameObject target, string title, string content, int starNum = 0)
        {
            showTask(target, title, content, starNum).Run();
        }
        private static async CTask showTask(GameObject target, string title, string content, int starNum = 0)
        {
            ItemTipUI ui = Mgr.UI.GetUI<ItemTipUI>();
            if (ui == null)
            {
                ui = Mgr.UI.Show<ItemTipUI>();
            }
            await ui.Await();
            await ui.SetData(target, title, content, starNum);
        }
        
        /// <summary>释放UI引用</summary>
        public override void Dispose()
        {
        }
    }
}