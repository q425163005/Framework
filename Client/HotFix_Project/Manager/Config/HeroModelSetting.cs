using System;
using System.Collections.Generic;

namespace HotFix_Project
{
    /// <summary>英雄模型设置</summary>
    public class HeroModelSetting 
    {
        /// <summary>
        /// 模型名
        /// </summary>
       public string Model { get; set; }

        /// <summary>
        /// 位置[类型1(x,y,scale),类型2(x,y,scale)]
        /// </summary>
        public List<float[]> Pos { get; set; }
    }
}
