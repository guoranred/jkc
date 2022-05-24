using Jiepei.ERP.DeliverCentersClient.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiepei.ERP.DeliverCentersClient.Dto.SheetMetal
{
    public class DC_CreateOrderMemberDemandDto
    {
        /// <summary>
        /// 渠道客户名称
        /// </summary>
        public string MemberName { get; set; }

        /// <summary>
        /// 渠道客户编号
        /// </summary>
        public string MemberNo { get; set; }

        /// <summary>
        /// 渠道客户等级
        /// </summary>
        public int MemberLevel { get; set; }

        /// <summary>
        /// 渠道客户电话
        /// </summary>
        public string MemberPhone { get; set; }

        /// <summary>
        /// 应用领域
        /// </summary>
        public EnumDeliverCenterApplicationArea? ApplicationArea { get; set; }

        /// <summary>
        /// 预计年使用量
        /// </summary>
        public EnumDeliverCenterUsage? Usage { get; set; }

        /// <summary>
        /// 客户选择快递类型(code)
        /// </summary>
        public string MemberCourierCompany { get; set; }

        /// <summary>
        /// 客户备注
        /// </summary>
        public string MemberRemark { get; set; }

        /// <summary>
        /// 总重量
        /// </summary>
        public decimal Weight { get; set; }
    }
}