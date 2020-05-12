using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace HotFix_Project
{
    /// <summary>新手指引步骤</summary>
    public class GuideStepConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 指引Id*100+步骤序号
        /// </summary>
        public int id;
        /// <summary>
        /// 指引类型
        /// 0:对话
        /// 1:UI操作
        /// 2:打开海报界面
        /// 3:箭头提示
        /// 4:进入指引战斗
        /// 5:战斗拖动(点击)
        /// 6:指引改名
        /// </summary>
        public int type;
        /// <summary>
        /// 指引类型参数
        /// 4:战斗关Id
        /// 5:高亮格子索引
        /// </summary>
        public int[] typeArgs;
        /// <summary>
        /// 触发指引类型
        /// 0:完成上步自动触发
        /// 1:打开UI触发
        /// 2:加载场景触发
        /// 3:中元素符号触发
        /// </summary>
        public int triggerType;
        /// <summary>
        /// 触发参数2
        /// HomeUI:
        /// 0表示主城
        /// 1表示副本
        /// 
        /// WarUI
        /// 0等待可操作
        /// 1不限制
        /// 
        /// StagePreviewUI:
        /// 关卡Id
        /// </summary>
        public int triggerArg2;
        /// <summary>
        /// 触发参数
        /// 1:UI名称
        /// 2:场景名称
        /// 3:符号类型
        /// </summary>
        public string triggerArg;
        /// <summary>
        /// 指引点击对象
        /// 
        /// 
        /// </summary>
        public string clickTarget;
        /// <summary>
        /// 是否隐藏黑色遮罩
        /// 默认不隐藏
        /// 1隐藏
        /// </summary>
        public bool hideMask;
        /// <summary>
        /// 跳过当前步骤类型
        /// 0不可跳过
        /// 1可跳过
        /// 2延时5秒显示跳过
        /// </summary>
        public int skipType;
        /// <summary>
        /// 完成此步是否强制保存进度，保存后断线重连后，此指引不再出现
        /// 
        /// 默认整体指引步骤完成之后再自动保存
        /// </summary>
        public bool isForceSave;
        /// <summary>
        /// 指引旋转角度
        /// (只有指引对象才会出箭头)
        /// -1:无
        /// 0旋转0度
        /// 45
        /// 90
        /// 180
        /// 270
        /// </summary>
        public int arrowAngle;
        /// <summary>
        /// 位置偏移
        /// 相对于点击对象的位置
        /// </summary>
        public int[] arrowOffset;
        /// <summary>
        /// 箭头内容文字
        /// 不填不显示文字
        /// </summary>
        public Lang arrowTxt;
        /// <summary>
        /// 位置偏移
        /// 箭头对象的位置偏移，不填写默认箭头对象上方150像素
        /// 
        /// (对话指引表示对话框的偏移,原显示位置进行偏移)
        /// </summary>
        public int[] arrowTxtOffset;
        /// <summary>
        /// 指引箭头延时显示(秒)
        /// </summary>
        public double arrowDelay;
        /// <summary>
        /// 头像类型
        /// 0无头像(文字中间)
        /// 1英雄头像
        /// 2怪物头像
        /// 3怪物图片
        /// </summary>
        public int dialogHeadType;
        /// <summary>
        /// 对话NPC头像
        /// 对应英雄或怪配置Id
        /// </summary>
        public int dialogHeadId;
        /// <summary>
        /// 头像缩放
        /// 英雄建议1
        /// 怪物建议2
        /// </summary>
        public double dialogHeadScale;
        /// <summary>
        /// 头像显示
        /// 0左边
        /// 1右边
        /// 2左边(转)
        /// 3右边(转)
        /// </summary>
        public int headPlace;
        /// <summary>
        /// 对话背景图
        /// 没有不填
        /// </summary>
        public string dialogBG;
        /// <summary>
        /// 对话内容
        /// {n}玩家名
        /// </summary>
        public Lang dialogTxt;
        /// <summary>
        /// 是否为关闭UI
        /// </summary>
        public bool isCloseUI;
        /// <summary>
        /// 前往字符串参数
        /// 1:UI名
        /// </summary>
        public string guideGoStrArg;
        /// <summary>
        /// 前往Int参数1
        /// 前往Int参数2
        /// </summary>
        public int[] guideGoIntArg;
       
    }        
}
