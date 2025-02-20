using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Core.Entities;
using LMS.Core.Enums;
using System.Data;
using K4os.Hash.xxHash;
using LMS.Core.Constants;
using static Dapper.SqlMapper;
using LMS.DAL;
using LMS.Core.Entities.POS;
using LMS.Service.POS; 

namespace LMS.Service.POS
{
    public partial class SaleService : BaseService, ISaleService
    {

        public ItemsDE Manage_Items(ItemsDE mod)
        {
            bool closeConnectionFlag = false;
            try
            {
                _entity = LMS.Core.Enums.TableNames.POS_Items.ToString();
                if (cmd == null || cmd.Connection.State != ConnectionState.Open)
                {
                    cmd = LMSDataContext.OpenMySqlConnection();
                    closeConnectionFlag = true;
                }




                if (_soDAL.Manage_Items(mod, cmd))
                {
                    mod.AddSuccessMessage(string.Format(AppConstants.CRUD_DB_OPERATION, _entity, mod.DBoperation.ToString()));
                    _logger.Info($"Success: " + string.Format(AppConstants.CRUD_DB_OPERATION, _entity, mod.DBoperation.ToString()));
                }
                else
                {
                    mod.AddErrorMessage(string.Format(AppConstants.CRUD_ERROR, _entity));
                    _logger.Info($"Error: " + string.Format(AppConstants.CRUD_ERROR, _entity));
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
            finally
            { 
                if (closeConnectionFlag)
                    LMSDataContext.CloseMySqlConnection(cmd);
            }
            return mod;
        }
        public List<ItemsDE> Search_Items(ItemsDE mod)
        {
            List<ItemsDE> item = new List<ItemsDE>();
            bool closeConnectionFlag = false;
            try
            {
                if (cmd == null || cmd.Connection.State != ConnectionState.Open)
                {
                    cmd = LMSDataContext.OpenMySqlConnection();
                    closeConnectionFlag = true;
                }
                #region Search

                string WhereClause = " Where 1=1";
                if (mod.Id != default && mod.Id != 0)
                    WhereClause += $" AND Id={mod.Id}";
                if (mod.ClientId != default && mod.ClientId != 0)
                    WhereClause += $" AND ClientId={mod.ClientId}";
                if (!string.IsNullOrEmpty(mod.ItemCode))
                    WhereClause += $" AND ItemCode='{mod.ItemCode}'";
                if (!string.IsNullOrEmpty(mod.Description))
                    WhereClause += $" AND Description='{mod.Description}'";
                if ((mod.Barcode != default && mod.Barcode != 0))
                    WhereClause += $" AND Barcode={mod.Barcode}";
                if (!string.IsNullOrEmpty(mod.BrandName))
                    WhereClause += $" AND BrandName='{mod.BrandName}'";
                if (!string.IsNullOrEmpty(mod.Size))
                    WhereClause += $" AND Size='{mod.Size}'";
                if ((mod.Quantity != default && mod.Quantity != 0))
                    WhereClause += $" AND Quantity='{mod.Quantity}'";
                if (mod.CostPrice != default && mod.CostPrice != 0)
                    WhereClause += $" AND CostPrice={mod.CostPrice}";
                if (mod.WholeSalePrice != default && mod.WholeSalePrice != 0)
                    WhereClause += $" AND wholeSalePrice={mod.WholeSalePrice}";
                if (mod.RetailPrice != default && mod.RetailPrice != 0)
                    WhereClause += $" AND RetailPrice={mod.RetailPrice}";
                if (mod.IsActive == true)
                    WhereClause += $" AND ctl.IsActive=1";


                if (mod.PageNo != default)
                    item = _soDAL.Search_Items(WhereClause, cmd, mod.PageNo, mod.PageSize);
                else
                    item = _soDAL.Search_Items(WhereClause, cmd );  ;

                #endregion
            }
            catch (Exception exp)
            {
                _logger.Error(exp);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    LMSDataContext.CloseMySqlConnection(cmd);
            }
            return item;
        }

    }
}

