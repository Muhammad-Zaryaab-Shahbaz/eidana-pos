using Dapper;
using LMS.Core.Entities;
using LMS.Core.Enums;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text.RegularExpressions;

namespace LMS.DAL
{
    public class CoreDAL
    {

            public int GetnextId(string mod)
            {
                int retVal = 0;
                MySqlCommand cmd = LMSDataContext.OpenMySqlConnection();
                try
                {
                    cmd = LMSDataContext.SetStoredProcedure(cmd, "GetMaxId");
                    retVal = LMSDataContext.ExecuteScalar(LMSDataContext.AddParameters(cmd
                        , "@prm_TableName", mod.ToString()
                        ));
                    retVal += 1;
                    //if (retVal != -1)
                    //    retVal += 1;
                    //else     
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    LMSDataContext.CloseMySqlConnection(cmd);
                }
                return retVal;
            }
            public int GetMaxId(string mod)
            {
                int retVal = 0;
                MySqlCommand cmd = LMSDataContext.OpenMySqlConnection();
                try
                {
                    cmd = LMSDataContext.SetStoredProcedure(cmd, "GetMaxId");
                    retVal = LMSDataContext.ExecuteScalar(LMSDataContext.AddParameters(cmd
                        , "@prm_TableName", mod.ToString()
                        ));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    LMSDataContext.CloseMySqlConnection(cmd);
                }
                return retVal;
            }

            public int GetnextLineId(string mod, int headerId, string columnName)
            {
                int retVal = 0;
                MySqlCommand cmd = LMSDataContext.OpenMySqlConnection();
                try
                {
                    cmd = LMSDataContext.SetStoredProcedure(cmd, "GetMaxLineId");
                    retVal = LMSDataContext.ExecuteScalar(LMSDataContext.AddParameters(cmd
                        , "@prm_TableName", mod.ToString(),
                                    "@prm_HeaderId", headerId,
                                    "@prm_ColumnName", columnName
                        ));
                    // retVal += 1;
                    //if (retVal != -1)
                    //    retVal += 1;
                    //else     
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    LMSDataContext.CloseMySqlConnection(cmd);
                }
                return retVal;
            }
            public int GetnextAddOnNo(int headerId, string columnName, int ClientId)
            {
                int retVal = 0;
                MySqlCommand cmd = LMSDataContext.OpenMySqlConnection();
                try
                {
                    cmd = LMSDataContext.SetStoredProcedure(cmd, "GetNextAddOnNo");
                    retVal = LMSDataContext.ExecuteScalar(LMSDataContext.AddParameters(cmd
                        ,
                                    "@prm_HeaderId", headerId,
                                    "clientId", ClientId,
                                    "@prm_ColumnName", columnName
                        ));
                    retVal += 1;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    LMSDataContext.CloseMySqlConnection(cmd);
                }
                return retVal;
            }
            public List<EnumValueDE> GetEnumValues(EnumType type, MySqlCommand cmd = null)
            {
                List<EnumValueDE> top = new List<EnumValueDE>();
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
                    top = cmd.Connection.Query<EnumValueDE>("call Restaurant.GetEnumValues( '" + (int)type + "')").ToList();
                    return top;
                }
                catch (Exception exp)
                {
                    return top;
                }
                finally
                {
                    if (closeConnectionFlag)
                        LMSDataContext.CloseMySqlConnection(cmd);
                }
            }

