using MySql.Data.MySqlClient;
using LMS.Core.Entities.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Core.Constants;


namespace LMS.DAL.POS
{
    public interface  ISaleDAL
    {
        public bool POS_Manage_SaleInvoice(SaleInvoiceDE pos, MySqlCommand? cmd);
        public List<SaleInvoiceDE> POS_Search_SaleInvoice(string whereClause, MySqlCommand cmd = null);
        public bool POS_Manage_SaleInvoiceDetail(SaleInvoiceDetailDE posd, MySqlCommand? cmd);
        public List<SaleInvoiceDetailDE> POS_Search_SaleInvoiceDetail(string whereClause, MySqlCommand cmd = null);

        public bool Manage_Items(ItemsDE mod, MySqlCommand? cmd);
        public List<ItemsDE> Search_Items(string WhereClause, MySqlCommand? cmd, int PageNo = 1, int PageSize = AppConstants.GRID_MAX_PAGE_SIZE);
    }
}
