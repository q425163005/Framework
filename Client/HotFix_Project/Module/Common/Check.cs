using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotFix_Project.Common;

namespace HotFix_Project.Module.Common
{
    public class Check
    {
        

        /// <summary>
        /// 设置界面层级
        /// </summary>
        /// <param name="baseUI"></param>
        /// <param name="Node"></param>
        public static EUINode SetUILayer(EUINode node)
        {
            //if (Mgr.UI.GetUI<War.WarUI>() != null)
            //{
            //    return EUINode.UIWar;
            //}
            return node;
        }
    }
}