using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFix_Project.UIEffect
{
    //界面元素动画，界面按钮元素默认变暗
    public enum UIItemEffectEnum
    {
        Smaller,               //变小
        Bigger,                //变大
        Brighten,              //变亮
        Darken,                //变暗
        BtnSmallerAndBrighten, //按钮变小变亮
        BtnBiggerAndBrighten,  //按钮变大变亮
        BtnSmallerAndDarken,   //变小变暗
        BtnBiggerAndDarken,    //变大变暗
    }
}