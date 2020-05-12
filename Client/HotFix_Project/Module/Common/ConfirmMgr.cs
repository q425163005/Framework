using System;
using System.Collections.Generic;

namespace HotFix_Project.Common
{
    /// <summary>
    /// 战斗结算弹框管理
    /// </summary>
    public class ConfirmMgr : BaseDataMgr<ConfirmMgr>, IDisposable
    {
        Queue<Action> QueueUIConfirm = new Queue<Action>();
        
        public int CraftConfigId = -1;
        /// <summary>
        /// 战斗结算,玩家升级了
        /// </summary>
        void PlayerLeveUp_event()
        {
            //QueueUIConfirm.Enqueue(PlayerMgr.I.ShowPlayerLevelUpUI);
        }

      
        
        /// <summary>
        /// 执行响应事件(玩家升级/资源已满)
        /// </summary>
        public void ShowUIConfirm()
        {
            if (QueueUIConfirm.Count > 0)
            {
                QueueUIConfirm.Dequeue().Invoke();
            }
        }

       
        public override void Dispose()
        {
            QueueUIConfirm.Clear();
            CraftConfigId = -1;

        }
    }
}
