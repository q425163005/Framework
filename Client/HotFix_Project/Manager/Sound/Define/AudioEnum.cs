using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFix_Project
{
    //声音的类型，枚举元素名称必须与声音文件名相同。
    public enum AudioEnum
    {
        bg001 = 0,//背景音乐01
        bg002,//背景音乐02
        war_ready,//战斗准备阶段
        war_run,//战斗进行中
       
        war_win,    //胜利
        war_failure, //失败
        war_bomb, //爆炸
        war_flash, //闪电
        war_symbolhit,  //士兵向上击中
    }
}
