using Dapper;
using MySql.Data.MySqlClient;

using LMS.Core.Constants;
using System.Data; 
using LMS.Core.Entities.POS;
using LMS.DAL.POS;

namespace LMS.DAL.POS
{
    public partial class SaleDAL : ISaleDAL
    {

        #region DbOperations
        public bool Manage_Items(ItemsDE mod, MySqlCommand? cmd)
        {
            bool closeConnection = false;
            try
            {
                if (cmd == null)
                {
                    cmd = LMSDataContext.OpenMySqlConnection();
                    closeConnection = true;
                }
                cmd.CommandText = SPNames.POS_Manage_Items;
                cmd.Parameters.AddWithValue("prm_id", mod.Id);
                cmd.Parameters.AddWithValue("prm_clientId", mod.ClientId);
                cmd.Parameters.AddWithValue("prm_itemCode", mod.ItemCode);
                cmd.Parameters.AddWithValue("prm_description", mod.Description);
                cmd.Parameters.AddWithValue("prm_barcode", mod.Barcode);
                cmd.Parameters.AddWithValue("prm_brandName", mod.BrandName);
                cmd.Parameters.AddWithValue("prm_size", mod.Size);
                cmd.Parameters.AddWithValue("prm_quantity", mod.Quantity);
                cmd.Parameters.AddWithValue("prm_costPrice", mod.CostPrice);
                cmd.Parameters.AddWithValue("prm_wholeSalePrice", mod.WholeSalePrice);
                cmd.Parameters.AddWithValue("prm_retailPrice", mod.RetailPrice);
                cmd.Parameters.AddWithValue("prm_createdOn", mod.CreatedOn);
                cmd.Parameters.AddWithValue("prm_createdById", mod.CreatedById);
                cmd.Parameters.AddWithValue("prm_modifiedOn", mod.ModifiedOn);
                cmd.Parameters.AddWithValue("prm_modifiedById", mod.ModifiedById);
                cmd.Parameters.AddWithValue("prm_isActive", mod.IsActive);
                cmd.Parameters.AddWithValue("prm_dbOperation", mod.DBoperation.ToString());
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cmd.Parameters.Clear();
                if (closeConnection)
                    LMSDataContext.CloseMySqlConnection(cmd);
            }
        }
        public List<ItemsDE> Search_Items(string WhereClause, MySqlCommand? cmd, int PageNo = 1, int PageSize = AppConstants.GRID_MAX_PAGE_SIZE)
        {
            bool closeConnection = false;
            List<ItemsDE> Add = new List<ItemsDE>();
            try
            {
                if (cmd == null)
                {
                    cmd = LMSDataContext.OpenMySqlConnection();
                    closeConnection = true;
                }
                var parameters = new
                {
                    WhereClause = WhereClause
                ,
                    prm_Start = PageNo
                ,
                    prm_Limit = PageSize
                ,
                };
                Add = cmd.Connection.Query<ItemsDE>(SPNames.POS_Search_Items.ToString(), parameters, commandType: CommandType.StoredProcedure).ToList();
                return Add;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (closeConnection)
                    LMSDataContext.CloseMySqlConnection(cmd);
            }
        }

    }
    #endregion

}



