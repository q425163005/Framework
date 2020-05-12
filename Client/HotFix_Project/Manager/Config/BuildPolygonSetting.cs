using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFix_Project
{
    /// <summary>建筑多边形点击响应区域数据</summary>
    public class BuildPolygonSetting
    {
        /// <summary>
        /// 建筑id
        /// </summary>
        public string name { get; set; }

        public List<float[]> pos { get; set; }
    }
}
