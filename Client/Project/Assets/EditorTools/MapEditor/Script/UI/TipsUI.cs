using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace MapEditor
{
    public class TipsUI : MonoBehaviour
    {

        public Text labContent;

        void Start()
        {
        }

        private float leftTime = 0;
        void show(string content,bool isError)
        {
            labContent.text = content;
            labContent.color = isError ? Color.red : Color.green;
            gameObject.SetActive(true);
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InOutBack);
            leftTime = 1.5f;
        }

        void Update()
        {
            leftTime -= Time.deltaTime;
            if (leftTime < 0)
                close();
        }

        void close()
        {
            gameObject.SetActive(false);
        }


        public static void ShowError(string content)
        {
            UIRoot.I.Tips.show(content,true);
        }

        public static void Show(string content)
        {
            UIRoot.I.Tips.show(content,false);
        }
    }
}
