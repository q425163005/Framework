namespace HotFix_Project.Common
{
    /// <summary>
    /// 装备排序类型
    /// </summary>
    public enum EHeroEditor
    {
        /// <summary> 只查看英雄技能属性 </summary>
        OnlyCheck = 0 ,

        /// <summary> 查看英雄技能及天赋属性但不能编辑 </summary>
        CheckNotEdit = 1,

        /// <summary> 查看英雄技能及天赋属性并可以编辑 </summary>
        CheckAndEdit = 2,             
    }

    
}