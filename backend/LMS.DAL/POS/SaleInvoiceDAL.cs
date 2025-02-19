using Dapper;
using LMS.Core.Entities.POS;
using LMS.DAL;
using LMS.DAL.POS;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;  

namespace LMS.DAL.POS
{
    public partial class SaleDAL : ISaleDAL
    {
        #region Header Operations
        public bool POS_Manage_SaleInvoice(SaleInvoiceDE pos, MySqlCommand? cmd)
        {
            bool closeConnectionFlag = false;
            try
            {
                if (cmd == null)
                {
                    cmd = LMSDataContext.OpenMySqlConnection();
                    closeConnectionFlag = true;
                }
                if (cmd.Connection.State == ConnectionState.Open)
                    Console.WriteLine("Connection  has been created");
                else
                    Console.WriteLine("Connection error");
                cmd.CommandText = "POS_Manage_SaleInvoice";
                cmd.Parameters.AddWithValue("prm_id", pos.Id);
                cmd.Parameters.AddWithValue("prm_clientId", pos.ClientId);
                cmd.Parameters.AddWithValue("prm_invoiceNo", pos.InvoiceNo);
                cmd.Parameters.AddWithValue("prm_customerName", pos.CustomerName);
                cmd.Parameters.AddWithValue("prm_cell", pos.Cell);
                cmd.Parameters.AddWithValue("prm_date", pos.Date);
                cmd.Parameters.AddWithValue("prm_createdOn", pos.CreatedOn);
                cmd.Parameters.AddWithValue("prm_createdById", pos.CreatedById);
                cmd.Parameters.AddWithValue("prm_modifiedOn", pos.ModifiedOn);
                cmd.Parameters.AddWithValue("prm_modifiedById", pos.ModifiedById);
                cmd.Parameters.AddWithValue("prm_isActive", pos.IsActive);
                cmd.Parameters.AddWithValue("prm_dBoperation", pos.DBoperation.ToString());

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    LMSDataContext.CloseMySqlConnection(cmd);
                cmd.Parameters.Clear();
            }
        }
        public List<SaleInvoiceDE> POS_Search_SaleInvoice(string whereClause, MySqlCommand cmd = null)
        {
            List<SaleInvoiceDE> top = new List<SaleInvoiceDE>();
            bool closeConnectionFlag = false;
            try
            {
                if (cmd == null)
                {
                    cmd = LMSDataContext.OpenMySqlConnection();
                    closeConnectionFlag = true;
                }
                if (cmd.Connection.State == ConnectionState.Open)
                    Console.WriteLine("Connection  has been created");
                else
                    Console.WriteLine("Connection error");
                top = cmd.Connection.Query<SaleInvoiceDE>("call POS_Search_SaleInvoice( '" + whereClause + "')").ToList();
                return top;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    LMSDataContext.CloseMySqlConnection(cmd);
                cmd.Parameters.Clear();
            }
        }
        #endregion
        #region Line Operations
        public bool POS_Manage_SaleInvoiceDetail (SaleInvoiceDetailDE posd, MySqlCommand? cmd)
        {
            bool closeConnectionFlag = false;
            try
            {
                if (cmd == null)
                {
                    cmd = LMSDataContext.OpenMySqlConnection();
                    closeConnectionFlag = true;
                }
                if (cmd.Connection.State == ConnectionState.Open)
                    Console.WriteLine("Connection  has been created");
                else
                    Console.WriteLine("Connection error");
                cmd.CommandText = "POS_Manage_SaleInvoiceDetail";
                cmd.Parameters.AddWithValue("prm_id", posd.Id > 0 ? posd.Id : DBNull.Value);
                cmd.Parameters.AddWithValue("prm_clientId", posd.ClientId);
                cmd.Parameters.AddWithValue("prm_saleInvoiceId", posd.SaleInvoiceId);
                cmd.Parameters.AddWithValue("prm_itemCodeId", posd.ItemCodeId);
                cmd.Parameters.AddWithValue("prm_barcodeId", posd.BarcodeId);
                cmd.Parameters.AddWithValue("prm_quantity", posd.Quantity);
                cmd.Parameters.AddWithValue("prm_discount", posd.Discount);
                cmd.Parameters.AddWithValue("prm_discAmount", posd.DiscAmount);
                cmd.Parameters.AddWithValue("prm_lineTotal", posd.LineTotal);
                cmd.Parameters.AddWithValue("prm_createdOn", posd.CreatedOn);
                cmd.Parameters.AddWithValue("prm_createdById", posd.CreatedById);
                cmd.Parameters.AddWithValue("prm_modifiedOn", posd.ModifiedOn);
                cmd.Parameters.AddWithValue("prm_modifiedById", posd.ModifiedById);
                cmd.Parameters.AddWithValue("prm_isActive", posd.IsActive);
                cmd.Parameters.AddWithValue("prm_retailPriceId", posd.RetailPriceId);
                cmd.Parameters.AddWithValue("prm_dBoperation", posd.DBoperation.ToString());

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    LMSDataContext.CloseMySqlConnection(cmd);
                cmd.Parameters.Clear();
            }
        }
        public List<SaleInvoiceDetailDE> POS_Search_SaleInvoiceDetail(string whereClause, MySqlCommand cmd = null)
        {
            List<SaleInvoiceDetailDE> top = new List<SaleInvoiceDetailDE>();
            bool closeConnectionFlag = false;
            try
            {
                if (cmd == null)
                {
                    cmd = LMSDataContext.OpenMySqlConnection();
                    closeConnectionFlag = true;
                }
                if (cmd.Connection.State == ConnectionState.Open)
                    Console.WriteLine("Connection  has been created");
                else
                    Console.WriteLine("Connection error");
                top = cmd.Connection.Query<SaleInvoiceDetailDE>("call POS_Search_SaleInvoiceDetail( '" + whereClause + "')").ToList();
                return top;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    LMSDataContext.CloseMySqlConnection(cmd);
            }
        }
        #endregion
    }
}
