using System.ComponentModel;

namespace Jiepei.ERP.DeliverCentersClient.Enums
{
    /// <summary>
    /// 应用领域
    /// </summary>
    public enum EnumDeliverCenterApplicationArea
    {       
        /// <summary>
             /// 无
             /// </summary>
        [Description("无")]
        Other = 0,
        /// <summary>
        /// 汽车行业
        /// </summary>
        [Description("汽车行业")]
        AutomobileIndustry = 1,

        /// <summary>
        /// 消费产品
        /// </summary>
        [Description("消费产品")]
        Consumer = 2,

        /// <summary>
        /// 日常用品
        /// </summary>
        [Description("日常用品")]
        DailyNecessities = 3,

        /// <summary>
        /// 智能硬件
        /// </summary>
        [Description("智能硬件")]
        Intelligent = 4,

        /// <summary>
        /// 电子产品
        /// </summary>
        [Description("电子产品")]
        Electronics = 5,

        /// <summary>
        /// 能源设备
        /// </summary>
        [Description("能源设备")]
        Energy = 6,

        /// <summary>
        /// 自动设备
        /// </summary>
        [Description("自动设备")]
        AutoEquipment = 7,

        /// <summary>
        /// 游戏动漫
        /// </summary>
        [Description("游戏动漫")]
        Game = 8,

        /// <summary>
        /// 医疗牙科
        /// </summary>
        [Description("医疗牙科")]
        Medical = 9,

        /// <summary>
        /// 高校科研
        /// </summary>
        [Description("高校科研")]
        ScientificResearch = 10
    }
}