            public bool ManageEnumValue(EnumValueDE mod, MySqlCommand cmd = null)
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
                    cmd.CommandText = "ManageEnumValue";
                    cmd.Parameters.AddWithValue("@id", mod.Id);
                    cmd.Parameters.AddWithValue("@enumTypeId", mod.EnumTypeId);
                    cmd.Parameters.AddWithValue("@value", mod.Value);

                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    if (closeConnectionFlag)
                        LMSDataContext.CloseMySqlConnection(cmd);
                }
            }
            public string GetnextVchNo(string mod)
            {
                string? retVal = null;
                MySqlCommand cmd = LMSDataContext.OpenMySqlConnection();
                try
                {
                    if (cmd == null)
                        cmd = LMSDataContext.OpenMySqlConnection();
                    cmd.CommandText = "GetNextVchNo";
                    cmd.Parameters.AddWithValue("@VchKeyCode", mod);
                    cmd.Parameters.Add(new MySqlParameter("@MaxVchNo", MySqlDbType.VarChar, 200));
                    cmd.Parameters["@MaxVchNo"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    retVal = cmd.Parameters["@MaxVchNo"].Value.ToString();
                    var numVal = 0;

                    if (retVal != null && retVal != "")
                    {
                        String numStr = Regex.Match(retVal, @"\d+\.*\d*").Value;
                        numVal = int.Parse(numStr);
                        numVal = numVal + 1;
                    }
                    else
                        numVal = 1;
                    retVal = mod + numVal;

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    LMSDataContext.CloseMySqlConnection(cmd);
                }
                return retVal;
            }

            public int GetNextIdByClient(string tblname, int headerId, string columnName)
            {
                int retVal = 0;
                MySqlCommand cmd = LMSDataContext.OpenMySqlConnection();
                try
                {
                    cmd = LMSDataContext.SetStoredProcedure(cmd, "GetMaxIdByClient");
                    retVal = LMSDataContext.ExecuteScalar(LMSDataContext.AddParameters(cmd
                        , "@prm_TableName", tblname.ToString(),
                                    "@prm_HeaderId", headerId,
                                    "@prm_ColumnName", columnName
                        ));
                    retVal += 1;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    LMSDataContext.CloseMySqlConnection(cmd);
                }
                return retVal;
            }
            public int GetNextLineIdByClt(string tblname, string columnName, int headerId, int clientId)
            {
                int retVal = 0;
                MySqlCommand cmd = LMSDataContext.OpenMySqlConnection();
                try
                {
                    cmd = LMSDataContext.SetStoredProcedure(cmd, "GetMaxLineIdByClt");
                    retVal = LMSDataContext.ExecuteScalar(LMSDataContext.AddParameters(cmd
                        , "@prm_TableName", tblname.ToString(),
                                    "@prm_HeaderId", headerId,
                                    "@prm_ClientId", clientId,
                                    "@prm_ColumnName", columnName
                        ));
                    retVal += 1;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    LMSDataContext.CloseMySqlConnection(cmd);
                }
                return retVal;
            }
            public int GetMaxIdByClient(string tblname, int headerId, string columnName)
            {
                int retVal = 0;
                MySqlCommand cmd = LMSDataContext.OpenMySqlConnection();
                try
                {
                    cmd = LMSDataContext.SetStoredProcedure(cmd, "GetMaxIdByClient");
                    retVal = LMSDataContext.ExecuteScalar(LMSDataContext.AddParameters(cmd
                        , "@prm_TableName", tblname.ToString(),
                                    "@prm_HeaderId", headerId,
                                    "@prm_ColumnName", columnName
                        ));
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    LMSDataContext.CloseMySqlConnection(cmd);
                }
                return retVal;
            }
            public int GetMaxLineIdByClt(string tblname, string columnName, int? headerId, int clientId)
            {
                int retVal = 0;
                MySqlCommand cmd = LMSDataContext.OpenMySqlConnection();
                try
                {
                    cmd = LMSDataContext.SetStoredProcedure(cmd, "GetMaxLineIdByClt");
                    retVal = LMSDataContext.ExecuteScalar(LMSDataContext.AddParameters(cmd
                        , "@prm_TableName", tblname.ToString(),
                                    "@prm_HeaderId", headerId,
                                    "@prm_ClientId", clientId,
                                    "@prm_ColumnName", columnName
                        ));
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    LMSDataContext.CloseMySqlConnection(cmd);
                }
                return retVal;
            }
            public string GenerateUniquePassword()
            {
                // Get current timestamp to ensure uniqueness
                long timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                // Generate a random 2-digit number
                Random random = new Random();
                int randomNum = random.Next(10, 100);

                // Combine timestamp and random number to create a 4-digit code
                string uniqueCode = $"{timestamp}{randomNum}";

                // Ensure the code is exactly 4 digits by taking the last 4 characters
                uniqueCode = uniqueCode.Substring(uniqueCode.Length - 4, 4);

                return uniqueCode;
            }

        public string GetNextPageNo(int ClientId, string tblname, string columnName, string prefix, MySqlCommand? cmd = null)
        {
            bool closeConnectionFlag = false;
            string nextPageNo = "";
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

                cmd.CommandText = "GetNextPageNumber";
                cmd.Parameters.AddWithValue("@clientId", ClientId);
                cmd.Parameters.AddWithValue("@tableName", tblname);
                cmd.Parameters.AddWithValue("@columnName", columnName);
                cmd.Parameters.AddWithValue("@prefix", prefix);
                cmd.Parameters.Add(new MySqlParameter("@nextNo", MySqlDbType.VarChar, 200));
                cmd.Parameters["@nextNo"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                nextPageNo = cmd.Parameters["@nextNo"].Value.ToString();

                return nextPageNo;
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


    }
}
    


