using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor
{
    /// <summary>
    /// 怪物格子状态
    /// </summary>
    public enum EMapGridState
    {
        /// <summary>没有</summary>
        None = 0,
        /// <summary>有怪</summary>
        Have = 1,
        /// <summary>拖动经过</summary>
        Drag = 2,
        /// <summary>拖动经过有怪</summary>
        DragHave = 3,
    }
}
