using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiepei.ERP.Cncs
{
    public enum EnumCncSurfaceLevel
    {

        /// <summary>
        /// 默认
        /// </summary>   
        [EnumDesc("")]
        Level0 = 0,

        /// <summary>
        /// 一级（表面无划痕无刀路）
        /// </summary>   
        [EnumDesc("一级（表面无划痕无刀路）")]
        Level1 = 1,

        /// <summary>
        /// 二级（表面些许轻微划痕）
        /// </summary>   
        [EnumDesc("二级（表面些许轻微划痕）")]
        Level2 = 2,

        /// <summary>
        /// 三级（表面处理即可）
        /// </summary>   
        [EnumDesc("三级（表面处理即可）")]
        Level3 = 3,

        /// <summary>
        /// 四级（无表面处理要求）
        /// </summary>   
        [EnumDesc("四级（无表面处理要求）")]
        Level4 = 4,

    }
}
