using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MapEditor
{
    public class WaveItem : MonoBehaviour
    {
        public Toggle toggSelect;
        public Text txtWaveNum;
        public Text txtNum;

        // Start is called before the first frame update

        public int Index; //波数索引
        void Awake()
        {
            toggSelect.AddChange(toggSelect_Change);
        }

        //选择波数
        void toggSelect_Change(bool isOn)
        {
            if (isOn)
                MapEditor.I.SetWaveMonster(Index);
        }

        public void SetData(int index=0,int count=0)
        {
            Index = index;
            txtWaveNum.text = $"第{index + 1}波";
            SetNum(count);
        }

        public void SetNum(int count)
        {
            txtNum.text = $"{count}怪";
            txtNum.color = count == 0 ? Color.red : Color.green;
        }

        public void SetSelect(bool isOn)
        {
            if (toggSelect.isOn == isOn)
                toggSelect_Change(isOn);
            else
                toggSelect.isOn = isOn;
        }
        public bool IsSelect => toggSelect.isOn;

        public void Dispose()
        {
        }
    }
}
