using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFix_Project
{
    /// <summary>副本位置数据</summary>
    public class FBMaplPosSetting
    {
        public float[] item_Pos;
        public float[] lock_Pos;
        public float[] title_Pos;

        public List<LevelItemPosData> levelItem_data;
    }

    /// <summary>关卡位置数据</summary>
    public class LevelItemPosData
    {
        public float[] item_Pos;
        public float[] point1_Pos;
        public float[] point2_Pos;
    }
}
