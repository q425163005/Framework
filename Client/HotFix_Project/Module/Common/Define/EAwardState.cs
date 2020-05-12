namespace HotFix_Project.Common
{
    public enum EAwardState
    {
        /// <summary>未完成</summary>
        Undone = 0,
        /// <summary>条件已达成未领取(可领取)</summary>
        Done = 1,
        /// <summary>已领取(或已结束)</summary>
        HaveGet = 2,

    }
}
