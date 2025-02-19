using LMS.Core.Entities.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Service.POS
{
    public interface ISaleService
    {

        public SaleInvoiceDE POS_Manage_SaleInvoice(SaleInvoiceDE mod);
        public List<SaleInvoiceDE> POS_Search_SaleInvoice(SaleInvoiceDE mod);
        public string GetNextInvoNo(SaleInvoiceDE SI);
        public ItemsDE Manage_Items(ItemsDE mod);
        public List<ItemsDE> Search_Items(ItemsDE mod);

    }
}
