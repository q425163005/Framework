using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFix_Project.AnimatorWidget
{
    public class AnimParam
    {
        public static string State = "State";
        public static string Param = "Param";
        public static string IsWhip = "IsWhip";
        public static string IsTumble = "IsTumble";
    }
    /// <summary>
    /// 动画状态
    /// </summary>
    public enum EAnimState
    {
        Idle, //待机状态
        Run, //跑状态   
        Train,//训练
    }

    public enum EAnimIdleParam
    {
        Idle,
        ActHeadShake,  //摆头动作
        ActStep,         //蹬蹄动作
        ActLook,        //回头像动作
    }

    public enum EAnimRunParam
    {
        Run,
        FastRun,
        Tumble,
    }

    public enum EAnimTrainParam
    {
        Swimming, //游泳
        Trot,       //爬坡
        Walk,     //行走  
        Run,       //跑 
    }
}
