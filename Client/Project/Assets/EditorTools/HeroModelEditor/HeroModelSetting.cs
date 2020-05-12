using System;
using System.Collections.Generic;

namespace HeroModelEditor
{
    /// <summary>英雄模型设置</summary>
    public class HeroModelSetting
    {
        /// <summary>
        /// 模型名
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// 位置/缩放  [x,y,s]
        /// </summary>
        public List<float[]> Pos { get; set; }
    }
}
