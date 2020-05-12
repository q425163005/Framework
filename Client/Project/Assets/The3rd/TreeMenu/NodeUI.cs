using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace TreeMenuSpace
{
    public class NodeUI : MonoBehaviour
    {
        public NodeData Data;
        /// <summary>
        /// 箭头按钮
        /// </summary>
        public Button btnArrow;
        public Text textName;
        [SerializeField]
        RectTransform rectTransform;
        public Action<NodeUI> BtnOnClick;
        public Action<NodeUI> ClickSelf;
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            btnArrow.onClick.AddListener(OnClickBtn);
        }
        /// <summary>
        /// 点击箭头
        /// </summary>
        void OnClickBtn()
        {
            BtnOnClick?.Invoke(this);
            SetArrowBtnState(Data.NodeIsOpen);
        }
        /// <summary>
        /// 点击自身
        /// </summary>
        public void OnClickSelf()
        {
            if (Data.LevelX < 3)
                return;
            ClickSelf?.Invoke(this);
            Debug.Log(Data.id);
        }

        void SetArrowBtnState(bool i)
        {
            if (i)
                btnArrow.transform.localEulerAngles = new Vector3(0, 0, 0);
            else
                btnArrow.transform.localEulerAngles = new Vector3(0, 0, 90);
        }
        public void SetData(NodeData nodeData)
        {
            Data = nodeData;
            textName.text = nodeData.Name;
            rectTransform.anchoredPosition = new Vector2(Data.PosX, 0);
            btnArrow.gameObject.SetActive(Data.NodeDatas.Count <= 0 ? false : true);
            SetArrowBtnState(Data.NodeIsOpen);
        }
        /// <summary>
        /// 被选中
        /// </summary>
        public void Select(bool i)
        {
            if(i)
                textName.color = Color.red;
            else
                textName.color = Color.black;
        }

        public void SetPos()
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, -Data.PosY); 
        }


        public void DestroyItem()
        {
            Data = null;
            Destroy(this.gameObject);
            BtnOnClick = null;
            ClickSelf = null;
        }
    }
}

