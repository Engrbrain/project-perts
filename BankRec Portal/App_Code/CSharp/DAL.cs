using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Data;
using System.Collections;
using System.Data.Common;
using Microsoft.ApplicationBlocks.Data;
using BankStatementProcessor.Utility;
using BankStatementProcessor.ObjectInfo;
using System.Data.SqlClient;
/// <summary>
/// Summary description for DAL
/// </summary>
namespace BankStatementProcessor.DAL
{

    public class AppConnection
    {
        public static SqlConnection GetAppConnection()
        {
            return new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SiteSqlServer"].ConnectionString);
        }
        public static SqlConnection GetBobjAppConnection()
        {
            return new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["BOBJSqlServer"].ConnectionString);
        }
    }

    public class C2GBanks
    {
        public static OperationResultInfo InsertBank(string BakNAme)
        {
            OperationResultInfo retVal = new OperationResultInfo();

            retVal.ReturnObject = SqlHelper.ExecuteScalar(AppConnection.GetAppConnection(), "C2GBank_Insert", BakNAme);

            return retVal;

        }
        public static OperationResultInfo UpdateBank(int ID, string BakNAme, int Status)
        {
            OperationResultInfo retVal = new OperationResultInfo();

            retVal.ReturnObject = SqlHelper.ExecuteScalar(AppConnection.GetAppConnection(), "C2GBank_Update", ID, BakNAme, Status);

            return retVal;

        }
        public static OperationResultInfo FetchAllBank(int ID)
        {
            OperationResultInfo retVal = new OperationResultInfo();

            retVal.ReturnObject = SqlHelper.ExecuteDataset(AppConnection.GetAppConnection(), "C2GBank_Fetch", ID);

            return retVal;

        }
        public static OperationResultInfo FetchAllActiveBank()
        {
            OperationResultInfo retVal = new OperationResultInfo();

            retVal.ReturnObject = SqlHelper.ExecuteDataset(AppConnection.GetAppConnection(), "C2GBank_FetchActive");

            return retVal;

        }
    }
    public class C2GHouseBanks
    {
        public static OperationResultInfo InsertHouseBank(int BankID, string HouseBankID, string BankName)
        {
            OperationResultInfo retVal = new OperationResultInfo();

            retVal.ReturnObject = SqlHelper.ExecuteScalar(AppConnection.GetAppConnection(), "C2GHouseBank_Insert", BankID, HouseBankID, BankName);

            return retVal;

        }
        public static OperationResultInfo UpdateHouseBank(int ID, int BankID, string HouseBankID, int Status, string BankName)
        {
            OperationResultInfo retVal = new OperationResultInfo();

            retVal.ReturnObject = SqlHelper.ExecuteScalar(AppConnection.GetAppConnection(), "C2GHouseBank_Update", ID, BankID, HouseBankID, Status, BankName);

            return retVal;

        }
        public static OperationResultInfo FetchAllHouseBank(int ID, int BankID)
        {
            OperationResultInfo retVal = new OperationResultInfo();

            retVal.ReturnObject = SqlHelper.ExecuteDataset(AppConnection.GetAppConnection(), "C2GHouseBank_Fetch", ID, BankID);

            return retVal;

        }
        public static OperationResultInfo FetchAllActiveHouseBank(int BankID)
        {
            OperationResultInfo retVal = new OperationResultInfo();

            retVal.ReturnObject = SqlHelper.ExecuteDataset(AppConnection.GetAppConnection(), "C2GHouseBank_FetchActive", BankID);

            return retVal;

        }
    }

    public class C2GCompany
    {
        public static OperationResultInfo InsertCompany(string CompanyName, string companycode)
        {
            OperationResultInfo retVal = new OperationResultInfo();

            retVal.ReturnObject = SqlHelper.ExecuteScalar(AppConnection.GetAppConnection(), "C2GCompany_Insert", CompanyName, companycode);

            return retVal;

        }
        public static OperationResultInfo UpdateCompany(int ID, string CompanyName, string companycode, int Status)
        {
            OperationResultInfo retVal = new OperationResultInfo();

            retVal.ReturnObject = SqlHelper.ExecuteScalar(AppConnection.GetAppConnection(), "C2GCompany_Update", ID, CompanyName, companycode, Status);

            return retVal;

        }
        public static OperationResultInfo FetchAllCompany(int ID)
        {
            OperationResultInfo retVal = new OperationResultInfo();

            retVal.ReturnObject = SqlHelper.ExecuteDataset(AppConnection.GetAppConnection(), "C2GCompany_Fetch", ID);

            return retVal;

        }
        public static OperationResultInfo FetchAllActiveCompany()
        {
            OperationResultInfo retVal = new OperationResultInfo();

            retVal.ReturnObject = SqlHelper.ExecuteDataset(AppConnection.GetAppConnection(), "C2GCompany_FetchActive");

            return retVal;

        }
    }

    public class C2GAccount
    {
        public static OperationResultInfo InsertAccount(int Companyid, string BankId, string AccountId, string Currency, string Account)
        {
            OperationResultInfo retVal = new OperationResultInfo();

            retVal.ReturnObject = SqlHelper.ExecuteScalar(AppConnection.GetAppConnection(), "C2GAccount_Insert", Companyid, BankId, AccountId, Currency, Account);

            return retVal;

        }
        public static OperationResultInfo UpdateAccount(int ID, int Companyid, string BankId, string AccountId, string Currency, int status, string Account)
        {
            OperationResultInfo retVal = new OperationResultInfo();

            retVal.ReturnObject = SqlHelper.ExecuteScalar(AppConnection.GetAppConnection(), "C2GAccount_Update", ID, Companyid, BankId, AccountId, Currency, status, Account);

            return retVal;

        }
        public static OperationResultInfo FetchAllAccount(int ID, int BankID)
        {
            OperationResultInfo retVal = new OperationResultInfo();

            retVal.ReturnObject = SqlHelper.ExecuteDataset(AppConnection.GetAppConnection(), "C2GAccount_Fetch", ID, BankID);

            return retVal;

        }
        public static OperationResultInfo FetchAllActiveAccount()
        {
            OperationResultInfo retVal = new OperationResultInfo();

            retVal.ReturnObject = SqlHelper.ExecuteDataset(AppConnection.GetAppConnection(), "C2GAccount_FetchActive");

            return retVal;

        }

        public static OperationResultInfo FetchBAnkByAccountID(int ID)
        {
            OperationResultInfo retVal = new OperationResultInfo();

            retVal.ReturnObject = SqlHelper.ExecuteDataset(AppConnection.GetAppConnection(), "C2GAccount_FetchBankBYAccount", ID);

            return retVal;

        }
    }

    public class C2GSystemSettings
    {
        public static OperationResultInfo InsertSystemSettings(int BankID, string Transaction, string ExternalCode, string BankTransactionCategory, string OperationType, string BankName)
        {
            OperationResultInfo retVal = new OperationResultInfo();

            retVal.ReturnObject = SqlHelper.ExecuteScalar(AppConnection.GetAppConnection(), "C2GSystemSettings_insert", BankID, Transaction, ExternalCode, BankTransactionCategory, OperationType, BankName);

            return retVal;

        }
        public static OperationResultInfo UpdateSystemSettings(int ID, int BankID, string Transaction, string ExternalCode, string BankTransactionCategory, string OperationType, int Status, string BankName)
        {
            OperationResultInfo retVal = new OperationResultInfo();

            retVal.ReturnObject = SqlHelper.ExecuteScalar(AppConnection.GetAppConnection(), "C2GSystemSettings_Update", ID, BankID, Transaction, ExternalCode, BankTransactionCategory, OperationType, Status, BankName);

            return retVal;

        }
        public static OperationResultInfo FetchAllSystemSettings(int ID, int BankID)
        {
            OperationResultInfo retVal = new OperationResultInfo();

            retVal.ReturnObject = SqlHelper.ExecuteDataset(AppConnection.GetAppConnection(), "C2GSystemSettings_Fetch", ID, BankID);

            return retVal;

        }
        public static OperationResultInfo FetchAllActiveSystemSettings()
        {
            OperationResultInfo retVal = new OperationResultInfo();

            retVal.ReturnObject = SqlHelper.ExecuteDataset(AppConnection.GetAppConnection(), "C2GSystemSettings_FetchActive");

            return retVal;

        }

        public static OperationResultInfo FetchAllSystemSettingsBYBank(int ID)
        {
            OperationResultInfo retVal = new OperationResultInfo();

            retVal.ReturnObject = SqlHelper.ExecuteDataset(AppConnection.GetAppConnection(), "C2GSystemSettings_FetchByBankID", ID);

            return retVal;

        }
    }
    public class C2GFormats
    {
        public static OperationResultInfo InsertFormats(ref int id, int BakNAme, string ColumnName, string ColumnText, string BankName)
        {
            OperationResultInfo retVal = new OperationResultInfo();

            retVal.ReturnObject = SqlHelper.ExecuteScalar(AppConnection.GetAppConnection(), "C2GBAnkStatementFormat_Insert", id, BakNAme, ColumnName, ColumnText, BankName);

            return retVal;

        }
        public static OperationResultInfo UpdateFormats(int ID, int BakNAme, string ColumnName, string ColumnText, int Status, string BankName)
        {
            OperationResultInfo retVal = new OperationResultInfo();

            retVal.ReturnObject = SqlHelper.ExecuteScalar(AppConnection.GetAppConnection(), "C2GBAnkStatementFormat_Update", ID, BakNAme, ColumnName, ColumnText, Status, BankName);

            return retVal;

        }
        public static OperationResultInfo FetchAllFormat(int ID, int BankID)
        {
            OperationResultInfo retVal = new OperationResultInfo();

            retVal.ReturnObject = SqlHelper.ExecuteDataset(AppConnection.GetAppConnection(), "C2GBAnkStatementFormat_Fetch", ID, BankID);

            return retVal;

        }
        public static OperationResultInfo FetchAllActiveFormatByBankID(int BankID)
        {
            OperationResultInfo retVal = new OperationResultInfo();

            retVal.ReturnObject = SqlHelper.ExecuteDataset(AppConnection.GetAppConnection(), "C2GBAnkStatementFormat_FetchByBankID", BankID);

            return retVal;

        }

        public static OperationResultInfo UpdateFormatIndex(int ID, int Index)
        {
            OperationResultInfo retVal = new OperationResultInfo();

            retVal.ReturnObject = SqlHelper.ExecuteDataset(AppConnection.GetAppConnection(), "C2GBAnkStatementFormat_UpdateIndex", ID, Index);

            return retVal;

        }

        public static OperationResultInfo DeleteFormatByID(int ID)
        {
            OperationResultInfo retVal = new OperationResultInfo();

            retVal.ReturnObject = SqlHelper.ExecuteScalar(AppConnection.GetAppConnection(), "C2GBAnkStatementFormat_delete", ID);

            return retVal;

        }
    }

    public class C2GDateGetter
    {
        public static OperationResultInfo GetDate(string MainDate)
        {
            OperationResultInfo retVal = new OperationResultInfo();

            retVal.ReturnObject = SqlHelper.ExecuteScalar(AppConnection.GetAppConnection(), "C2GDateFormat_Fetch", MainDate);

            return retVal;

        }
        public static OperationResultInfo GetValueDate(string MainDate)
        {
            OperationResultInfo retVal = new OperationResultInfo();

            retVal.ReturnObject = SqlHelper.ExecuteScalar(AppConnection.GetAppConnection(), "C2GDateFormat_FetchValueDate", MainDate);

            return retVal;

        }
    }
    public class C2GReportGetter
    {
        public static OperationResultInfo GetMAinReport(string StartDate, string AccountNo, string Company, string Client)
        {
            OperationResultInfo retVal = new OperationResultInfo();
            SqlConnection con = AppConnection.GetBobjAppConnection();
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand command = con.CreateCommand();
            command.Connection = con;
            command.CommandTimeout = 7200;
            DataSet dsreply = new DataSet();
            try
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "C2GReconReport_FetchByStatusTest";
                command.Parameters.Add(new SqlParameter("@AccountID", AccountNo));
                command.Parameters.Add(new SqlParameter("@Date", StartDate));
                command.Parameters.Add(new SqlParameter("@CompanyCode", Company));
                command.Parameters.Add(new SqlParameter("@Client", Client));

                SqlDataAdapter _Adapter = new SqlDataAdapter();
                _Adapter.SelectCommand = command;
                _Adapter.Fill(dsreply);
                _Adapter.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
           retVal.ReturnObject = dsreply;
            return retVal;
        }

        public static OperationResultInfo GetUserBU(string UserId)
        {
            OperationResultInfo retVal = new OperationResultInfo();

            retVal.ReturnObject = SqlHelper.ExecuteScalar(AppConnection.GetAppConnection(), "C2G_FetchBU", UserId).ToString();

            return retVal;

        }

        public static OperationResultInfo GetBuAccount(string BUId)
        {
            OperationResultInfo retVal = new OperationResultInfo();

            retVal.ReturnObject = SqlHelper.ExecuteScalar(AppConnection.GetAppConnection(), "C2G_GetAccountId", BUId).ToString();

            return retVal;

        }
        public static OperationResultInfo ExistenceTest(string AccountNo)
        {
            OperationResultInfo retVal = new OperationResultInfo();

            retVal.ReturnObject = SqlHelper.ExecuteScalar(AppConnection.GetBobjAppConnection(), "C2GReconReport_TestExistence", AccountNo);

            return retVal;

        }

    }

}