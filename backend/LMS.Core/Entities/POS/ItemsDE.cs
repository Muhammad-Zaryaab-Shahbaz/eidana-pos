using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LMS.Core.Entities.POS
{
    public class ItemsDE : BaseDomain 
    {
        #region Properties
        public string? ItemCode { get; set; }
        public string? Description { get; set; }
        public int? Barcode { get; set; }
        public string? BrandName { get; set; }
        public string? Size { get; set; }
        public int? Quantity { get; set; }
        public int? CostPrice { get; set; }
        public int? WholeSalePrice { get; set; }
        public int? RetailPrice { get; set; }
      
        #endregion

    }
}
