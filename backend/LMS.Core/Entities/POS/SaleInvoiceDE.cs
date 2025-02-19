using LMS.Core.Entities.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Core.Entities.POS
{
    public class SaleInvoiceDE: BaseDomain
    {
        #region Constructor
        public SaleInvoiceDE()
        {
            SILines = new List<SaleInvoiceDetailDE>();
        }
        #endregion

           public string? InvoiceNo { get; set; }
            public string? CustomerName { get; set; }
            public string? Cell { get; set; }
            public DateTime? Date { get; set; }
        public List<SaleInvoiceDetailDE> SILines { get; set; }
    }
}
