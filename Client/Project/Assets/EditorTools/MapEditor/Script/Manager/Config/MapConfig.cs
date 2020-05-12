using System.Collections.Generic;

namespace MapEditor
{
    public class MapConfig
    {
        //地图Id 和副本关卡Id保持一致
        public int id { get; set; }

        //type: 地图类型  1副本地图  2活动副本
        public int type { get; set; } = 1;

        //怪物波数
        public List<List<MapMonster>> monster { get; set; }
        //初始符号配置
        public List<MapSymbol> symbols { get; set; }
    }

    public class MapMonster
    {
        ////怪物配置ID
        public int mId { get; set; }

        //place:怪物位置
        public int place { get; set; }
        /// <summary>
        /// 占格大小
        /// </summary>
        public int size { get; set; }

        //光环大小
        public int haloSize { get; set; }

        //所在位置X偏移
        public int offX { get; set; }

        //所在位置Y偏移
        public int offY { get; set; }
    }

    public class MapSymbol
    {
        public int index; //位置索引
        public int type; //符号类型
        public int state; //状态(0正常 1冻结 2封印 3炸弹)
        public int stack; //层叠数
    }
   
}