using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiepei.ERP.DeliverCentersClient.Dto.SheetMetal
{
    public class DC_CancelOrderDto
    {
        //
        // 摘要:
        //     取消理由
        [Required]
        public string CancelReason
        {
            get;
            set;
        }
    }
}
