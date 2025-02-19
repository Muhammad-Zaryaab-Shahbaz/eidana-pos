using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Core.Entities.POS
{
    public class SaleInvoiceDetailDE : BaseDomain
    {
        public int? SaleInvoiceId { get; set; }
        public int? ItemCodeId { get; set; }
        public string? ItemCode { get; set; }
        public int? BarcodeId { get; set; }
        public string? Barcode { get; set; }
        public int? Quantity { get; set; }
        public double? Discount { get; set; }
        public double? DiscAmount { get; set; }
        public double? LineTotal { get; set; }
        public int? RetailPriceId { get; set; }
        public double? RetailPrice { get; set; } // Changed to double? if needed
    }
}
