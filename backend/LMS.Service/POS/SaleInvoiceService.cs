using LMS.Core.Constants;
using LMS.Core.Enums;
using LMS.DAL;
using MySql.Data.MySqlClient;
using LMS.Core.Entities.POS;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Service.POS
{
    public partial class SaleService: BaseService, ISaleService
    {
            public SaleInvoiceDE POS_Manage_SaleInvoice(SaleInvoiceDE mod)
            {
                bool retVal = false;
                int lineId = 0;
                MySqlCommand cmd = null;
                try
                {
                    _entity = TableNames.POS_SaleInvoice.ToString();
                    cmd = LMSDataContext.OpenMySqlConnection();
                    LMSDataContext.StartTransaction(cmd);

                    if (mod.DBoperation == DBoperations.Insert)
                        mod.Id = _coreDAL.GetNextIdByClient(_entity, mod.ClientId ?? 0 , "ClientId");
                    if (_soDAL.POS_Manage_SaleInvoice(mod, cmd))
                    {
                        mod.AddSuccessMessage(string.Format(AppConstants.CRUD_DB_OPERATION, _entity, mod.DBoperation.ToString()));
                        _logger.Info($"Success: " + string.Format(AppConstants.CRUD_DB_OPERATION, _entity, mod.DBoperation.ToString()));
                    }
                    else
                    {
                        mod.AddErrorMessage(string.Format(AppConstants.CRUD_ERROR, _entity));
                        _logger.Info($"Error: " + string.Format(AppConstants.CRUD_ERROR, _entity));
                    }
                    if (mod.DBoperation == DBoperations.Insert || mod.DBoperation == DBoperations.Update)
                        lineId = _coreDAL.GetNextLineIdByClt(TableNames.POS_SaleInvoiceDetail.ToString(), "SaleInvoiceId", mod.Id.Value, mod.ClientId ?? 0);
                    foreach (var line in mod.SILines)
                    {
                        line.SaleInvoiceId = mod.Id. Value ;
                        line.ClientId = mod.ClientId;
                        if (line.DBoperation == DBoperations.Insert)
                        {
                            // lineId += 1;
                            line.Id = lineId;
                        }
                        if (_soDAL.POS_Manage_SaleInvoiceDetail(line, cmd))
                        {
                            mod.AddSuccessMessage(string.Format(AppConstants.CRUD_DB_OPERATION, _entity, mod.DBoperation.ToString()));
                            _logger.Info($"Success: " + string.Format(AppConstants.CRUD_DB_OPERATION, _entity, mod.DBoperation.ToString()));
                            if (line.DBoperation == DBoperations.Insert)
                                lineId += 1;
                        }
                        else
                        {
                            mod.AddErrorMessage(string.Format(AppConstants.CRUD_ERROR, _entity));
                            _logger.Info($"Error: " + string.Format(AppConstants.CRUD_ERROR, _entity));
                        }

                    }
                    if (retVal == true)

                        mod.DBoperation = DBoperations.NA;
                    LMSDataContext.EndTransaction(cmd);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                    LMSDataContext.CancelTransaction(cmd);
                    throw;
                }
                finally
                {
                    if (cmd != null)
                        LMSDataContext.CloseMySqlConnection(cmd);
                string whereClause = " Where 1=1";
                mod.SILines = _soDAL.POS_Search_SaleInvoiceDetail(
                    whereClause += $" AND posd.SaleInvoiceId={mod.Id} AND posd.ClientId={mod.ClientId} AND posd.IsActive=1");
            }
            return mod;
            }
            public List<SaleInvoiceDE> POS_Search_SaleInvoice(SaleInvoiceDE mod)
            {
                List<SaleInvoiceDE> SIVs = new List<SaleInvoiceDE>();
                bool closeConnectionFlag = false;
                MySqlCommand cmd = null;
                try
                {
                    cmd = LMSDataContext.OpenMySqlConnection();

                #region Search

                string whereClause = " Where 1=1";
                if (mod.Id != default)
                    whereClause += $" AND pos.Id={mod.Id}";
                if (mod.ClientId != default)
                    whereClause += $" AND pos.ClientId={mod.ClientId}";
                if (mod.IsActive != default)
                    whereClause += $" AND pos.IsActive=1";

                SIVs = _soDAL.POS_Search_SaleInvoice(whereClause);
                foreach (var SIV in SIVs)
                {
                    whereClause = " Where 1=1";
                    SIV.SILines = _soDAL.POS_Search_SaleInvoiceDetail(
                        whereClause += $" AND posd.SaleInvoiceId={SIV.Id} AND posd.ClientId={mod.ClientId} AND posd.IsActive=1");
                }

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
                return SIVs;
            }
            public string GetNextInvoNo(SaleInvoiceDE SI)
            {
                return _coreDAL.GetNextPageNo(SI.ClientId?? 0, TableNames.POS_SaleInvoice.ToString(), "InvoiceNo", "INV-");
            }
        }
    }
