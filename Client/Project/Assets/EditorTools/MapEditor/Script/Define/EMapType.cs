using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor
{
    /// <summary>
    /// 地图类型,和EWarType保持一至
    /// </summary>
    public enum EMapType
    {

        /// <summary>无</summary>
        None = 0,
        /// <summary>副本</summary>
        FB = 1,
        /// <summary>活动副本</summary>
        EventFB = 2,
        /// <summary>泰坦</summary>
        Titan = 3,

        //.....预留其它需要配置战斗地图用
        /// <summary>竞技场</summary>
        Arena = 10,
        /// <summary>指引战斗</summary>
        Guide = 11,
    }
}
