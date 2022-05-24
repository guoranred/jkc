using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiepei.ERP.DeliverCentersClient.Dto.SheetMetal
{
    public class Rootobject
    {
        public int productNum { get; set; }
        public Bomlist[] bomList { get; set; }
        public bool isBackPricing { get; set; }
        public int pricingCompany { get; set; }
    }

    public class Bomlist
    {
        public string partName { get; set; }
        public string length { get; set; }
        public string width { get; set; }
        public string height { get; set; }
        public string materialId { get; set; }
        public string materialCategoryName { get; set; }
        public string materialName { get; set; }
        public string num { get; set; }
        public string remark { get; set; }
        public Bomcraftlist[] bomCraftList { get; set; }
    }

    public class Bomcraftlist
    {
        public string craftId { get; set; }
        public object craftValue { get; set; }
        public string fieldType { get; set; }
        public int bomMaterialId { get; set; }
        public string itemName { get; set; }
        public string craftName { get; set; }
        public string craftUnit { get; set; }
    }

}
