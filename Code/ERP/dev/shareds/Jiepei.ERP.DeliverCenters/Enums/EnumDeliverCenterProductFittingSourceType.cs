using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Jiepei.BizMO.DeliverCenters.PrecisionMetal.Enums
{
    public enum EnumDeliverCenterProductFittingSourceType
    {
        [Description("无 ")]
        Nothing =0,

        [Description("代采购 ")]
        Purchasing =1,

        [Description("自供")]
        SelfConfession =2,

        [Description("代采购 ")]
        blend =3
    }
}
