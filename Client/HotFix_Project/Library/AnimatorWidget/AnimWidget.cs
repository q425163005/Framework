using CSF.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotFix_Project.AnimatorWidget
{
    public class AnimWidget
    {

        private EAnimState m_State;
        private int m_Param;        

        private Animator m_Animator;
        public AnimWidget()
        {
           
        }

        public void SetAnimator(Animator anim)
        {
            m_Animator = anim;
            m_Animator.SetInteger(AnimParam.State, (int)m_State);
            m_Animator.SetInteger(AnimParam.Param, m_Param);
        }

        /// <summary>
        /// 播放待机动画
        /// </summary>
        /// <param name="param"></param>
        public async CTask PlayIdle(EAnimIdleParam param = EAnimIdleParam.Idle)
        {
            m_State = EAnimState.Idle;
            m_Param = (int)param;
            if (m_Animator == null) return;
            m_Animator.SetInteger(AnimParam.State, (int)EAnimState.Idle);
            m_Animator.SetInteger(AnimParam.Param, m_Param);
            if (param != EAnimIdleParam.Idle)
            {
                await CTask.WaitForNextFrame();
                if(m_Animator!=null)
                    m_Animator.SetInteger(AnimParam.Param, (int)EAnimIdleParam.Idle);
            }
        }
        /// <summary>
        /// 设置播放速度
        /// </summary>
        /// <param name="speed"></param>
        public void SetSpeed(float speed)
        {
            if (m_Animator == null) return;
            m_Animator.speed = speed;
        }

        /// <summary>
        /// 播放跑动画
        /// </summary>
        /// <param name="param"></param>
        public void PlayRun(EAnimRunParam param = EAnimRunParam.Run)
        {
            m_State = EAnimState.Run;
            m_Param = (int)param;
            if (m_Animator == null) return;
            m_Animator.SetInteger(AnimParam.State, (int)EAnimState.Run);
            m_Animator.SetInteger(AnimParam.Param, m_Param);            
        }
        /// <summary>
        /// 播放挥鞭动画
        /// </summary>
        public async CTask PlayRunWhip(bool isOnce = true)
        {
            if (m_Animator == null) return;
            m_Animator.SetBool(AnimParam.IsWhip, true);
            if (isOnce)
            {
                await CTask.WaitForNextFrame();
                if (m_Animator != null)
                    m_Animator.SetBool(AnimParam.IsWhip, false);
            }
        }
        /// <summary>
        /// 播放摔倒动画
        /// </summary>
        public void PlayTumble(bool isTumble)
        {
            if (m_Animator == null) return;
            m_Animator.SetBool(AnimParam.IsTumble, isTumble);
            //await CTask.WaitForEndOfFrame();
            //m_Animator.SetBool(AnimParam.IsTumble, false);
        }

        /// <summary>
        /// 播放跑动画
        /// </summary>
        /// <param name="param"></param>
        public void PlayTrain(EAnimTrainParam param = EAnimTrainParam.Run)
        {
            m_State = EAnimState.Train;
            m_Param = (int)param;
            if (m_Animator == null) return;
            m_Animator.SetInteger(AnimParam.State, (int)EAnimState.Train);
            m_Animator.SetInteger(AnimParam.Param, m_Param);
        }

        public void Dispose()
        {
            m_Animator = null;
        }
    }
}
