using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using CSF.Tasks;

namespace HotFix_Project.UIEffect
{
    //管理界面元素动画效果
    public class UIItemEffectMgr 
    {
        private float SmallerCoefficient = 0.05f;//变小变大的百分百系数
        private Material brightenMat;
        private List<UIItemEffectBase> _effectList = new List<UIItemEffectBase>();

        public Material BrightenMat { get => brightenMat; }

        public async CTask Initialize()
        {
            brightenMat = await CSF.Mgr.Assetbundle.LoadMaterial("ui/Brighten");
        }
        public void TriggerEffect(UIItemEffectEnum type,Button btn)
        {
            for (int i = 0; i < _effectList.Count; i++)
            {
                if (btn.gameObject == _effectList[i].target)
                {
                    _effectList[i].TriggerEffect();
                    return;
                }
            }
            UIItemEffectBase effectBase = CreateEffectItem(type, btn);
            effectBase.TriggerEffect();
            _effectList.Add(effectBase);
        }
        public void ResetEffect(Button btn)
        {
            for (int i = 0; i < _effectList.Count; i++)
            {
                if (btn.gameObject == _effectList[i].target && _effectList[i].target!=null)
                {
                    _effectList[i].ResetEffect();
                    return;
                }
            }
            RefreshList();
        }

        void RefreshList()
        {
            for (int i = 0; i < _effectList.Count; i++)
            {
                if (_effectList[i].target == null)
                    _effectList.Remove(_effectList[i]);
            }
        }
        UIItemEffectBase CreateEffectItem(UIItemEffectEnum type, Button btn)
        {
            UIItemEffectBase Effectbase = new UIButtonEffect(type,btn, SmallerCoefficient);
            return Effectbase;
        }
   
        public  void Dispose()
        {
            _effectList.Clear();
        }

    }
}
